"""
Holds utilities for getting all available courses.
Used by recommendation system (CLI, GUI).
"""


from typing import Optional, Tuple, List
from Course import Course, CourseEncoder
import src_data
import utils
import cli


FIELD_OUTPUT_JSON: cli.InputField = cli.InputField(str, "Output JSON", "--output-json")
IO_INPUT_FIELDS: List[cli.InputField] = [FIELD_OUTPUT_JSON]


def main() -> None:
    io_dict, = cli.parse_args(IO_INPUT_FIELDS)
    output_json_path: Optional[str] = io_dict[FIELD_OUTPUT_JSON.field_name]
    utils.append_output_json(output_json_path, {"Courses": get_all_courses()}, CourseEncoder)


def get_all_courses() -> List[Tuple[Course, bool]]:
    """
    Gets all available courses.
    :return: List of (course, is_evening).
    """
    data: src_data.PrimaryData = src_data.PrimaryData.read_csv("../data/cleaned_data.csv")
    return data.get_all_courses()


if __name__ == "__main__":
    main()