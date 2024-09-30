using RecommendationGUI.BackEnd;
using RecommendationGUI.Models.StudentFeatures;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace RecommendationGUI.Models;

/// <summary>
/// Represents a student, with their features.
/// </summary>
public class Student
{
    /// <summary>
    /// Student's marital status.
    /// </summary>
    public MaritalStatus MaritalStatus { get; set; } = MaritalStatus.DefaultOption;

    /// <summary>
    /// How student has applied.
    /// </summary>
    public ApplicationMode ApplicationMode { get; set; } = ApplicationMode.DefaultOption;

    /// <summary>
    /// Student's application order.
    /// </summary>
    public ApplicationOrder ApplicationOrder { get; set; } = ApplicationOrder.DefaultOption;

    /// <summary>
    /// All courses that student's enrolled in.
    /// </summary>
    public Course[] Courses { get; set; } = [];

    /// <summary>
    /// Previous qualification of student.
    /// </summary>
    public StudentQualification PreviousQualification { get; set; } = StudentQualification.DefaultOption;

    /// <summary>
    /// Student's grade in previous qualification.
    /// </summary>
    public float PreviousQualificationGrade { get; set; } = PrevGrade.DefaultOption;

    /// <summary>
    /// Student's nationality.
    /// </summary>
    public Nationality Nationality { get; set; } = Nationality.DefaultOption;

    /// <summary>
    /// Qualification of student's mother.
    /// </summary>
    public ParentQualification MotherQualification { get; set; } = ParentQualification.DefaultOption;

    /// <summary>
    /// Qualification of student's father.
    /// </summary>
    public ParentQualification FatherQualification { get; set; } = ParentQualification.DefaultOption;

    /// <summary>
    /// Occupation of student's mother.
    /// </summary>
    public ParentOccupation MotherOccupation { get; set; } = ParentOccupation.DefaultOption;

    /// <summary>
    /// Occupation of student's father.
    /// </summary>
    public ParentOccupation FatherOccupation { get; set; } = ParentOccupation.DefaultOption;

    /// <summary>
    /// Student's admission grade.
    /// </summary>
    public float AdmissionGrade { get; set; } = PrevGrade.DefaultOption;

    /// <summary>
    /// Whether or not student was displaced.
    /// </summary>
    public YesNo Displaced { get; set; } = YesNo.No;

    /// <summary>
    /// Whether or not student has educational special needs.
    /// </summary>
    public YesNo EducationalSpecialNeeds { get; set; } = YesNo.No;

    /// <summary>
    /// Whether or not student is a debtor.
    /// </summary>
    public YesNo Debtor { get; set; } = YesNo.No;

    /// <summary>
    /// Whether or not student's tuition fees are up-to-date.
    /// </summary>
    public YesNo TuitionFeesUpToDate { get; set; } = YesNo.Yes;

    /// <summary>
    /// Student's gender.
    /// </summary>
    public Gender Gender { get; set; } = Gender.DefaultOption;

    /// <summary>
    /// Whether or not student is a scholarship holder.
    /// </summary>
    public YesNo ScholarshipHolder { get; set; } = YesNo.No;

    /// <summary>
    /// Student's age at enrollment.
    /// </summary>
    public int AgeAtEnrollment { get; set; } = 21;

    /// <summary>
    /// Whether or not student is studying international.
    /// </summary>
    public YesNo International { get; set; } = YesNo.No;

    /// <summary>
    /// Units of student's first semester.
    /// </summary>
    public UnitsInput FirstSemester { get; set; } = UnitsInput.DefaultOption;

    /// <summary>
    /// Student's grade in first semester.
    /// </summary>
    public int FirstSemesterGrade { get; set; } = SemGrade.DefaultOption;

    /// <summary>
    /// Units in student's second semester.
    /// </summary>
    public UnitsInput SecondSemester { get; set; } = UnitsInput.DefaultOption;

