"""
Recommendation system CLI main script.
Execute this script to run the recommendation system CLI.
Usage (values' format is provided in description of dataset at
       https://archive.ics.uci.edu/dataset/697/predict+students+dropout+and+academic+success):
python recommendation.py
--marital-status <Student's marital status>
--application-mode <Student's application mode>
--application-order <Student's application order>
--course <Student's course taken>
--attendance <Student's time of attendance (0=night, 1=day)>
--prev-qualification <Student's Previous qualification>
--prev-qualification-grade <Student's Previous qualification grade>
--nationality <Student's nationality>
--mother-qualification <Student mother's qualification>
--father-qualification <Student father's qualification>
--mother-occupation <Student mother's occupation>
--father-occupation <Student father's occupation>
--admission-grade <Student's admission grade>
--displaced <1 if student was displaced, otherwise 0>
--educational-needs <1 if student has educational special needs, otherwise 0>
--debtor <1 if student is debtor, otherwise 0>
--tuition-up-to-date <1 if student's tuition fees are up to date, otherwise 0>
--gender <0 for female, 1 for male>
--scholarship <1 if student is a scholarship holder, otherwise 0>
--age <Student's age at enrollment>
--international <1 if student is studying international, otherwise 0>
--cu1st-sem-credited <Curricular units 1st sem (credited)>
--cu1st-sem-enrolled <Curricular units 1st sem (enrolled)>
--cu1st-sem-evaluations <Curricular units 1st sem (evaluations)>
--cu1st-sem-approved <Curricular units 1st sem (approved)>
--cu1st-sem-without-eval <Curricular units 1st sem (without evaluations)>
--cu2nd-sem-enrolled <Curricular units 2nd sem (enrolled)>
--cu2nd-sem-evaluations <Curricular units 2nd sem (evaluations)>
--cu2nd-sem-approved <Curricular units 2nd sem (approved)>
--cu2nd-sem-grade <Curricular units 2nd sem (grade)>
--cu2nd-sem-without-eval <Curricular units 2nd sem (without evaluations)>
--unemployment-rate <Student's unemployment rate>
--inflation-rate <Inflation rate>
--gdp <Student's GDP>
[--actions-rec-count <Number of actions to recommend>]
[--courses-rec-count <Number of courses to recommend>]
[--output <Output file path>]
[--err <Output error log file path>]

OR

python recommendation.py
--input <Input csv path>
[--actions-rec-count <Number of actions to recommend>]
[--courses-rec-count <Number of courses to recommend>]
[--output <Output file path>]
[--err <Output error log file path>]

Missing arguments will be collected interactively.
"""


from RecommendationException import RecommendationException
from typing import Optional, Callable, Tuple, List
from RecSys import RecSys
import pandas as pd
import predictor
import utils
import cli


# represents recommendation: description and effectiveness
Recommendation = Tuple[str, float]

# recommendations list
RecList = List[Recommendation]


# names of uility matrices chace
ACTIONS_UTILITY_CACHE_NAME: str = "actions_utility"
COURSESS_UTILITY_CACHE_NAME: str = "courses_utility"


# CLI input fields related to recommendation system
FIELD_NUM_ACTIONS_REC: cli.InputField = cli.InputField(int, "Number of actions to recommend", "--actions-rec-count", default=5)
FIELD_NUM_COURSES_REC: cli.InputField = cli.InputField(int, "Number of courses to recommend", "--courses-rec-count", default=5)
REC_SYS_INPUT_FIELDS: List[cli.InputField] = [FIELD_NUM_ACTIONS_REC, FIELD_NUM_COURSES_REC]


# CLI input fields related to IO
FIELD_INPUT_FILE: cli.InputField = cli.InputField(str, "CSV input file", "--input")
FIELD_OUTPUT_FILE: cli.InputField = cli.InputField(str, "Output file", "--output")
FIELD_OUTPUT_JSON: cli.InputField = cli.InputField(str, "Output JSON", "--output-json")
FIELD_ERR_FILE: cli.InputField = cli.InputField(str, "Error logging file", "--err")
IO_INPUT_FIELDS: List[cli.InputField] = [FIELD_INPUT_FILE, FIELD_OUTPUT_FILE, 
                                         FIELD_OUTPUT_JSON, FIELD_ERR_FILE]


