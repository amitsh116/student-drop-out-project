"""
Tests for CLI module.
"""


from typing import List, Iterator
from pytest import MonkeyPatch
import recommendation


def __run_cli_test_with(monkeypatch: MonkeyPatch, args_str: str, interactive_inputs: List[str]) -> None:
    args: List[str] = args_str.split()
    input_iter: Iterator[str] = iter(interactive_inputs)
    monkeypatch.setattr("sys.argv", ["recommendation.py"] + args)
    monkeypatch.setattr("builtins.input", lambda _: next(input_iter))
    recommendation.main()


def test_case_1(monkeypatch: MonkeyPatch) -> None:
    args_str: str = """--marital-status 1 
                       --application-mode 2 
                       --application-order 0 
                       --course 33 
                       --attendance 1 
                       --prev-qualification 1 
                       --prev-qualification-grade 160 
                       --nationality 1 
                       --mother-qualification 3 
                       --father-qualification 2 
                       --mother-occupation 2 
                       --father-occupation 1 
                       --admission-grade 170 
                       --displaced 0 
                       --educational-needs 0 
                       --debtor 0 
                       --tuition-up-to-date 1 
                       --gender 1 
                       --scholarship 0 
                       --age 18 
                       --international 0 
                       --cu1st-sem-credited 0 
                       --cu1st-sem-enrolled 5 
                       --cu1st-sem-evaluations 5 
                       --cu1st-sem-approved 4 
                       --cu1st-sem-grade 14 
                       --cu1st-sem-without-eval 0 
                       --cu2nd-sem-credited 0 
                       --cu2nd-sem-enrolled 5 
                       --cu2nd-sem-evaluations 5 
                       --cu2nd-sem-approved 5 
                       --cu2nd-sem-grade 16 
                       --cu2nd-sem-without-eval 0 
                       --unemployment-rate 7.1 
                       --inflation-rate 1.5 
                       --gdp 21000 
                       --actions-rec-count 5 
                       --courses-rec-count 5"""
    interactive_inputs: List[str] = []
    __run_cli_test_with(monkeypatch, args_str, interactive_inputs)


def test_case_2(monkeypatch: MonkeyPatch) -> None:
    args_str: str = ""
    interactive_inputs: List[str] = [
        "2", # Marital Status
        "17", # Application mode
        "1", # Application order
        "9003", # Course
        "0", # Daytime/evening attendance
        "3", # Previous qualification
        "150", # Previous qualification (grade)
        "1", # Nationality
        "1", # Mother's qualification
        "1", # Father's qualification
        "5", # Mother's occupation
        "7", # Father's occupation
        "155", # Admission grade
        "1", # Displaced (1-yes", 0-no)
        "0", # Educational special needs (1-yes", 0-no)
        "0", # Debtor (1-yes", 0-no)
        "1", # Tuition fees up to date (1-yes", 0-no)
        "0", # Gender (1-male", 0-female)
        "1", # Scholarship holder (1-yes", 0-no)
        "19", # Age at enrollment
        "0", # International (1-yes", 0-no)
        "0", # Curricular units 1st sem (credited)
        "6", # Curricular units 1st sem (enrolled)
        "6", # Curricular units 1st sem (evaluations)
        "5", # Curricular units 1st sem (approved)
        "13", # Curricular units 1st sem (grade)
        "0", # Curricular units 1st sem (without evaluations)
        "0", # Curricular units 2nd sem (credited)
        "6", # Curricular units 2nd sem (enrolled)
        "6", # Curricular units 2nd sem (evaluations)
        "6", # Curricular units 2nd sem (approved)
        "15", # Curricular units 2nd sem (grade)
        "0", # Curricular units 2nd sem (without evaluations)
        "6.8", # Unemployment rate
        "1.3", # Inflation rate
        "20000" # GDP
    ]
    __run_cli_test_with(monkeypatch, args_str, interactive_inputs)


def test_case_3(monkeypatch: MonkeyPatch) -> None:
    args_str: str = "--marital-status 3 --course 171 --age 20 --gdp 22000"
    interactive_inputs: List[str] = [
        "18", #Application mode
        "2", #Application order
        "1", #Daytime/evening attendance
        "2", #Previous qualification
        "140", #Previous qualification (grade)
        "1", #Nationality
        "2", #Mother's qualification
        "2", #Father's qualification
        "3", #Mother's occupation
        "3", #Father's occupation
        "145", #Admission grade
        "0", #Displaced (1-yes, 0-no)
        "0", #Educational special needs (1-yes, 0-no)
        "0", #Debtor (1-yes, 0-no)
        "1", #Tuition fees up to date (1-yes, 0-no)
        "1", #Gender (1-male, 0-female)
        "0", #Scholarship holder (1-yes, 0-no)
        "0", #International (1-yes, 0-no)
        "0", #Curricular units 1st sem (credited)
        "5", #Curricular units 1st sem (enrolled)
        "5", #Curricular units 1st sem (evaluations)
        "3", #Curricular units 1st sem (approved)
        "12", #Curricular units 1st sem (grade)
        "0", #Curricular units 1st sem (without evaluations)
        "0", #Curricular units 2nd sem (credited)
        "5", #Curricular units 2nd sem (enrolled)
        "5", #Curricular units 2nd sem (evaluations)
        "4", #Curricular units 2nd sem (approved)
        "13", #Curricular units 2nd sem (grade)
        "0", #Curricular units 2nd sem (without evaluations)
        "7.0", #Unemployment rate
        "1.4" #Inflation rate
    ]
    __run_cli_test_with(monkeypatch, args_str, interactive_inputs)


