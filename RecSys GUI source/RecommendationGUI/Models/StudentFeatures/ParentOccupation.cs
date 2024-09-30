namespace RecommendationGUI.Models.StudentFeatures;

/// <summary>
/// Represents student parent's occupation.
/// </summary>
public class ParentOccupation : CategorialBase
{
    /// <summary>
    /// Constructor of `ParentOccupation`.
    /// </summary>
    /// <param name="id">Representative numeric ID.</param>
    /// <param name="name">Display name.</param>
    protected ParentOccupation(int id, string name) : base(id, name) { }

    public static ParentOccupation Student { get; } = new(0, "Student");
    public static ParentOccupation LegislativeExecutiveManagers { get; } = new(1, "Representatives of the Legislative Power and Executive Bodies, Directors, Directors and Executive Managers");
    public static ParentOccupation SpecialistsIntellectualScientific { get; } = new(2, "Specialists in Intellectual and Scientific Activities");
    public static ParentOccupation IntermediateTechniciansProfessions { get; } = new(3, "Intermediate Level Technicians and Professions");
    public static ParentOccupation AdministrativeStaff { get; } = new(4, "Administrative Staff");
    public static ParentOccupation PersonalServicesSecurityWorkersSellers { get; } = new(5, "Personal Services, Security and Safety Workers and Sellers");
    public static ParentOccupation FarmersSkilledAgricultureFisheries { get; } = new(6, "Farmers and Skilled Workers in Agriculture, Fisheries and Forestry");
    public static ParentOccupation SkilledWorkersIndustryConstruction { get; } = new(7, "Skilled Workers in Industry, Construction and Craftsmen");
    public static ParentOccupation MachineOperatorsAssemblyWorkers { get; } = new(8, "Installation and Machine Operators and Assembly Workers");
    public static ParentOccupation UnskilledWorkers { get; } = new(9, "Unskilled Workers");
    public static ParentOccupation ArmedForcesProfessions { get; } = new(10, "Armed Forces Professions");
    public static ParentOccupation OtherSituation { get; } = new(90, "Other Situation");
    public static ParentOccupation Blank { get; } = new(99, "(blank)");
    public static ParentOccupation HealthProfessionals { get; } = new(122, "Health Professionals");
    public static ParentOccupation Teachers { get; } = new(123, "Teachers");
    public static ParentOccupation ICTSpecialists { get; } = new(125, "Specialists in Information and Communication Technologies (ICT)");
    public static ParentOccupation IntermediateScienceEngineeringTechnicians { get; } = new(131, "Intermediate Level Science and Engineering Technicians and Professions");
    public static ParentOccupation IntermediateHealthTechnicians { get; } = new(132, "Technicians and Professionals, of Intermediate Level of Health");
    public static ParentOccupation IntermediateLegalSocialCulturalServicesTechnicians { get; } = new(134, "Intermediate Level Technicians from Legal, Social, Sports, Cultural and Similar Services");
    public static ParentOccupation OfficeWorkersSecretaries { get; } = new(141, "Office Workers, Secretaries in General and Data Processing Operators");
    public static ParentOccupation DataAccountingStatisticalOperators { get; } = new(143, "Data, Accounting, Statistical, Financial Services and Registry-Related Operators");
    public static ParentOccupation OtherAdministrativeSupportStaff { get; } = new(144, "Other Administrative Support Staff");
    public static ParentOccupation PersonalServiceWorkers { get; } = new(151, "Personal Service Workers");
    public static ParentOccupation Sellers { get; } = new(152, "Sellers");
    public static ParentOccupation PersonalCareWorkers { get; } = new(153, "Personal Care Workers and the Like");
    public static ParentOccupation SkilledConstructionWorkersExceptElectricians { get; } = new(171, "Skilled Construction Workers and the Like, Except Electricians");
    public static ParentOccupation SkilledWorkersPrintingPrecision { get; } = new(173, "Skilled Workers in Printing, Precision Instrument Manufacturing, Jewelers, Artisans and the Like");
    public static ParentOccupation FoodProcessingWoodworkingIndustryWorkers { get; } = new(175, "Workers in Food Processing, Woodworking, Clothing and Other Industries and Crafts");
    public static ParentOccupation CleaningWorkers { get; } = new(191, "Cleaning Workers");
    public static ParentOccupation UnskilledAgricultureWorkers { get; } = new(192, "Unskilled Workers in Agriculture, Animal Production, Fisheries and Forestry");
    public static ParentOccupation UnskilledIndustryConstructionTransportWorkers { get; } = new(193, "Unskilled Workers in Extractive Industry, Construction, Manufacturing and Transport");
    public static ParentOccupation MealPreparationAssistants { get; } = new(194, "Meal Preparation Assistants");

    /// <summary>
    /// Default parent occupation option (to display before input).
    /// </summary>
    public static ParentOccupation DefaultOption { get; } = Blank;

    /// <summary>
    /// All parent occupation options.
    /// </summary>
    public static ParentOccupation[] AllOptions { get; } =
    [
        Student, LegislativeExecutiveManagers, SpecialistsIntellectualScientific, IntermediateTechniciansProfessions, AdministrativeStaff,
        PersonalServicesSecurityWorkersSellers, FarmersSkilledAgricultureFisheries, SkilledWorkersIndustryConstruction, MachineOperatorsAssemblyWorkers,
        UnskilledWorkers, ArmedForcesProfessions, OtherSituation, Blank, HealthProfessionals, Teachers, ICTSpecialists,
        IntermediateScienceEngineeringTechnicians, IntermediateHealthTechnicians, IntermediateLegalSocialCulturalServicesTechnicians,
        OfficeWorkersSecretaries, DataAccountingStatisticalOperators, OtherAdministrativeSupportStaff, PersonalServiceWorkers,
        Sellers, PersonalCareWorkers, SkilledConstructionWorkersExceptElectricians, SkilledWorkersPrintingPrecision, FoodProcessingWoodworkingIndustryWorkers,
        CleaningWorkers, UnskilledAgricultureWorkers, UnskilledIndustryConstructionTransportWorkers, MealPreparationAssistants
    ];
}
