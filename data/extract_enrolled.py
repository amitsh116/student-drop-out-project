# used for extracting students with "enrolled" label (neither "graduate" or "dropout")

import pandas as pd

def main() -> None:
    file_path: str = "./cleaned_data.csv"
    output_path: str = "./enrolled.csv"

    # load cleaned data
    df: pd.DataFrame = pd.read_csv(file_path)

    # filter only students with "enrolled" label
    df = df[df["Target"] == "Enrolled"]

    df.to_csv(output_path, index=False)

if __name__ == "__main__":
    main()