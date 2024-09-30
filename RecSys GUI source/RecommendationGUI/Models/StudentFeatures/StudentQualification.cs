namespace RecommendationGUI.Models.StudentFeatures;

/// <summary>
/// Represents student's qualification.
/// </summary>
public class StudentQualification : CategorialBase
{
    /// <summary>
    /// Constructor of `StudentQualification`.
    /// </summary>
    /// <param name="id">Representative numeric ID.</param>
    /// <param name="name">Display name.</param>
    protected StudentQualification(int id, string name) : base(id, name) { }

    public static StudentQualification SecondaryEducation { get; } = new(1, "Secondary Education");
    public static StudentQualification HigherEducationBachelors { get; } = new(2, "Higher Education - Bachelor's Degree");
    public static StudentQualification HigherEducationDegree { get; } = new(3, "Higher Education - Degree");
    public static StudentQualification HigherEducationMasters { get; } = new(4, "Higher Education - Master's");
    public static StudentQualification HigherEducationDoctorate { get; } = new(5, "Higher Education - Doctorate");
    public static StudentQualification HigherEducationInProgress { get; } = new(6, "Frequency of Higher Education");
    public static StudentQualification TwelfthYearNotCompleted { get; } = new(9, "12th Year of Schooling - Not Completed");
    public static StudentQualification EleventhYearNotCompleted { get; } = new(10, "11th Year of Schooling - Not Completed");
    public static StudentQualification OtherEleventhYear { get; } = new(12, "Other - 11th Year of Schooling");
    public static StudentQualification TenthYear { get; } = new(14, "10th Year of Schooling");
    public static StudentQualification TenthYearNotCompleted { get; } = new(15, "10th Year of Schooling - Not Completed");
    public static StudentQualification BasicEducationThirdCycle { get; } = new(19, "Basic Education 3rd Cycle (9th/10th/11th Year) or Equivalent");
    public static StudentQualification BasicEducationSecondCycle { get; } = new(38, "Basic Education 2nd Cycle (6th/7th/8th Year) or Equivalent");
    public static StudentQualification TechnologicalSpecializationCourse { get; } = new(39, "Technological Specialization Course");
    public static StudentQualification HigherEducationFirstCycleDegree { get; } = new(40, "Higher Education - Degree (1st Cycle)");
    public static StudentQualification ProfessionalHigherTechnicalCourse { get; } = new(42, "Professional Higher Technical Course");
    public static StudentQualification HigherEducationSecondCycleMaster { get; } = new(43, "Higher Education - Master (2nd Cycle)");

    /// <summary>
    /// Default student qualification option (to display before input).
    /// </summary>
    public static StudentQualification DefaultOption { get; } = SecondaryEducation;

    /// <summary>
    /// All student qualification options.
    /// </summary>
    public static StudentQualification[] AllOptions { get; } =
    [
        SecondaryEducation, HigherEducationBachelors, HigherEducationDegree, HigherEducationMasters, HigherEducationDoctorate,
        HigherEducationInProgress, TwelfthYearNotCompleted, EleventhYearNotCompleted, OtherEleventhYear, TenthYear,
        TenthYearNotCompleted, BasicEducationThirdCycle, BasicEducationSecondCycle, TechnologicalSpecializationCourse,
        HigherEducationFirstCycleDegree, ProfessionalHigherTechnicalCourse, HigherEducationSecondCycleMaster
    ];
}
