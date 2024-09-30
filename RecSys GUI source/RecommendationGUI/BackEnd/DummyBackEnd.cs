using RecommendationGUI.Models;
using RecommendationGUI.Models.StudentFeatures;
using System;
using System.Collections.Generic;

namespace RecommendationGUI.BackEnd;

/// <summary>
/// Dummy implementation of IBackEnd.
/// </summary>
public class DummyBackEnd : IBackEnd
{
    public List<Course> GetAllCourses()
    {
        return [
            new(id: 1, name: "Some Course", time: DaytimeEveningAttendance.Daytime),
            new(id: 2, name: "Other Course", time: DaytimeEveningAttendance.Evening),
            new(id: 3, name: "Gablpmoo", time: DaytimeEveningAttendance.Daytime)
        ];
    }

    public Tuple<float, List<Recommendation>, List<Recommendation>>
        Recommend(Student student, Course orgCourse, int numActionsRec = 5, int numCoursesRec = 5)
    {
        float orgChance = 80;
        Course course = new(id: 3, name: "Gablpmoo", time: DaytimeEveningAttendance.Daytime);
        List<Recommendation> actions = [new("Remove 1 course", 15), new("Add night courses", 5)];
        List<Recommendation> courses = [new("Some Course (1)", 17), new("Another Course", 7)];
        return Tuple.Create(orgChance, actions, courses);
    }
}
