"""
Manages command-line interface (CLI) utilities.
Used for recommendation system CLI.
"""


from typing import Tuple, List
from argparse import ArgumentParser
from InputField import InputField


STUDENT_INPUT_FIELDS: List[InputField] = [
    InputField(int, "Marital status", "--marital-status"),
    InputField(int, "Application mode", "--application-mode"),
    InputField(int, "Application order", "--application-order"),
    InputField(int, "Course", "--course"),
    InputField(int, "Daytime/evening attendance", "--attendance"),
    InputField(int, "Previous qualification", "--prev-qualification"),
    InputField(int, "Previous qualification (grade)", "--prev-qualification-grade"),
    InputField(int, "Nacionality", "--nationality", description="Nationality"),
    InputField(int, "Mother's qualification", "--mother-qualification"),
    InputField(int, "Father's qualification", "--father-qualification"),
    InputField(int, "Mother's occupation", "--mother-occupation"),
    InputField(int, "Father's occupation", "--father-occupation"),
    InputField(float, "Admission grade", "--admission-grade"),
    InputField(int, "Displaced", "--displaced", choices=[0, 1]),
    InputField(int, "Educational special needs", "--educational-needs", choices=[0, 1]),
    InputField(int, "Debtor", "--debtor", choices=[0, 1]),
    InputField(int, "Tuition fees up to date", "--tuition-up-to-date", choices=[0, 1]),
    InputField(int, "Gender", "--gender", choices=[0, 1]),
    InputField(int, "Scholarship holder", "--scholarship", choices=[0, 1]),
    InputField(int, "Age at enrollment", "--age"),
    InputField(int, "International", "--international", choices=[0, 1]),
    InputField(int, "Curricular units 1st sem (credited)", "--cu1st-sem-credited"),
    InputField(int, "Curricular units 1st sem (enrolled)", "--cu1st-sem-enrolled"),
    InputField(int, "Curricular units 1st sem (evaluations)", "--cu1st-sem-evaluations"),
    InputField(int, "Curricular units 1st sem (approved)", "--cu1st-sem-approved"),
    InputField(int, "Curricular units 1st sem (grade)", "--cu1st-sem-grade"),
    InputField(int, "Curricular units 1st sem (without evaluations)", "--cu1st-sem-without-eval"),
    InputField(int, "Curricular units 2nd sem (credited)", "--cu2nd-sem-credited"),
    InputField(int, "Curricular units 2nd sem (enrolled)", "--cu2nd-sem-enrolled"),
    InputField(int, "Curricular units 2nd sem (evaluations)", "--cu2nd-sem-evaluations"),
    InputField(int, "Curricular units 2nd sem (approved)", "--cu2nd-sem-approved"),
    InputField(int, "Curricular units 2nd sem (grade)", "--cu2nd-sem-grade"),
    InputField(int, "Curricular units 2nd sem (without evaluations)", "--cu2nd-sem-without-eval"),
    InputField(float, "Unemployment rate", "--unemployment-rate"),
    InputField(float, "Inflation rate", "--inflation-rate"),
    InputField(float, "GDP", "--gdp")
]


def parse_args(*args: List[InputField]) -> Tuple[dict, ...]:
    """
    Parses command-line arguments for each input field in provided input fields.
    :param args: List(s) of InputField objects to add to the argument parser.
    :return: Parsed arguments as a dictionary (for each input fields list).
    """
    parser: ArgumentParser = ArgumentParser(description="Student Graduation Recommendation System CLI")
    for fields in args:
        for field in fields:
            field.add_to_arg_parser(parser)
    
    parsed: dict = vars(parser.parse_args()) # read arguments object as dict
    # if you read read as read you have to re-read read to read it as read and not read

    return tuple({field.field_name: parsed[field.safe_arg_name] for field in fields} for fields in args)


def collect_user_input(*args: List[InputField]) -> Tuple[dict, ...]:
    """
    Collects user input for each input field in provided input fields.
    :param args: List(s) of InputField objects to add to read values for.
    :return: Dictionaries of user input data.
    """
    return tuple({field.field_name: field.input() for field in fields} for fields in args)


def collect_missing_user_input(*args: Tuple[List[InputField], dict]) -> Tuple[dict, ...]:
    """
    Collect missing user input for each input field in provided input fields.
    :param args: Pair(s) of: - List of InputField objects to read values for.
                             - Dictionary of fields already read.
    :return: Dictionaries of user input data.
    """
    res: List[dict] = list()
    for fields, read_values in args:
        values: dict = read_values.copy()
        for field in fields:
            if field.field_name not in values.keys() or values[field.field_name] is None:
                if field.has_default:
                    values[field.field_name] = field.default
                else:
                    values[field.field_name] = field.input()
        res.append(values)
    return tuple(res)


def parse_args_collect_missing(*args: List[InputField]) -> Tuple[dict, ...]:
    """
    Parses command-line arguments for each input field in provided input fields, reads missing values.
    :param args: List(s) of InputField objects to add to the argument parser.
    :return: Parsed arguments as a dictionary (for each input fields list).
    """
    return collect_missing_user_input(*zip(args, parse_args(*args)))
