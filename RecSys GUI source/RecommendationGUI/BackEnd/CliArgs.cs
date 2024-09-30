namespace RecommendationGUI.BackEnd;

/// <summary>
/// Static class holding CLI argument names.
/// </summary>
public static class CliArgs
{
    public static string OutputJson { get; } = "output-json";

    public static string NumActionsRec { get; } = "actions-rec-count";
    public static string NumCoursesRec { get; } = "courses-rec-count";

    public static string MaritalStatus { get; } = "marital-status";
    public static string ApplicationMode { get; } = "application-mode";
    public static string ApplicationOrder { get; } = "application-order";
    public static string Course { get; } = "course";
    public static string DaytimeEveningAttendance { get; } = "attendance";
    public static string PreviousQualification { get; } = "prev-qualification";
    public static string PreviousQualificationGrade { get; } = "prev-qualification-grade";
    public static string Nationality { get; } = "nationality";
    public static string MotherQualification { get; } = "mother-qualification";
    public static string FatherQualification { get; } = "father-qualification";
    public static string MotherOccupation { get; } = "mother-occupation";
    public static string FatherOccupation { get; } = "father-occupation";
    public static string AdmissionGrade { get; } = "admission-grade";
    public static string Displaced { get; } = "displaced";
    public static string EducationalSpecialNeeds { get; } = "educational-needs";
    public static string Debtor { get; } = "debtor";
    public static string TuitionFeesUpToDate { get; } = "tuition-up-to-date";
    public static string Gender { get; } = "gender";
    public static string ScholarshipHolder { get; } = "scholarship";
    public static string AgeAtEnrollment { get; } = "age";
    public static string International { get; } = "international";
    public static string CurricularUnitsFirstSemCredited { get; } = "cu1st-sem-credited";
    public static string CurricularUnitsFirstSemEnrolled { get; } = "cu1st-sem-enrolled";
    public static string CurricularUnitsFirstSemEvaluations { get; } = "cu1st-sem-evaluations";
    public static string CurricularUnitsFirstSemApproved { get; } = "cu1st-sem-approved";
    public static string CurricularUnitsFirstSemGrade { get; } = "cu1st-sem-grade";
    public static string CurricularUnitsFirstSemWithoutEvaluations { get; } = "cu1st-sem-without-eval";

    public static string CurricularUnitsSecondSemCredited { get; } = "cu2nd-sem-credited";
    public static string CurricularUnitsSecondSemEnrolled { get; } = "cu2nd-sem-enrolled";
    public static string CurricularUnitsSecondSemEvaluations { get; } = "cu2nd-sem-evaluations";
    public static string CurricularUnitsSecondSemApproved { get; } = "cu2nd-sem-approved";
    public static string CurricularUnitsSecondSemGrade { get; } = "cu2nd-sem-grade";
    public static string CurricularUnitsSecondSemWithoutEvaluations { get; } = "cu2nd-sem-without-eval";
    public static string UnemploymentRate { get; } = "unemployment-rate";
    public static string InflationRate { get; } = "inflation-rate";
    public static string GDP { get; } = "gdp";
}
