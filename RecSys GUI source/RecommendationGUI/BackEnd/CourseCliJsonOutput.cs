namespace RecommendationGUI.BackEnd;

/// <summary>
/// Used for parsing CLI get_all_courses form JSON.
/// </summary>
internal struct CourseCliJsonOutput
{
    /// <summary>
    /// Course's ID.
    /// </summary>
    public required int ID { get; set; }

    /// <summary>
    /// Course's name.
    /// </summary>
    public required string Name { get; set; }
}