def main() -> None:
    """
    Main function to be called when running as main module.
    Recommends based on CLI input fields.
    """
    # retrieve CLI input:
    io_dict, student_dict, rec_sys_dict = \
        cli.parse_args(IO_INPUT_FIELDS, cli.STUDENT_INPUT_FIELDS, REC_SYS_INPUT_FIELDS)
    input_csv_path: Optional[str] = io_dict[FIELD_INPUT_FILE.field_name]
    output_txt_path: Optional[str] = io_dict[FIELD_OUTPUT_FILE.field_name]
    output_json_path: Optional[str] = io_dict[FIELD_OUTPUT_JSON.field_name]
    err_log_path: Optional[str] = io_dict[FIELD_ERR_FILE.field_name]
    num_actions_rec: int = rec_sys_dict[FIELD_NUM_ACTIONS_REC.field_name]
    num_courses_rec: int = rec_sys_dict[FIELD_NUM_COURSES_REC.field_name]

    # output function, either file (if provided) or screen
    output: Callable[[str], None] = __output_func_from_path(output_txt_path)

    # error-logging function, either file (if provided) or screen
    err_log: Callable[[str], None] = __output_func_from_path(err_log_path)

    # delete JSON output file if already exists, to overwrite it
    utils.delete_file_if_exists(output_json_path)

    # construct new recommendation system with default predictor
    rec_sys: RecSys = RecSys(
        chance_predictor=predictor.get_predictor(),
        actions_utility_cache_name=ACTIONS_UTILITY_CACHE_NAME,
        courses_utility_cache_name=COURSESS_UTILITY_CACHE_NAME
    )

    if input_csv_path is None: # no input file - single student input (CLI/interactive)

        # collect missing input interactively
        student_dict, = cli.collect_missing_user_input((cli.STUDENT_INPUT_FIELDS, student_dict))

        student: pd.Series = pd.Series(student_dict)
        __output_single_student(rec_sys, student, 
                                num_actions_rec, num_courses_rec, 
                                output, output_json_path, err_log)

    else: # CSV input file - multiple students
        
        if not utils.file_exists(input_csv_path):
            utils.exit_with_error(f"No such file:\"{input_csv_path}\"", err_log)
        students_df: pd.DataFrame = pd.read_csv(input_csv_path).drop(columns=["Target"])
        __output_multiple_students(rec_sys, students_df, 
                                   num_actions_rec, num_courses_rec, 
                                   output, output_json_path, err_log)



def recommend(student_dict: dict, num_actions_rec: int, num_courses_rec: int,
              rec_sys: Optional[RecSys] = None) -> Tuple[float, RecList, RecList]:
    """
    Makes recommendations for a single student.
    :param student_dict: Student features provided as a dictionary.
    :param num_actions_rec: Amount of actions to recommend (at most).
    :param num_courses_rec: Amount of courses to recommend (at most).
    :param rec_sys: Recommendation system to use. if not provided, default will be used.
    :return: Original estimated chance of graduation,
             List of recommended actions and their estimated improvement rate,
             List of recommended courses and their estimated improvement rate.
    """
    if rec_sys is None:
        rec_sys = RecSys(
           chance_predictor=predictor.get_predictor(),
            actions_utility_cache_name=ACTIONS_UTILITY_CACHE_NAME,
            courses_utility_cache_name=COURSESS_UTILITY_CACHE_NAME
        )
    student: pd.Series = pd.Series(student_dict)
    return __make_recommendation(rec_sys, student, num_actions_rec, num_courses_rec)


def __frac_to_percent(frac: float) -> float:
    """
    Converts a faction to percentage.
    :param frac: Fraction to convert to percentage.
    :return: `frac` as percentage.
    """
    return round(frac * 100, 4)


