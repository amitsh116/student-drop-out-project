"""
Holds `StudentAction` class representing an action a student can take.
Used in recommendation system.
"""


from typing import Callable, Tuple, Dict
import pandas as pd
import src_data


# assumed average curricular units per course, used for estimation
AVG_COURSE_UNITS: int = 4


__ACT_ADD_DAY_COURSES: str = "add day"
__ACT_ADD_NIGHT_COURSES: str = "add night"
__ACT_REDUCE1: str = "reduce1"
__ACT_REDUCE2: str = "reduce2"
__ACT_REDUCE3: str = "reduce3"


def set_student_course_time(student: pd.Series, course_time: int) -> pd.Series:
    student[src_data.C_STUD_COURSE] = course_time
    return student

def reduce_student_units(student: pd.Series, units: int) -> pd.Series:
    total_units: pd.Series = (student[src_data.C_STUD_UNITS_SEM1] + student[src_data.C_STUD_UNITS_SEM2])
    if total_units >= units:
        student.loc[src_data.C_STUD_UNITS_SEM1] = (
            student.loc[src_data.C_STUD_UNITS_SEM1] - units / 2
        )
        student.loc[src_data.C_STUD_UNITS_SEM2] = (
            student.loc[src_data.C_STUD_UNITS_SEM2] - units / 2
        )
    return student


# maps student action ID to description and apply function
_STUDENT_ACTIONS: Dict[str, Tuple[str, Callable[[pd.Series], pd.Series]]] = {
    __ACT_ADD_DAY_COURSES: (
        "Prioritize daytime courses",
        lambda student: set_student_course_time(student, src_data.V_DAY)
    ),
    __ACT_ADD_NIGHT_COURSES: (
        "Prioritize nighttime courses",
        lambda student: set_student_course_time(student, src_data.V_NIGHT)
    ),
    __ACT_REDUCE1: (
        "Reduce one course from next semester",
        lambda student: reduce_student_units(student, AVG_COURSE_UNITS)
    ),
    __ACT_REDUCE2: (
        "Reduce two courses from next semester",
        lambda student: reduce_student_units(student, 2 * AVG_COURSE_UNITS)
    ),
    __ACT_REDUCE3: (
        "Reduce three courses from next semester",
        lambda student: reduce_student_units(student, 3 * AVG_COURSE_UNITS)
    )
}


class StudentAction:
    """
    Represents an action a student can take.
    """
    ADD_DAY_COURSES: 'StudentAction' = None # type: ignore
    ADD_NIGHT_COURSES: 'StudentAction' = None # type: ignore
    REDUCE1: 'StudentAction' = None # type: ignore
    REDUCE2: 'StudentAction' = None # type: ignore
    REDUCE3: 'StudentAction' = None # type: ignore

    def __init__(self, id: str) -> None:
        """
        Initializes a student action instance.
        :param id: ID of action.
        :param description: Action description.
        """
        description, apply = _STUDENT_ACTIONS[id]
        self.__id: str = id
        self.__description: str = description
        self.__apply: Callable[[pd.Series], pd.Series] = apply

    @property
    def id(self) -> str:
        """
        Student action ID, may be used as dataframe column name.
        """
        return self.__id
    
    @property
    def description(self) -> str:
        """
        Student action description.
        """
        return self.__description
    
    def apply(self, student: pd.Series) -> pd.Series:
        """
        Applies action on student.
        :param student: Student to apply action on.
        :return: Student with applied action.
        """
        return self.__apply(student)
    
    def __str__(self) -> str:
        """
        Gets string representation of student action.
        :return: String representation of student action.
        """
        return self.description
    
    def __repr__(self) -> str:
        """
        Gets debug representation of student action.
        :return Debug representation of student action.
        """
        return self.id

StudentAction.ADD_DAY_COURSES = StudentAction(__ACT_ADD_DAY_COURSES)
StudentAction.ADD_NIGHT_COURSES = StudentAction(__ACT_ADD_NIGHT_COURSES)
StudentAction.REDUCE1 = StudentAction(__ACT_REDUCE1)
StudentAction.REDUCE2 = StudentAction(__ACT_REDUCE2)
StudentAction.REDUCE3 = StudentAction(__ACT_REDUCE3)