def test_case_4(monkeypatch: MonkeyPatch) -> None:
    args_str: str = """--marital-status 4
                       --application-mode 1
                       --application-order 0
                       --course 9085
                       --attendance 1
                       --prev-qualification 1
                       --prev-qualification-grade 150
                       --nationality 1
                       --mother-qualification 1
                       --father-qualification 1
                       --mother-occupation 5
                       --father-occupation 5
                       --displaced 0
                       --educational-needs 0
                       --debtor 0
                       --tuition-up-to-date 1
                       --gender 0
                       --scholarship 1
                       --age 18
                       --international 0
                       --unemployment-rate 6.5
                       --inflation-rate 1.2
                       --gdp 20500
                       --actions-rec-count 5
                       --courses-rec-count 5"""
    interactive_inputs: List[str] = [
        "160", # Admission grade
        "0", # Curricular units 1st sem (credited)
        "5", # Curricular units 1st sem (enrolled)
        "5", # Curricular units 1st sem (evaluations)
        "4", # Curricular units 1st sem (approved)
        "14", # Curricular units 1st sem (grade)
        "0", # Curricular units 1st sem (without evaluations)
        "0", # Curricular units 2nd sem (credited)
        "5", # Curricular units 2nd sem (enrolled)
        "5", # Curricular units 2nd sem (evaluations)
        "5", # Curricular units 2nd sem (approved)
        "15", # Curricular units 2nd sem (grade)
        "0" # Curricular units 2nd sem (without evaluations)
    ]
    __run_cli_test_with(monkeypatch, args_str, interactive_inputs)


def test_case_5(monkeypatch: MonkeyPatch) -> None:
    args_str: str = "--course 9254 --attendance 1 --age 19"
    interactive_inputs: List[str] = [
        "1", # Marital Status
        "1", # Application mode
        "0", # Application order
        "1", # Previous qualification
        "140", # Previous qualification (grade)
        "1", # Nationality
        "2", # Mother's qualification
        "2", # Father's qualification
        "2", # Mother's occupation
        "2", # Father's occupation
        "150", # Admission grade
        "0", # Displaced (1-yes, 0-no)
        "0", # Educational special needs (1-yes, 0-no)
        "0", # Debtor (1-yes, 0-no)
        "1", # Tuition fees up to date (1-yes, 0-no)
        "1", # Gender (1-male, 0-female)
        "0", # Scholarship holder (1-yes, 0-no)
        "0", # International (1-yes, 0-no)
        "0", # Curricular units 1st sem (credited)
        "5", # Curricular units 1st sem (enrolled)
        "5", # Curricular units 1st sem (evaluations)
        "3", # Curricular units 1st sem (approved)
        "11", # Curricular units 1st sem (grade)
        "0", # Curricular units 1st sem (without evaluations)
        "0", # Curricular units 2nd sem (credited)
        "5", # Curricular units 2nd sem (enrolled)
        "5", # Curricular units 2nd sem (evaluations)
        "4", # Curricular units 2nd sem (approved)
        "12", # Curricular units 2nd sem (grade)
        "0", # Curricular units 2nd sem (without evaluations)
        "7.2", # Unemployment rate
        "1.3", # Inflation rate
        "21000" # GDP
    ]
    __run_cli_test_with(monkeypatch, args_str, interactive_inputs)


