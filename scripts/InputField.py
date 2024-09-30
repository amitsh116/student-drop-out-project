"""
Holds `InputField` class representing a CLI input field.
Used by reocmmendation system CLI.
"""


from typing import TypeVar, Generic, Optional, Type, List
from argparse import ArgumentParser


T = TypeVar('T') # used for generics


class InputField(Generic[T]):
    """
    Represents a single input field.
    """
    def __init__(self, type: Type[T], field_name: str, arg_name: str, choices: Optional[List[T]] = None, 
                 default: Optional[T] = None, description: Optional[str] = None) -> None:
        """
        Initializes input field instance.
        :param type: Type of input field's value.
        :param field_name: Name of field in dataframe or dictionary.
        :param arg_name: Name of CLI arg.
        :param choices: A list of all possible value choises (`None` if no such restriction).
        :param default: Default value for field (`None` if doesn't have such).
        :param description: Description of field, defaults to `data_col`.
        """
        if description is None:
            description = field_name
        self.__type: Type = type
        self.__field_name: str = field_name
        self.__arg_name: str = arg_name
        self.__choices: Optional[List[T]] = choices
        self.__default: Optional[T] = default
        self.__description: str = description
    
    @property
    def type(self) -> Type:
        """
        :return: Type of input field's value.
        """
        return self.__type
    
    @property
    def field_name(self) -> str:
        """
        :return: Name of field in dataframe or dictionary.
        """
        return self.__field_name
    
    @property
    def arg_name(self) -> str:
        """
        :return: Name of CLI arg.
        """
        return self.__arg_name
    
    @property
    def safe_arg_name(self) -> str:
        """
        :return: A naming-safe version of `arg_name`.
        """
        return self.arg_name[2:].replace("-", "_") # cut off "--" from start and replace
    
    @property
    def choices(self) -> Optional[List[T]]:
        """
        :return: All possible value choises (`None` if no such restriction).
        """
        return self.__choices
    
    @property
    def has_default(self) -> bool:
        """
        :return: `True` if has a default value, otherwise `False`.
        """
        return self.__default is not None

    @property
    def default(self) -> T:
        """
        :return: Default value for field (`None` if doesn't have such).
        """
        if self.__default is None:
            raise ValueError("No default value provided at field construction")
        return self.__default
    
    @property
    def description(self) -> str:
        """
        :return: Description of field.
        """
        return self.__description
    
    def add_to_arg_parser(self, parser: ArgumentParser) -> None:
        """
        Adds input field to a given argument parser.
        :param parser: Argument parser to add input field to.
        """
        if not self.has_default:
            parser.add_argument(self.arg_name, type=self.type, help=self.description, choices=self.choices)
        else:
            parser.add_argument(self.arg_name, type=self.type, help=self.description, choices=self.choices, 
                                default=self.default)
    
    def input(self) -> T:
        """
        Inputs field from user.
        :return: Read input.
        """
        res: Optional[T] = self.__try_input()
        while res is None:
            res = self.__try_input("Invalid input. Try again")
        return res
    
    def __try_input(self, msg: Optional[str] = None) -> Optional[T]:
        """
        Inputs field from user.
        :param msg: Message prompt (optional).
        :return: Read input, or `None` if invalid.
        """
        if msg is None:
            msg = self.description
        try:
            res: T = self.type(input(f"{msg}: "))
        except:
            return None
        if self.choices is not None and res not in self.choices:
            return None
        return res
