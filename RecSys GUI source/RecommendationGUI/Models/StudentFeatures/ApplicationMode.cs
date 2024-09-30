namespace RecommendationGUI.Models.StudentFeatures;

/// <summary>
/// Represents student's application mode (how student applied for their studies).
/// </summary>
public class ApplicationMode : CategorialBase
{
    /// <summary>
    /// Constructor of `ApplicationMode`.
    /// </summary>
    /// <param name="id">Representative numeric ID.</param>
    /// <param name="name">Display name.</param>
    protected ApplicationMode(int id, string name) : base(id, name) { }

    public static ApplicationMode FirstPhaseGeneralContingent { get; } = new(1, "1st Phase - General Contingent");
    public static ApplicationMode OrdinanceNo61293 { get; } = new(2, "Ordinance NO. 612/93");
    public static ApplicationMode FirstPhaseSpecialContingentAzoresIsland { get; } = new(5, "1st Phase - Special Contingent (Azores Island)");
    public static ApplicationMode HoldersOfOtherHigherCourses { get; } = new(7, "Holders of Other Higher Courses");
    public static ApplicationMode OrdinanceNo854B99 { get; } = new(10, "Ordinance No. 854-B/99");
    public static ApplicationMode InternationalStudentBachelor { get; } = new(15, "International Student (Bachelor)");
    public static ApplicationMode FirstPhaseSpecialContingentMadeiraIsland { get; } = new(16, "1st Phase - Special Contingent (Madeira Island)");
    public static ApplicationMode SecondPhaseGeneralContingent { get; } = new(17, "2nd Phase - General Contingent");
    public static ApplicationMode ThirdPhaseGeneralContingent { get; } = new(18, "3rd Phase - General Contingent");
    public static ApplicationMode OrdinanceNo533A99ItemB2 { get; } = new(26, "Ordinance No. 533-A/99, Item b2) (Different Plan)");
    public static ApplicationMode OrdinanceNo533A99ItemB3 { get; } = new(27, "Ordinance No. 533-A/99, Item b3 (Other Institution)");
    public static ApplicationMode Over23YearsOld { get; } = new(39, "Over 23 Years Old");
    public static ApplicationMode Transfer { get; } = new(42, "Transfer");
    public static ApplicationMode ChangeOfCourse { get; } = new(43, "Change of Course");
    public static ApplicationMode TechnologicalSpecializationDiplomaHolders { get; } = new(44, "Technological Specialization Diploma Holders");
    public static ApplicationMode ChangeOfInstitutionCourse { get; } = new(51, "Change of Institution/Course");
    public static ApplicationMode ShortCycleDiplomaHolders { get; } = new(53, "Short Cycle Diploma Holders");
    public static ApplicationMode ChangeOfInstitutionCourseInternational { get; } = new(57, "Change of Institution/Course (International)");

    /// <summary>
    /// Default application mode option (to display before input).
    /// </summary>
    public static ApplicationMode DefaultOption { get; } = FirstPhaseGeneralContingent;

    /// <summary>
    /// All application mode options.
    /// </summary>
    public static ApplicationMode[] AllOptions { get; } =
    [
        FirstPhaseGeneralContingent, OrdinanceNo61293, FirstPhaseSpecialContingentAzoresIsland,
        HoldersOfOtherHigherCourses, OrdinanceNo854B99, InternationalStudentBachelor,
        FirstPhaseSpecialContingentMadeiraIsland, SecondPhaseGeneralContingent, ThirdPhaseGeneralContingent,
        OrdinanceNo533A99ItemB2, OrdinanceNo533A99ItemB3, Over23YearsOld, Transfer, ChangeOfCourse,
        TechnologicalSpecializationDiplomaHolders, ChangeOfInstitutionCourse, ShortCycleDiplomaHolders,
        ChangeOfInstitutionCourseInternational
    ];
}