def test_case_6(monkeypatch: MonkeyPatch) -> None:
    args_str: str = "--actions-rec-count 10 --courses-rec-count 8"
    interactive_inputs: List[str] = [
        "1", # Marital Status
        "1", # Application mode
        "0", # Application order
        "9130", # Course
        "1", # Daytime/evening attendance
        "1", # Previous qualification
        "145", # Previous qualification (grade)
        "1", # Nationality
        "1", # Mother's qualification
        "1", # Father's qualification
        "5", # Mother's occupation
        "6", # Father's occupation
        "155", # Admission grade
        "0", # Displaced (1-yes, 0-no)
        "0", # Educational special needs (1-yes, 0-no)
        "0", # Debtor (1-yes, 0-no)
        "1", # Tuition fees up to date (1-yes, 0-no)
        "1", # Gender (1-male, 0-female)
        "0", # Scholarship holder (1-yes, 0-no)
        "18", # Age at enrollment
        "0", # International (1-yes, 0-no)
        "0", # Curricular units 1st sem (credited)
        "5", # Curricular units 1st sem (enrolled)
        "5", # Curricular units 1st sem (evaluations)
        "5", # Curricular units 1st sem (approved)
        "16", # Curricular units 1st sem (grade)
        "0", # Curricular units 1st sem (without evaluations)
        "0", # Curricular units 2nd sem (credited)
        "5", # Curricular units 2nd sem (enrolled)
        "5", # Curricular units 2nd sem (evaluations)
        "5", # Curricular units 2nd sem (approved)
        "17", # Curricular units 2nd sem (grade)
        "0", # Curricular units 2nd sem (without evaluations)
        "6.9", # Unemployment rate
        "1.4", # Inflation rate
        "20000" # GDP
    ]
    __run_cli_test_with(monkeypatch, args_str, interactive_inputs)


def test_case_7(monkeypatch: MonkeyPatch) -> None:
    args_str: str = ""
    interactive_inputs: List[str] = [
        "one", # Marital Status
        "1", # Marital Status
        "two", # Application mode
        "1", # Application mode
        "0", # Application order
        "33", # Course
        "1", # Daytime/evening attendance
        "1", # Previous qualification
        "160", # Previous qualification (grade)
        "1", # Nationality
        "abc", # Mother's qualification
        "3", # Mother's qualification
        "2", # Father's qualification
        "2", # Mother's occupation
        "1", # Father's occupation
        "170", # Admission grade
        "yes", # Displaced (1-yes, 0-no)
        "0", # Displaced (1-yes, 0-no)
        "0", # Educational special needs (1-yes, 0-no)
        "0", # Debtor (1-yes, 0-no)
        "1", # Tuition fees up to date (1-yes, 0-no)
        "male", # Gender (1-male, 0-female)
        "1", # Gender (1-male, 0-female)
        "0", # Scholarship holder (1-yes, 0-no)
        "eighteen", # Age at enrollment
        "18", # Age at enrollment
        "0", # International (1-yes, 0-no)
        "0", # Curricular units 1st sem (credited)
        "5", # Curricular units 1st sem (enrolled)
        "5", # Curricular units 1st sem (evaluations)
        "4", # Curricular units 1st sem (approved)
        "14", # Curricular units 1st sem (grade)
        "0", # Curricular units 1st sem (without evaluations)
        "0", # Curricular units 2nd sem (credited)
        "5", # Curricular units 2nd sem (enrolled)
        "5", # Curricular units 2nd sem (evaluations)
        "5", # Curricular units 2nd sem (approved)
        "16", # Curricular units 2nd sem (grade)
        "0", # Curricular units 2nd sem (without evaluations)
        "seven point one", # Unemployment rate
        "7.1", # Unemployment rate
        "1.5", # Inflation rate
        "21000" # GDP
    ]
    __run_cli_test_with(monkeypatch, args_str, interactive_inputs)


def test_case_8(monkeypatch: MonkeyPatch) -> None:
    args_str: str = """--marital-status 6 
                       --application-mode 57 
                       --application-order 9 
                       --course 9991 
                       --attendance 0 
                       --prev-qualification 44 
                       --prev-qualification-grade 0 
                       --nationality 109 
                       --mother-qualification 44 
                       --father-qualification 44 
                       --mother-occupation 195 
                       --father-occupation 195 
                       --admission-grade 0 
                       --displaced 1 
                       --educational-needs 1 
                       --debtor 1 
                       --tuition-up-to-date 0 
                       --gender 0 
                       --scholarship 1 
                       --age 50 
                       --international 1 
                       --cu1st-sem-credited 10 
                       --cu1st-sem-enrolled 0 
                       --cu1st-sem-evaluations 0 
                       --cu1st-sem-approved 0 
                       --cu1st-sem-grade 0 
                       --cu1st-sem-without-eval 0 
                       --cu2nd-sem-credited 10 
                       --cu2nd-sem-enrolled 0 
                       --cu2nd-sem-evaluations 0 
                       --cu2nd-sem-approved 0 
                       --cu2nd-sem-grade 0 
                       --cu2nd-sem-without-eval 0 
                       --unemployment-rate 0.0 
                       --inflation-rate 0.0 
                       --gdp 0 
                       --actions-rec-count 5 
                       --courses-rec-count 5"""
    interactive_inputs: List[str] = []
    __run_cli_test_with(monkeypatch, args_str, interactive_inputs)