    /// <summary>
    /// Student's grade in second semester.
    /// </summary>
    public int SecondSemesterGrade { get; set; } = SemGrade.DefaultOption;

    /// <summary>
    /// Student's unemployment rate.
    /// </summary>
    public float UnemploymentRate { get; set; } = 10;

    /// <summary>
    /// Inflation rate during student's studying.
    /// </summary>
    public float InflationRate { get; set; } = 2;

    /// <summary>
    /// Student's GDP score.
    /// </summary>
    public float GDP { get; set; } = 2;

    /// <summary>
    /// Converts student to CLI arguments, for a given course they're enrolled in (referred to as "origin course").
    /// </summary>
    /// <param name="orgCourse">Origin course.</param>
    /// <returns>A dictionary of args.</returns>
    public Dictionary<string, object> ToCliArgsDict(Course orgCourse) => new()
    {
        [CliArgs.MaritalStatus] = MaritalStatus.ToNum(),
        [CliArgs.ApplicationMode] = ApplicationMode.ToNum(),
        [CliArgs.ApplicationOrder] = ApplicationOrder.ToNum(),
        [CliArgs.Course] = orgCourse.ID,
        [CliArgs.DaytimeEveningAttendance] = orgCourse.Time.ToNum(),
        [CliArgs.PreviousQualification] = PreviousQualification.ToNum(),
        [CliArgs.PreviousQualificationGrade] = PreviousQualificationGrade,
        [CliArgs.Nationality] = Nationality.ToNum(),
        [CliArgs.MotherQualification] = MotherQualification.ToNum(),
        [CliArgs.FatherQualification] = FatherQualification.ToNum(),
        [CliArgs.MotherOccupation] = MotherOccupation.ToNum(),
        [CliArgs.FatherOccupation] = FatherOccupation.ToNum(),
        [CliArgs.AdmissionGrade] = AdmissionGrade,
        [CliArgs.Displaced] = Displaced.ToNum(),
        [CliArgs.EducationalSpecialNeeds] = EducationalSpecialNeeds.ToNum(),
        [CliArgs.Debtor] = Debtor.ToNum(),
        [CliArgs.TuitionFeesUpToDate] = TuitionFeesUpToDate.ToNum(),
        [CliArgs.Gender] = Gender.ToNum(),
        [CliArgs.ScholarshipHolder] = ScholarshipHolder.ToNum(),
        [CliArgs.AgeAtEnrollment] = AgeAtEnrollment,
        [CliArgs.International] = International.ToNum(),
        [CliArgs.CurricularUnitsFirstSemCredited] = FirstSemester.UnitsCredited,
        [CliArgs.CurricularUnitsFirstSemEnrolled] = FirstSemester.UnitsEnrolled,
        [CliArgs.CurricularUnitsFirstSemEvaluations] = FirstSemester.Evaluations,
        [CliArgs.CurricularUnitsFirstSemApproved] = FirstSemester.UnitsApproved,
        [CliArgs.CurricularUnitsFirstSemGrade] = FirstSemesterGrade,
        [CliArgs.CurricularUnitsFirstSemWithoutEvaluations] = FirstSemester.UnitsWithoutEvaluations,
        [CliArgs.CurricularUnitsSecondSemCredited] = SecondSemester.UnitsCredited,
        [CliArgs.CurricularUnitsSecondSemEnrolled] = SecondSemester.UnitsEnrolled,
        [CliArgs.CurricularUnitsSecondSemEvaluations] = SecondSemester.Evaluations,
        [CliArgs.CurricularUnitsSecondSemApproved] = SecondSemester.UnitsApproved,
        [CliArgs.CurricularUnitsSecondSemGrade] = SecondSemesterGrade,
        [CliArgs.CurricularUnitsSecondSemWithoutEvaluations] = SecondSemester.UnitsWithoutEvaluations,
        [CliArgs.UnemploymentRate] = UnemploymentRate,
        [CliArgs.InflationRate] = InflationRate,
        [CliArgs.GDP] = GDP
    };
}
