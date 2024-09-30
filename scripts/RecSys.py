"""
Holds recommendation system main class.
Used by recommendation system.
"""


from typing import TypeVar, Optional, Callable, Iterable, Tuple, Dict, List
from sklearn.metrics.pairwise import cosine_similarity
from StudentAction import StudentAction
from Course import Course
import pandas as pd
import src_data
import utils


T = TypeVar('T') # used for generics later


# float series definition may change in different environments
FloatSeries = pd.Series
# FloatSeries = pd.Series[float]


# pair of an item and its score
ItemScorePair = Tuple[T, float]

# assumed average curricular units per course, used for estimation
AVG_COURSE_UNITS: int = 4


# path to CSV of default student dataset
DEFAULT_DATASET_PATH: str = "../data/cleaned_data.csv"

# default student similarity function used by recommendation system
DEFAULT_SIMILARITY_FUNC: Callable[[pd.Series, pd.Series], float] = \
        lambda student1, student2: cosine_similarity(
            student1.values.reshape(1, -1), student2.values.reshape(1, -1))[0, 0] # type: ignore


class RecSys:
    """
    Recommendation system of actions and courses.
    """
    def __init__(self, chance_predictor: Callable[[pd.Series], float],
                 primary_data: Optional[src_data.PrimaryData] = None,
                 similarity_func: Callable[[pd.Series, pd.Series], float] = DEFAULT_SIMILARITY_FUNC,
                 k: int = 5,
                 actions_utility_cache_name: Optional[str] = None,
                 courses_utility_cache_name: Optional[str] = None) -> None:
        """
        Initalizes new recommendation sysem.
        :param primary_data: Primary students data.
        :param chance_predictor: A function that receives a student, and predicts their chance of graduating.
        :param similarity_func: Function that calculates similarity between two students.
        :param k: K parameter of CF (take K most similar users).
        :param actions_utility_cache_name: Name to use for actions utility matrix cache, `None` to not use cache.
        :param courses_utility_cache_name: Name to use for courses utility matrix cache, `None` to not use cache.
        """
        if primary_data is None:
            primary_data = src_data.PrimaryData.read_csv(DEFAULT_DATASET_PATH) # use default dataset

        self.__chance_predictor: Callable[[pd.Series], float] = chance_predictor
        self.__similarity_func: Callable[[pd.Series, pd.Series], float] = similarity_func
        self.__k: int = k
        self.__graduates_df: pd.DataFrame = primary_data.get_graduates_df() # DF of graduate students
        self.__courses_df: pd.DataFrame = primary_data.get_courses_df() # DF of courses
        self.__org_chances: FloatSeries = self.__graduates_df.apply(chance_predictor, axis=1) # original predicted graduate chances
        self.__actions_utility_df: pd.DataFrame = RecSys.__get_actions_utility_df(
            actions_utility_cache_name,
            self.__graduates_df, chance_predictor, self.__org_chances
        )
        self.__courses_utility_df: pd.DataFrame = RecSys.__get_courses_utility_df(
            courses_utility_cache_name,
            self.__graduates_df, chance_predictor, self.__org_chances,
            courses=map(int, self.__courses_df[src_data.C_STUD_COURSE])
        )

    def predict_chance(self, student: pd.Series) -> float:
        """
        Predicts student's chances of graduating.
        :param student: Representative student vector (series).
        :return: Predicted graduation chance.
        """
        return self.__chance_predictor(student)

    def __calc_k_similar(self, student: pd.Series) -> Tuple[FloatSeries, pd.Index]:
        """
        Given a representative student vector (series), calculates similarity between said student and all students in
        graduates DF and locates `k` most similar students (same `k` from constructor).
        :param student: Representative student vector (series).
        :return: Similarity scores (`FloatSeries`), indices of similar students in graduates DF.
        """
        similarity_scores: FloatSeries = self.__graduates_df.apply(lambda row: self.__similarity_func(row, student), axis=1)
        similar_indices: pd.Index = similarity_scores.nlargest(self.__k).index
        return similarity_scores, similar_indices
    
    def recommend_actions(self, student: pd.Series, rec_count: int) -> List[ItemScorePair[StudentAction]]:
        """
        Given a representative student vector (series), recommends actions to increase graduation chances.
        :param student: Representative student vector (series).
        :param rec_count: Amount of actions to recommend (max).
        :return: List of recommended actions and their scores.
        """
        similarity_scores, similar_indices = self.__calc_k_similar(student)
        return self.__recommend_actions(rec_count, similarity_scores, similar_indices)
    
    def __recommend_actions(self, rec_count: int,
                            similarity_scores: FloatSeries,
                            similar_indices: pd.Index) -> List[ItemScorePair[StudentAction]]:
        """
        Given a student's similarity to graduates, recommends actions to increase graduation chances.
        :param rec_count: Amount of actions to recommend (max).
        :param similarity_scores: Similarity scores between `student` and every graduate.
        :param similar_indices: Indices of `k` most similar graduates.
        :return: List of recommended actions and said actions' scores.
        """
        action_scores: Dict[StudentAction, float] = RecSys.__predict_scores(
            self.__actions_utility_df, similar_indices, similarity_scores, StudentAction
        )
        return RecSys.__get_items_max_score(action_scores, rec_count)
    
    def recommend_courses(self, student: pd.Series, rec_count: int) -> List[ItemScorePair[Course]]:
        """
        Given a representative student vector (series), recommends courses to increase graduation chances.
        :param student: Representative student vector (series).
        :param rec_count: Amount of courses to recommend (max).
        :return: List of recommended courses and their scores.
        """
        similarity_scores, similar_indices = self.__calc_k_similar(student)
        return self.__recommend_courses(rec_count, similarity_scores, similar_indices)
    
    def __recommend_courses(self, rec_count: int,
                            similarity_scores: FloatSeries,
                            similar_indices: pd.Index) -> List[ItemScorePair[Course]]:
        """
        Given a student's similarity to graduates, recommends coueses to increase graduation chances.
        :param rec_count: Amount of courses to recommend (max).
        :param similarity_scores: Similarity scores between `student` and every graduate.
        :param similar_indices: Indices of `k` most similar graduates.
        :return: List of recommended courses and their scores.
        """
        course_scores: Dict[int, float] = RecSys.__predict_scores(
            self.__courses_utility_df, similar_indices, similarity_scores, int
        )
        recommended_courses: List[ItemScorePair[int]] = RecSys.__get_items_max_score(course_scores, rec_count)
        return [(self.__get_course_by_id(id), score) for id, score in recommended_courses]

    def recommend(self, student: pd.Series,
                  actions_rec_count: int = 5,
                  courses_rec_count: int = 5) -> Tuple[List[ItemScorePair[StudentAction]],
                                                       List[ItemScorePair[Course]]]:
        """
        Given a representative student vector (series), recommends actions and courses to increase graduation chances.
        :param student: Representative student vector (series).
        :param actions_rec_count: Amount of actions to recommend (max).
        :param courses_rec_count: Amount of courses to recommend (max).
        :return: List of recommended actions and their scores, 
                 list of recommended courses and their scores.
        """
        similarity_scores, similar_indices = self.__calc_k_similar(student)

        return self.__recommend_actions(actions_rec_count, similarity_scores, similar_indices), \
               self.__recommend_courses(courses_rec_count, similarity_scores, similar_indices)
    
    def __get_course_by_id(self, course_id: int) -> Course:
        """
        Gets a course by its ID.
        :param course_id: ID of course.
        :return: Course with ID `course_id`.
        """
        row: pd.DataFrame = self.__courses_df[self.__courses_df[src_data.C_COURSE_ID] == course_id]
        if row.empty:
            raise ValueError(f"No course {course_id}")
        assert 1 == len(row) # course ID should be unique

        course_name: str = row[src_data.C_COURSE_NAME].values[0]
        
        return Course(id=course_id, name=course_name)
    
    @staticmethod
    def __get_items_max_score(scores: Dict[T, float], count: int) -> List[ItemScorePair[T]]:
        """
        Given a dictionary of scores, gets `count` items with the highest positive score.
        :param scores: A dictionary of scores.
        :param count: Amount of items to return.
        :return: `count` items with maximum score in `scores` (key and value).
        """
        sorted_by_score: List[Tuple[T, float]] = sorted(scores.items(), key=lambda item: item[1], reverse=True)
        trim_end: int = count
        for i in range(count):
            if sorted_by_score[i][1] <= 0: # if non-positive score
                trim_end = i # trim before non-positive score
                break
        return sorted_by_score[:trim_end]
        
    @staticmethod
    def __predict_scores(utility_df: pd.DataFrame,
                         similar_indices: pd.Index,
                         similarity_scores: FloatSeries,
                         item_type: Callable[[str], T]) -> Dict[T, float]:
        """
        Predicts recommendation score based on utility dataframe.
        :param utility_df: Utility dataframe.
        :param simialr_indices: Indices of users most similar to user we're predicting for.
        :param similarity_scores: Score of similarity between each user and the user we're predicting for.
        :param item_type: Type of recommended item (utility columns).
        :return: A dictionary of item scores.
        """
        total_similarity: float = similarity_scores[similar_indices].sum()
        scores: Dict[T, float] = dict()
        for column in utility_df.columns:
            utility_values: FloatSeries = utility_df.loc[similar_indices, column] # similar graduates utility values
            actions_weighted_sum: float = (similarity_scores[similar_indices] * utility_values).sum()
            scores[item_type(column)] = actions_weighted_sum / total_similarity if 0 != total_similarity else 0
        return scores
    
    @staticmethod
    def __get_actions_utility_df(cache_name: Optional[str],
                                 graduates_df: pd.DataFrame,
                                 chance_predictor: Callable[[pd.Series], float],
                                 org_chances: FloatSeries) -> pd.DataFrame:
        """
        Gets actions utility dataframe - each row represents a student, each column an action.
        :param cache_name: Name to use for cache, `None` to not use cache.
        :param graduates_df: Graduate students dataframe.
        :param chance_predictor: A function that receives a student, and predicts their chance of graduating.
        :param org_chances: Predicted graduation chances for original graduate samples in `graduates_df`.
        :return: Actions utility dataframe.
        """
        def gen_func() -> pd.DataFrame:
            cmp_df: pd.DataFrame = graduates_df.copy()

            def calc_improve_add_course_type(course_time: int) -> FloatSeries:
                # calculate improve effectiveness if adding `course_time` courses (V_DAY or V_NIGHT)
                cmp_df[src_data.C_STUD_COURSE_TIME] = course_time # compare original with course time set to `course_time`
                new_chances: FloatSeries = cmp_df.apply(chance_predictor, axis=1) # chances after adding `course_time` courses
                improvement: FloatSeries = org_chances.combine(new_chances, utils.calc_improve) # calculate improve effectiveness
                improvement[graduates_df[src_data.C_STUD_COURSE_TIME] == course_time] = utils.NO_IMPROVE # if was already `course_time`, no improvement
                return improvement
            add_day_improve: FloatSeries = calc_improve_add_course_type(src_data.V_DAY) # improve effectiveness of adding day courses
            add_night_improve: FloatSeries = calc_improve_add_course_type(src_data.V_NIGHT) # improve effectiveness of adding night courses

            def calc_improve_reduce_units(units: int) -> FloatSeries:
                # calculate improve effectiveness if reducing `units` curricular units
                total_units: pd.Series = (graduates_df[src_data.C_STUD_UNITS_SEM1] + graduates_df[src_data.C_STUD_UNITS_SEM2])
                reduced_df: pd.DataFrame = graduates_df.copy() # DF with reduced units to use for comperation
                # reduce half points from each semester, then compare:
                mask = total_units >= units
                reduced_df.loc[mask, src_data.C_STUD_UNITS_SEM1] = (
                    graduates_df.loc[mask, src_data.C_STUD_UNITS_SEM1] - units / 2
                ).clip(lower=0)
                reduced_df.loc[mask, src_data.C_STUD_UNITS_SEM2] = (
                    graduates_df.loc[mask, src_data.C_STUD_UNITS_SEM2] - units / 2
                ).clip(lower=0)
                new_chances: FloatSeries = reduced_df.apply(chance_predictor, axis=1)
                improvement: FloatSeries = org_chances.combine(new_chances, utils.calc_improve) # calculate improve effectiveness
                improvement[(graduates_df[src_data.C_STUD_UNITS_SEM1] + graduates_df[src_data.C_STUD_UNITS_SEM2])
                            < units] = utils.NO_IMPROVE # if couldn't reduce (not enough units), no improvement
                return improvement
            # for i=1..3, calculate improve effectiveness of reducing i average courses:
            reduce1_improve: FloatSeries = calc_improve_reduce_units(1 * AVG_COURSE_UNITS)
            reduce2_improve: FloatSeries = calc_improve_reduce_units(2 * AVG_COURSE_UNITS)
            reduce3_improve: FloatSeries = calc_improve_reduce_units(3 * AVG_COURSE_UNITS)
            
            return pd.DataFrame({
                StudentAction.ADD_DAY_COURSES.id: add_day_improve,
                StudentAction.ADD_NIGHT_COURSES.id: add_night_improve,
                StudentAction.REDUCE1.id: reduce1_improve,
                StudentAction.REDUCE2.id: reduce2_improve,
                StudentAction.REDUCE3.id: reduce3_improve
            })

        return gen_func() if cache_name is None else utils.get_cached_dataframe(gen_func, cache_name)

    @staticmethod
    def __get_courses_utility_df(cache_name: Optional[str],
                                 graduates_df: pd.DataFrame,
                                 chance_predictor: Callable[[pd.Series], float],
                                 org_chances: FloatSeries,
                                 courses: Iterable[int]) -> pd.DataFrame:
        """
        Gets curses utility dataframe - each row represents a student, each column a course.
        :param cache_name: Name to use for cache, `None` to not use cache.
        :param graduates_df: Graduate students dataframe.
        :param chance_predictor: A function that receives a student, and predicts their chance of graduating.
        :param org_chances: Predicted graduation chances for original graduate samples in `graduates_df`.
        :param courses: All available courses (IDs).
        :return: Courses utility dataframe.
        """
        cmp_df: pd.DataFrame = graduates_df.copy()
        
        def gen_func() -> pd.DataFrame:
            def calc_improve_with_course(course: int) -> FloatSeries:
                # calculate improve effectiveness if enrolling in course `course`
                cmp_df[src_data.C_STUD_COURSE] = course # compare original with course set to `course`
                new_chances: FloatSeries = cmp_df.apply(chance_predictor, axis=1) # chances after enrolling in course `course`
                improvement: FloatSeries = org_chances.combine(new_chances, utils.calc_improve) # calculate improve effectiveness
                improvement[graduates_df[src_data.C_STUD_COURSE] == course] = utils.NO_IMPROVE # if was already `course`, no improvement
                return improvement
            # calculate improvement of each course:
            courses_improve: Dict[int, FloatSeries] = {
                cid: calc_improve_with_course(cid) for cid in courses
            }
            return pd.DataFrame(courses_improve)
        return gen_func() if cache_name is None else utils.get_cached_dataframe(gen_func, cache_name)
