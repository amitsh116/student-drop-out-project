using RecommendationGUI.BackEnd;
using RecommendationGUI.Models;
using RecommendationGUI.Models.StudentFeatures;
using System;

namespace RecommendationGUI.ViewModels;

/// <summary>
/// ViewModel used for inputting student's features.
/// </summary>
public class StudentInputViewModel : NullTracingBase
{
    /// <summary>
    /// ViewModel for martial status selector.
    /// </summary>
    public SelectorViewModel<MaritalStatus> MaritalStatusSelectorVM { get; } =
        new(MaritalStatus.AllOptions, MaritalStatus.DefaultOption);

    /// <summary>
    /// ViewModel for application mode selector.
    /// </summary>
    public SelectorViewModel<ApplicationMode> ApplicationModeSelectorVM { get; } =
        new(ApplicationMode.AllOptions, ApplicationMode.DefaultOption);

    /// <summary>
    /// ViewModel for application order selector.
    /// </summary>
    public SelectorViewModel<ApplicationOrder> ApplicationOrderSelectorVM { get; } = 
        new(ApplicationOrder.AllOptions, ApplicationOrder.DefaultOption);

    /// <summary>
    /// ViewModel for courses picker.
    /// </summary>
    public CoursesPickerViewModel CoursesPickerVM { get; }

    /// <summary>
    /// ViewModel for daytime/evening attendance selector.
    /// </summary>
    public SelectorViewModel<DaytimeEveningAttendance> DaytimeEveningAttendanceSelectorVM { get; } =
        new(DaytimeEveningAttendance.AllOptions, DaytimeEveningAttendance.DefaultOption);

    /// <summary>
    /// ViewModel for previous qualification selector.
    /// </summary>
    public SelectorViewModel<StudentQualification> PreviousQualificationSelectorVM { get; } = 
        new(StudentQualification.AllOptions, StudentQualification.DefaultOption);

    private decimal? _previousQualificationGradePercentage = (decimal)PrevGrade.DefaultPercentage;
    /// <summary>
    /// Binded previous qualification grade (%) input.
    /// </summary>
    public decimal? PreviousQualificationGradePercentage
    {
        get => _previousQualificationGradePercentage;
        set => this.SetAndNullTraceIfChanged(ref _previousQualificationGradePercentage, value);
    }

    /// <summary>
    /// ViewModel for nationality selector.
    /// </summary>
    public SelectorViewModel<Nationality> NationalitySelectorVM { get; } = 
        new(Nationality.AllOptions, Nationality.DefaultOption);

    /// <summary>
    /// ViewModel for mother's qualification selector.
    /// </summary>
    public SelectorViewModel<ParentQualification> MotherQualificationSelectorVM { get; } =
        new(ParentQualification.AllOptions, ParentQualification.DefaultOption);

    /// <summary>
    /// ViewModel for father's qualification selector.
    /// </summary>
    public SelectorViewModel<ParentQualification> FatherQualificationSelectorVM { get; } =
        new(ParentQualification.AllOptions, ParentQualification.DefaultOption);

    /// <summary>
    /// ViewModel for mother's occupation selector.
    /// </summary>
    public SelectorViewModel<ParentOccupation> MotherOccupationSelectorVM { get; } =
        new(ParentOccupation.AllOptions, ParentOccupation.DefaultOption);

    /// <summary>
    /// ViewModel for father's occupation selector.
    /// </summary>
    public SelectorViewModel<ParentOccupation> FatherOccupationSelectorVM { get; } =
        new(ParentOccupation.AllOptions, ParentOccupation.DefaultOption);

    private decimal? _admissionGradePercentage = (decimal)PrevGrade.DefaultPercentage;
    /// <summary>
    /// Binded admission grade (%) input.
    /// </summary>
    public decimal? AdmissionGradePercentage
    {
        get => _admissionGradePercentage;
        set => this.SetAndNullTraceIfChanged(ref _admissionGradePercentage, value);
    }

    /// <summary>
    /// ViewModel for displaced (binary) selector.
    /// </summary>
    public SelectorViewModel<YesNo> DisplacedSelectorVM { get; } = new(YesNo.AllOptions, YesNo.No);

    /// <summary>
    /// ViewModel for educational special needs (binary) selector.
    /// </summary>
    public SelectorViewModel<YesNo> EducationalSpecialNeedsSelectorVM { get; } = new(YesNo.AllOptions, YesNo.No);

    /// <summary>
    /// ViewModel for debtor (binary) selector.
    /// </summary>
    public SelectorViewModel<YesNo> DebtorSelectorVM { get; } = new(YesNo.AllOptions, YesNo.No);

    /// <summary>
    /// ViewModel for tuition fees up-to-date (binary) selector.
    /// </summary>
    public SelectorViewModel<YesNo> TuitionFeesUpToDateSelectorVM { get; } =
        new(YesNo.AllOptions, YesNo.Yes);

    /// <summary>
    /// ViewModel for gender selector.
    /// </summary>
    public SelectorViewModel<Gender> GenderSelectorVM { get; } = new(Gender.AllOptions, Gender.DefaultOption);

    /// <summary>
    /// ViewModel for scholarship holder (binary) selector.
    /// </summary>
    public SelectorViewModel<YesNo> ScholarshipHolderSelectorVM { get; } =
        new(YesNo.AllOptions, YesNo.No);

    private int? _ageAtEnrollment = 21;
    /// <summary>
    /// Binded age at enrollment input.
    /// </summary>
    public int? AgeAtEnrollment
    {
        get => _ageAtEnrollment;
        set => this.SetAndNullTraceIfChanged(ref _ageAtEnrollment, value);
    }

