"""
Holds source dataset utilities for recommendation system.
"""


from typing import Tuple, List, Dict
from Course import Course
import pandas as pd
import utils


# column of course ID in courses DF
C_COURSE_ID: str = "Course"

# column of course name in courses DF
C_COURSE_NAME = "Course Name"

# column of course time in courses DF
C_COURSE_TIME: str = "Daytime/evening attendance"

# representative integer for night course
V_NIGHT: int = 0

# representative integer for day course
V_DAY: int = 1

# column of course time in students DF
C_STUD_COURSE_TIME: str = C_COURSE_TIME

# column of curricular units in 1st semester
C_STUD_UNITS_SEM1: str = "Curricular units 1st sem (credited)"

# column of curricular units in 2nd semester
C_STUD_UNITS_SEM2: str = "Curricular units 2nd sem (credited)"

# column of course that student took
C_STUD_COURSE: str = C_COURSE_ID


# maps each course number to course name, taken from description of dataframe
_COURSES: Dict[int, str] = {
    33: "Biofuel Production Technologies",
    171: "Animation and Multimedia Design",
    8014: "Social Service",
    9003: "Agronomy",
    9070: "Communication Design",
    9085: "Veterinary Nursing",
    9119: "Informatics Engineering",
    9130: "Equinculture",
    9147: "Management",
    9238: "Social Service",
    9254: "Tourism",
    9500: "Nursing",
    9556: "Oral Hygiene",
    9670: "Advertising and Marketing Management",
    9773: "Journalism and Communication",
    9853: "Basic Education",
    9991: "Management"
}


class PrimaryData:
    """
    Base data used for calculations
    """
    def __init__(self, df: pd.DataFrame) -> None:
        """
        :param df: Base dataframe.
        """
        self.__df = df

    @staticmethod
    def read_csv(path: str) -> 'PrimaryData':
        """
        Loads primary data from CSV.
        :param path: CSV path.
        """
        return PrimaryData(pd.read_csv(path))
    
    def get_students_df_and_labels(self) -> Tuple[pd.DataFrame, pd.Series]:
        """
        Gets students' dataframe and target labels.
        :return: Students' dataframe and target labels.
        """
        return self.__df.drop(columns=["Target"]), self.__df["Target"]

    def get_graduates_df(self) -> pd.DataFrame:
        """
        Gets dataframe of graduates.
        :param primary_data: Primary dataset.
        :return: Dataframe of graduate.
        """
        def gen_func() -> pd.DataFrame:
            return self.__df[self.__df["Target"] == "Graduate"].drop(columns=["Target"])
        return utils.get_cached_dataframe(gen_func, "students_data")
    
    def get_courses_df(self) -> pd.DataFrame:
        """
        Gets dataframe of courses.
        :param primary_data: Primary dataset.
        :return: Dataframe of courses.
        """
        def gen_func() -> pd.DataFrame:
            courses_df: pd.DataFrame = self.__df[["Course", "Daytime/evening attendance"]].drop_duplicates()
            courses_df[C_COURSE_NAME] = courses_df["Course"].apply(lambda id: _COURSES[id])
            return courses_df
        return utils.get_cached_dataframe(gen_func, "courses_data")
    
    def get_all_courses(self) -> List[Tuple[Course, bool]]:
        """
        Gets all available courses.
        :return: List of (course, is_evening).
        """
        courses_df: pd.DataFrame = self.get_courses_df()
        return [
            (
                Course(id=int(course[C_COURSE_ID]), name=str(course[C_COURSE_NAME])),
                int(course[C_COURSE_TIME]) == V_NIGHT
            )
            for _, course in courses_df.iterrows()
        ]
