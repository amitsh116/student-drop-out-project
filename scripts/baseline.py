"""
Holds baseline predictor of student's graduation chance.
Used for evaluating the recommendation system and its base predictor.
"""


from sklearn.metrics import accuracy_score, classification_report
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LogisticRegression
from typing import Optional, Callable
import pandas as pd
import numpy as np
import src_data


def __train_baseline_model() -> LogisticRegression:
    """
    Trains student graduation chance baseline predictor.
    :return: Trained baseline predictor.
    """
    print("\nBaseline training started\n")

    # retreive data from CSV
    data: src_data.PrimaryData = src_data.PrimaryData.read_csv("../data/cleaned_data.csv")
    X, y = data.get_students_df_and_labels()

    # only work on students we know if graduated or dropped out
    X = X[y != "Enrolled"]
    y = y[y != "Enrolled"]

    # replace text labels with binary ones
    y = y.map({"Graduate": 1, "Dropout": 0})

    # split into train and test for chance estimation model (used for generating utility matrices later)
    X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=435)

    # initialize and train chace estimation model
    model: LogisticRegression = LogisticRegression()
    model.fit(X_train, y_train.to_numpy())

    y_pred = model.predict(X_test)

    # calculate (and print) accuracy and classification report
    print("Accuracy:", accuracy_score(y_test, y_pred))
    print("\nClassification Report:\n", classification_report(y_test, y_pred))

    return model


def get_predictor() -> Callable[[pd.Series], float]:
    """
    Gets a student graduation chance predictor, which is trained on first predict call.
    :return: Trained predictor, in the form of a callable.
    """
    model: Optional[LogisticRegression] = None
    def func(student: pd.Series) -> float:
        nonlocal model
        if model is None:
            model = __train_baseline_model()
        return float(model.predict_proba(pd.DataFrame([student]))[0, 1])
    return func
