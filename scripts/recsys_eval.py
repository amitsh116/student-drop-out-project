from typing import Callable
from RecSys import RecSys
from Course import Course
import pandas as pd
import predictor
import baseline
import src_data
import utils


def main() -> None:
    baseline_predictor: Callable[[pd.Series], float] = baseline.get_predictor()
    final_predictor: Callable[[pd.Series], float] = predictor.get_predictor()

    print("\nRunning estimations. This may take about 20 minutes.\n")

    eval_rec_sys_accuracies(baseline_predictor=baseline_predictor,
                            final_predictor=final_predictor)


def eval_rec_sys_accuracies(baseline_predictor: Callable[[pd.Series], float],
                            final_predictor: Callable[[pd.Series], float]) -> None:
    """
    Evaluates accuracy of recommendation system with both the baseline predictor and the "final" one.
    Prints results to the screen.
    :param basline_predictor: Baseline student gradutaion chance predictor, in the form of a callable.
    :param final_predictor: "Final" student graduation chance predictor, in the form of a callable.
    """
    unlabeled_df: pd.DataFrame = pd.read_csv("../data/enrolled.csv").drop(columns=["Target"])
    
    baseline_rec_sys: RecSys = RecSys(baseline_predictor)
    final_rec_sys: RecSys = RecSys(final_predictor)

    baseline_accuracy: float = calc_rec_sys_accuracy(baseline_rec_sys, unlabeled_df)
    print(f"Baseline recommendation system accuracy compared to predictor is {100 * baseline_accuracy}%")
    final_accuracy: float = calc_rec_sys_accuracy(final_rec_sys, unlabeled_df)
    print(f"Final recommendation system accuracy compared to predictor is {100 * final_accuracy}%")


def calc_rec_sys_accuracy(rec_sys: RecSys, students_df: pd.DataFrame) -> float:
    """
    Evaluates a recommendation system's accuracy.
    :param rec_sys: Recommendation system to evaluate its accuracy.
    :param: students_df: A dataframe of students to evaluate accuracy based on.
    :return: Evaluated accuracy.
    """
    errs = students_df.apply(lambda student: calc_student_err(rec_sys, student), axis=1)
    avg_err = 0.0 if errs.empty else errs.mean()
    return round(1 - avg_err, 2) # accuracy is 1 minus overall error


def calc_student_err(rec_sys: RecSys, student: pd.Series) -> float:
    """
    Calculates recommendation error for a single student.
    :param rec_sys: Recommendation system to calculate error on.
    :param student: Student's representative features vector.
    :return: Recommendation error (absolute difference between recommendation improvement rate and
             actual improvement rate).
    """
    org_chance: float = rec_sys.predict_chance(student)
    action_recs, course_recs = rec_sys.recommend(student)
    
    action_errs = [
        abs(improve_rate - utils.calc_improve(org_chance, rec_sys.predict_chance(action.apply(student))))
        for action, improve_rate in action_recs
    ]

    course_errs = [
        abs(improve_rate - utils.calc_improve(org_chance, rec_sys.predict_chance(__apply_course(student, course))))
        for course, improve_rate in course_recs
    ]

    total_err: float = sum(action_errs) + sum(course_errs)
    total_recs: float = len(action_recs) + len(course_recs)

    return 0 if 0 == total_recs else total_err / total_recs


def __apply_course(student: pd.Series, course: Course) -> pd.Series:
    """
    Applies course on student.
    :param student: Student to apply course on.
    :param course: Course to apply on student.
    :return: `student`, but enrolled at `course`.
    """
    student[src_data.C_STUD_COURSE] = course.id
    return student


if __name__ == "__main__":
    main()