    /// <summary>
    /// ViewModel for international (binary) selector.
    /// </summary>
    public SelectorViewModel<YesNo> InternationalSelectorVM { get; } = new(YesNo.AllOptions, YesNo.No);

    /// <summary>
    /// ViewModel for first semester units input view.
    /// </summary>
    public UnitsInputViewModel FirstSemesterUnitsInputVM { get; }

    private decimal? _firstSemesterGradePercentage = (decimal)SemGrade.DefaultPercentage;
    /// <summary>
    /// Binded first semester grade (%) input.
    /// </summary>
    public decimal? FirstSemesterGradePercentage
    {
        get => _firstSemesterGradePercentage;
        set => this.SetAndNullTraceIfChanged(ref _firstSemesterGradePercentage, value);
    }

    /// <summary>
    /// ViewModel for second semester units input view.
    /// </summary>
    public UnitsInputViewModel SecondSemesterUnitsInputVM { get; }

    private decimal? _secondSemesterGradePercentage = (decimal)SemGrade.DefaultPercentage;
    /// <summary>
    /// Binded second seester grade (%) input.
    /// </summary>
    public decimal? SecondSemesterGradePercentage
    {
        get => _secondSemesterGradePercentage;
        set => this.SetAndNullTraceIfChanged(ref _secondSemesterGradePercentage, value);
    }

    private decimal? _unemploymentRate = 2;
    /// <summary>
    /// Binded unemployment rate input.
    /// </summary>
    public decimal? UnemploymentRate
    {
        get => _unemploymentRate;
        set => this.SetAndNullTraceIfChanged(ref _unemploymentRate, value);
    }

    private decimal? _inflationRate = 2;
    /// <summary>
    /// Binded inflation rate input.
    /// </summary>
    public decimal? InflationRate
    {
        get => _inflationRate;
        set => this.SetAndNullTraceIfChanged(ref _inflationRate, value);
    }

    private decimal? _gdp = 2;
    /// <summary>
    /// Binded GDP input.
    /// </summary>
    public decimal? GDP
    {
        get => _gdp;
        set => this.SetAndNullTraceIfChanged(ref _gdp, value);
    }


    /// <summary>
    /// Constructor of `StudentInputViewModel`.
    /// </summary>
    /// <param name="backend">IBackEnd instance used for retreiving available courses.</param>
    /// <param name="onHasNull">Function to call when one (or more) field(s) is null.</param>
    /// <param name="onNoNull">Function to call when has no null fields.</param>
    public StudentInputViewModel(IBackEnd backend, Action? onHasNull = null, Action? onNoNull = null)
        : base(onHasNull, onNoNull)
    {
        CoursesPickerVM = new(backend);

        FirstSemesterUnitsInputVM = new(
            title: "1st Semester",
            registerNullInc: RegisterNullInc,
            registerNullDec: RegisterNullDec
        );

        SecondSemesterUnitsInputVM = new(
            title: "2nd Semester",
            registerNullInc: RegisterNullInc,
            registerNullDec: RegisterNullDec
        );
    }


    /// <summary>
    /// Gets inputted student.
    /// </summary>
    /// <returns>Student made of all user inputs.</returns>

    public Student GetStudent()
    {
        return new()
        {
            MaritalStatus = MaritalStatusSelectorVM.SelectedItem!,
            ApplicationMode = ApplicationModeSelectorVM.SelectedItem!,
            ApplicationOrder = ApplicationOrderSelectorVM.SelectedItem!,
            Courses = CoursesPickerVM.GetCourses(),
            PreviousQualification = PreviousQualificationSelectorVM.SelectedItem!,
            PreviousQualificationGrade = PrevGrade.BackendValFromPercentage((float)PreviousQualificationGradePercentage!),
            Nationality = NationalitySelectorVM.SelectedItem!,
            MotherQualification = MotherQualificationSelectorVM.SelectedItem!,
            FatherQualification = FatherQualificationSelectorVM.SelectedItem!,
            MotherOccupation = MotherOccupationSelectorVM.SelectedItem!,
            FatherOccupation = FatherOccupationSelectorVM.SelectedItem!,
            AdmissionGrade = PrevGrade.BackendValFromPercentage((float)AdmissionGradePercentage!),
            Displaced = DisplacedSelectorVM.SelectedItem!,
            EducationalSpecialNeeds = EducationalSpecialNeedsSelectorVM.SelectedItem!,
            Debtor = DebtorSelectorVM.SelectedItem!,
            TuitionFeesUpToDate = TuitionFeesUpToDateSelectorVM.SelectedItem!,
            Gender = GenderSelectorVM.SelectedItem!,
            ScholarshipHolder = ScholarshipHolderSelectorVM.SelectedItem!,
            AgeAtEnrollment = (int)AgeAtEnrollment!,
            International = InternationalSelectorVM.SelectedItem!,
            FirstSemester = FirstSemesterUnitsInputVM.GetInput(),
            FirstSemesterGrade = SemGrade.BackendValFromPercentage((float)FirstSemesterGradePercentage!),
            SecondSemester = SecondSemesterUnitsInputVM.GetInput(),
            SecondSemesterGrade = SemGrade.BackendValFromPercentage((float)SecondSemesterGradePercentage!),
            UnemploymentRate = (float)UnemploymentRate!,
            InflationRate = (float)InflationRate!,
            GDP = (float)GDP!
        };
    }
}
