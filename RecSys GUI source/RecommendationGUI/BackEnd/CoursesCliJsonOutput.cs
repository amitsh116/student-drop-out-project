using System;
using System.Collections.Generic;

namespace RecommendationGUI.BackEnd;

/// <summary>
/// Used for parsing CLI get_all_courses from JSON.
/// </summary>
internal struct CoursesCliJsonOutput
{
    /// <summary>
    /// List of returned courses.
    /// </summary>
    public required List<Tuple<CourseCliJsonOutput, bool>> Courses { get; set; }
}
