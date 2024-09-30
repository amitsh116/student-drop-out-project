namespace RecommendationGUI.Models.StudentFeatures;

/// <summary>
/// Represents student parent's qualification.
/// </summary>
public class ParentQualification : CategorialBase
{
    /// <summary>
    /// Constructor of `ParentQualification`.
    /// </summary>
    /// <param name="id">Representative numeric ID.</param>
    /// <param name="name">Display name.</param>
    protected ParentQualification(int id, string name) : base(id, name) { }

    public static ParentQualification SecondaryEducation12thYear { get; } = new(1, "Secondary Education - 12th Year of Schooling or Equivalent");
    public static ParentQualification HigherEducationBachelors { get; } = new(2, "Higher Education - Bachelor's Degree");
    public static ParentQualification HigherEducationDegree { get; } = new(3, "Higher Education - Degree");
    public static ParentQualification HigherEducationMasters { get; } = new(4, "Higher Education - Master's");
    public static ParentQualification HigherEducationDoctorate { get; } = new(5, "Higher Education - Doctorate");
    public static ParentQualification HigherEducationInProgress { get; } = new(6, "Frequency of Higher Education");
    public static ParentQualification TwelfthYearNotCompleted { get; } = new(9, "12th Year of Schooling - Not Completed");
    public static ParentQualification EleventhYearNotCompleted { get; } = new(10, "11th Year of Schooling - Not Completed");
    public static ParentQualification SeventhYearOld { get; } = new(11, "7th Year (Old)");
    public static ParentQualification OtherEleventhYear { get; } = new(12, "Other - 11th Year of Schooling");
    public static ParentQualification TenthYear { get; } = new(14, "10th Year of Schooling");
    public static ParentQualification GeneralCommerceCourse { get; } = new(18, "General Commerce Course");
    public static ParentQualification BasicEducationThirdCycle { get; } = new(19, "Basic Education 3rd Cycle (9th/10th/11th Year) or Equivalent");
    public static ParentQualification TechnicalProfessionalCourse { get; } = new(22, "Technical-Professional Course");
    public static ParentQualification SeventhYear { get; } = new(26, "7th Year of Schooling");
    public static ParentQualification SecondCycleHighSchoolCourse { get; } = new(27, "2nd Cycle of the General High School Course");
    public static ParentQualification NinthYearNotCompleted { get; } = new(29, "9th Year of Schooling - Not Completed");
    public static ParentQualification EighthYear { get; } = new(30, "8th Year of Schooling");
    public static ParentQualification Unknown { get; } = new(34, "Unknown");
    public static ParentQualification CantReadOrWrite { get; } = new(35, "Can't Read or Write");
    public static ParentQualification CanReadWithout4thYear { get; } = new(36, "Can Read Without Having a 4th Year of Schooling");
    public static ParentQualification BasicEducationFirstCycle { get; } = new(37, "Basic Education 1st Cycle (4th/5th Year) or Equivalent");
    public static ParentQualification BasicEducationSecondCycle { get; } = new(38, "Basic Education 2nd Cycle (6th/7th/8th Year) or Equivalent");
    public static ParentQualification TechnologicalSpecializationCourse { get; } = new(39, "Technological Specialization Course");
    public static ParentQualification HigherEducationFirstCycleDegree { get; } = new(40, "Higher Education - Degree (1st Cycle)");
    public static ParentQualification SpecializedHigherStudiesCourse { get; } = new(41, "Specialized Higher Studies Course");
    public static ParentQualification ProfessionalHigherTechnicalCourse { get; } = new(42, "Professional Higher Technical Course");
    public static ParentQualification HigherEducationSecondCycleMaster { get; } = new(43, "Higher Education - Master (2nd Cycle)");
    public static ParentQualification HigherEducationThirdCycleDoctorate { get; } = new(44, "Higher Education - Doctorate (3rd Cycle)");
    
    /// <summary>
    /// Default parent qualification option (to display before input).
    /// </summary>
    public static ParentQualification DefaultOption { get; } = HigherEducationBachelors;

    /// <summary>
    /// All parent qualification options.
    /// </summary>
    public static ParentQualification[] AllOptions { get; } =
    [
        SecondaryEducation12thYear, HigherEducationBachelors, HigherEducationDegree, HigherEducationMasters, HigherEducationDoctorate,
        HigherEducationInProgress, TwelfthYearNotCompleted, EleventhYearNotCompleted, SeventhYearOld, OtherEleventhYear,
        TenthYear, GeneralCommerceCourse, BasicEducationThirdCycle, TechnicalProfessionalCourse, SeventhYear, SecondCycleHighSchoolCourse,
        NinthYearNotCompleted, EighthYear, Unknown, CantReadOrWrite, CanReadWithout4thYear, BasicEducationFirstCycle,
        BasicEducationSecondCycle, TechnologicalSpecializationCourse, HigherEducationFirstCycleDegree, SpecializedHigherStudiesCourse,
        ProfessionalHigherTechnicalCourse, HigherEducationSecondCycleMaster, HigherEducationThirdCycleDoctorate
    ];
}
