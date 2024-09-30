using RecommendationGUI.Models;
using System;
using System.Collections.Generic;

namespace RecommendationGUI.BackEnd;

/// <summary>
/// BackEnd interface, states back end API.
/// </summary>
public interface IBackEnd
{
    /// <summary>
    /// Gets recommendations from backend.
    /// </summary>
    /// <param name="student">Student to get recommendations for.</param>
    /// <param name="orgCourse">Origin course to recommend based on.</param>
    /// <param name="numActionsRec">Max count of action recommendations to generate.</param>
    /// <param name="numCoursesRec">Max count of course recommendations to generate.</param>
    /// <returns>
    ///     Original estimated graduation chance, and recommendations of both actions and courses.
    /// </returns>
    /// <exception cref="BackEndException">In case of a backend error.</exception>
    Tuple<float, List<Recommendation>, List<Recommendation>>
        Recommend(Student student, Course orgCourse, int numActionsRec = 5, int numCoursesRec = 5);

    /// <summary>
    /// Gets all existing courses.
    /// </summary>
    /// <returns>A list of all courses.</returns>
    /// <exception cref="BackEndException">In case of a backend error.</exception>
    List<Course> GetAllCourses();
}
