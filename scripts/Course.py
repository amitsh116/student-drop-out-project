"""
Holds `Course` class representing a single course.
Used in recommendation system.
"""


from typing import Any
import json


class Course:
    """
    Represents a course.
    """
    def __init__(self, id: int, name: str) -> None:
        """
        Initializes a course instance.
        :param id: Course ID.
        :param name: Course name.
        """
        self.__id: int = id
        self.__name: str = name
    
    @property
    def id(self):
        """
        Course's ID.
        """
        return self.__id
    
    @property
    def name(self):
        """
        Course's name.
        """
        return self.__name
    
    def __str__(self) -> str:
        """
        Gets string representation of course.
        :return: String representation of course.
        """
        return f"({self.id}) {self.name}"
    
    def __repr__(self) -> str:
        """
        Gets debug representation of course.
        :return Debug representation of course.
        """
        return f"Course(id={self.id}, name='{self.name}')"


class CourseEncoder(json.JSONEncoder):
    """
    Custom JSON encoder for Course class.
    """
    def default(self, obj: Any) -> Any:
        if isinstance(obj, Course):
            return {
                "ID": obj.id,
                "Name": obj.name
            }
        return super().default(obj)