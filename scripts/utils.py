"""
Holds various utilities.
Used by recommendation system.
"""


from typing import Callable, Optional, Type, Dict, Any
import pandas as pd
import json
import sys
import os


# represents zero improvement rate
NO_IMPROVE: float = 0.0

# represents full positive improvement
FULL_POS_IMPROVE: float = 1.0

# represents full negative improvement
FULL_NEG_IMPROVE: float = -1.0


def exit_with_error(err_msg: str, err_log: Callable[[str], None], err_code: int = -1):
    """
    Exits the script with error.
    :param err_msg: Error message.
    :param err_log: An output function used for error logging.
    :param err_code: Exit code to use, default is `-1`.
    """
    err_log(err_msg)
    sys.exit(err_code)


def file_exists(path: str) -> bool:
    """
    Checks if a file exists at a given path.
    :param path: Path of file to check if exists.
    :return: `True` if file exists, otherwise `False`.
    """
    return os.path.isfile(path)


def delete_file_if_exists(path: Optional[str]) -> None:
    """
    Makes sure that a file doesn't exist (if it does, deletes it).
    :param path: Path of file to potentially delete, or `None` to ignore.
    """
    if path is None:
        return # ignore
    if file_exists(path):
        os.remove(path)


def create_folder_if_missing(path: Optional[str]) -> None:
    """
    Makes sure that a directory exists (if it doesn't, creates it).
    :param path: Path of directory to potentially create, or `None` to ignore.
    """
    if path is None:
        return # ignore
    if not os.path.exists(path):
        os.makedirs(path)


def append_output(path: Optional[str], data: str) -> None:
    """
    Appends data to an output file.
    :param path: Path of output file, or `None` to ignore.
    :param data: Data to append.
    """
    if path is None:
        return # ignore
    with open(path, "a") as file:
        file.write(data)


def append_output_json(path: Optional[str], data: Dict[str, Any],
                       encoder: Optional[Type[json.JSONEncoder]] = None) -> None:
    """
    Appends data to a JSON output file.
    :param path: Path of output JSON file, or `None` to ignore.
    :param data: Data to output as JSON.
    """
    append_output(path, json.dumps(data, cls=encoder))


def get_cached_dataframe(gen_func: Callable[[], pd.DataFrame], cache_name: str) -> pd.DataFrame:
    """
    Generates a dataframe from primary data, or reads from cache if exists.
    :param gen_func: A function which receives primary data and generates a dataframe.
    :param cache_name: Name of cache file.
    :param primary_data: Primary dataset.
    :return: Generated dataframe.
    """
    create_folder_if_missing("../cache") # create cache folder if missing
    cache_path: str = f"../cache/{cache_name}.pkl"
    if os.path.exists(cache_path):
        return pd.read_pickle(cache_path)
    df: pd.DataFrame = gen_func()
    df.to_pickle(cache_path)
    return df


def calc_improve(org: float, new: float) -> float:
    """
    Calculates improve rate based on given success chances.
    :param org: Original success chance.
    :param new: New success chance.
    :return: Improvement rate.
    """
    return new - org
