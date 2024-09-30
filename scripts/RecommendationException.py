"""
Holds recommendation exception class, used by recommendation system.
"""

class RecommendationException(Exception):
    """Exception thrown on recommendation failure."""
    def __init__(self, message: str, code: int = -1) -> None:
        """
        Initializes recommendation exception instance.
        :param message: Error message.
        :param code: Error code, defaults to `-1`.
        """
        self.__message: str = message
        self.__code: int = code
        super().__init__(message)
    
    @property
    def message(self) -> str:
        """:return: Error message."""
        return self.__message
    
    @property
    def code(self) -> int:
        """:return: Error code."""
        return self.__code
