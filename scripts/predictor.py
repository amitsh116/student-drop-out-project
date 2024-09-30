"""
Holds predictor of student's graduation chance.
Used by recommendation system.
"""


import pandas as pd
from sklearn.metrics import accuracy_score, classification_report
from sklearn.model_selection import train_test_split
from sklearn.ensemble import RandomForestClassifier
from typing import Callable, Optional


def __train_predictor_model() -> RandomForestClassifier:
    """
    Trains student graduation chance predictor.
    :return: Trained predictor.
    """
    print("\nPredictor training started\n")

    file_path = "../data/cleaned_data.csv"
    # Assume 'X' is the subset of data with relevant academic features
    data = pd.read_csv(file_path)

    # Automatically get all columns from the dataset (ignoring the target column for now)
    columns = list(data.columns)
    target_column = 'Target'  # Assuming the target column is named 'Target'
    if target_column in columns:
        columns.remove(target_column)
    X = data.dropna()


    # Select the target and features
    data = data.dropna(subset=['Target'])
    data = data[data['Target']!='Enrolled']
    y = data['Target'].map({'Dropout': 1, 'Graduate': 0})

    X = data.drop(columns=['Target'])

    x_train, x_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)

    # Train a Random Forest model
    rf = RandomForestClassifier(random_state=42)
    rf.fit(x_train, y_train)

    y_pred = rf.predict(x_test)

    print("Accuracy:", accuracy_score(y_test, y_pred))
    print("\nClassification Report:\n", classification_report(y_test, y_pred))

    print("\nPredictor training ended\n")
    return rf


def get_predictor() -> Callable[[pd.Series], float]:
    """
    Gets a student graduation chance predictor, which is trained on first predict call.
    :return: Trained predictor, in the form of a callable.
    """
    model: Optional[RandomForestClassifier] = None
    def func(student: pd.Series) -> float:
        nonlocal model
        if model is None:
            model = __train_predictor_model()
        return float(model.predict_proba(pd.DataFrame([student]))[0, 1])
    return func
