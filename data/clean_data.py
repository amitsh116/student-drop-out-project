import pandas as pd

def main() -> None:
    file_path: str = "./raw_data.csv"
    output_path: str = "./cleaned_data.csv"

    # load raw data
    df: pd.DataFrame = pd.read_csv(file_path, delimiter=';', header=None)

    # remove whitespaces from cells
    df = df.applymap(lambda x: x.strip() if isinstance(x, str) else x) # type: ignore

    # replace tabs with spaces
    df = df.replace('\t', ' ', regex=True)

    # save cleaned data
    df.to_csv(output_path, index=False)

if __name__ == "__main__":
    main()