def __make_recommendation(rec_sys: RecSys,
                          student: pd.Series, 
                          num_actions_rec: int, 
                          num_courses_rec: int) -> Tuple[float, RecList, RecList]:
    """
    Makes recommendations for a single student.
    :param rec_sys: Recommendation system instance to use.
    :param student: Student's representative vector (series).
    :param num_actions_rec: Amount of actions to recommend (at most).
    :param num_courses_rec: Amount of courses to recommend (at most).
    :return: Original estimated chance of graduation,
             List of action recommendations (each is description, improve percentage),
             List of course recommendations (each is description, improve percentage).
    """
    # calculate original estimated success chance (in percentage)
    org_chance: float = __frac_to_percent(rec_sys.predict_chance(student))

    # retreive recommendations from recommendation system
    rec_actions, rec_courses = rec_sys.recommend(student,
                                                 actions_rec_count=num_actions_rec,
                                                 courses_rec_count=num_courses_rec)
    
    # convert recommendations to descriptions and percentages
    res_rec_actions: RecList = list()
    res_rec_courses: RecList = list()
    for action, improvement_rate in rec_actions:
        res_rec_actions.append((str(action), __frac_to_percent(improvement_rate)))
    for course, improvement_rate in rec_courses:
        res_rec_courses.append((str(course), __frac_to_percent(improvement_rate)))

    # return calculated results
    return org_chance, res_rec_actions, res_rec_courses


def __output_single_student(rec_sys: RecSys, student: pd.Series, num_actions_rec: int, num_courses_rec: int,
                            output: Callable[[str], None], output_json_path: Optional[str],
                            err_log: Callable[[str], None]) -> None:
    """
    Outputs action recommentations and courses for a single student.
    :param rec_sys: Recommendation system instance to use.
    :param student: Student's representative vector (series)
    :param num_actions_rec: Amount of actions to recommend (at most).
    :param num_courses_rec: Amount of courses to recommend (at most).
    :param output: An output function to use for outputing recommendations.
    :param output_json_path: A JSON file path for JSON output, `None` if irrelevant.
    :param err_log: An output function to use for logging errors.
    """
    try: # try making recommendation, if failed exit with error
        org_chance, rec_actions, rec_courses = __make_recommendation(rec_sys, student, 
                                                                     num_actions_rec,
                                                                     num_courses_rec)
    except RecommendationException as e:
        utils.exit_with_error(e.message, err_log, e.code)
    except Exception as e:
        utils.exit_with_error(str(e), err_log)
    
    output(f"Current chance of graduation: {org_chance}%\n")
    
    output("Recommended actions:")
    for action, improvement_rate in rec_actions:
        output(f"{action}, Improvement rate: {improvement_rate}%")
    output("")

    output("Recommended courses:")
    for course, improvement_rate in rec_courses:
        output(f"{course}, Improvement rate: {improvement_rate}%")
    output("")

    # output JSON if required
    utils.append_output_json(output_json_path, {
        "OrgChance": org_chance,
        "ActionsRec": rec_actions,
        "CoursesRec": rec_courses
    })


def __output_multiple_students(rec_sys: RecSys, students: pd.DataFrame,
                               num_actions_rec: int, num_courses_rec: int,
                               output: Callable[[str], None], output_json_path: Optional[str],
                               err_log: Callable[[str], None]) -> None:
    """
    Outputs action recommendations and courses for multiple students.
    :param rec_sys: Recommendation system instance to use.
    :param students: Students' dataframe.
    :param num_actions_rec: Amount of actions to recommend for each student (at most).
    :param num_courses_rec: Amount of courses to recommend for each student (at most).
    :param output: An output function to use for outputing recommendations.
    :param output_json_path: A JSON file path for JSON output, `None` if irrelevant.
    :param err_log: An output function to use for logging errors.
    """
    utils.append_output(output_json_path, "[") # if requires JSON output, begin list

    # for each student, output its index and recommendation results
    last_index = len(students) - 1
    for i, ind in enumerate(students.index, 1):
        output(f"Student {i}:")
        __output_single_student(rec_sys, students.loc[ind], 
                                num_actions_rec, num_courses_rec, 
                                output, output_json_path, err_log)
        output("")

        if i < last_index:
            utils.append_output(output_json_path, ",") # if has more elements, append comma to JSON output
    
    utils.append_output(output_json_path, "]") # close list


def __output_func_from_path(path: Optional[str]) -> Callable[[str], None]:
    """
    Given a file path, returns an output function to that file.
    :param path: Path of file to output for, `None` for stdout.
    :return: A function that receives an `str` and outputs it to `path` (or stdout).
    """
    if path is None:
        return lambda text: print(text)
    
    utils.delete_file_if_exists(path)
    def func(text: str) -> None:
        with open(path, "a") as file:
            file.write(text + "\n")
    return func
    

if __name__ == "__main__":
    main()
