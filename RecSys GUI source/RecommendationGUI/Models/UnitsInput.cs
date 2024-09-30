namespace RecommendationGUI.Models;

/// <summary>
/// Model for semester units input.
/// </summary>
public class UnitsInput
{
    /// <summary>
    /// Number of units credited for semester.
    /// </summary>
    public int UnitsCredited { get; }

    /// <summary>
    /// Number of units enrolled in semester.
    /// </summary>
    public int UnitsEnrolled { get; }

    /// <summary>
    /// Number of evaluations in semester.
    /// </summary>
    public int Evaluations { get; }

    /// <summary>
    /// Number of units approved for semester.
    /// </summary>
    public int UnitsApproved { get; }

    /// <summary>
    /// Number of units without evaluations in semester.
    /// </summary>
    public int UnitsWithoutEvaluations { get; }

    /// <summary>
    /// Constructs a new `UnitsInput` instance.
    /// </summary>
    /// <param name="unitsCredited">Number of units credited for semester.</param>
    /// <param name="unitsEnrolled">Number of units enrolled in semester.</param>
    /// <param name="evaluations">Number of evaluations in semester.</param>
    /// <param name="unitsApproved">Number of units approved for semester.</param>
    /// <param name="unitsWithoutEvaluations">Number of units without evaluations in semester.</param>
    internal UnitsInput(int unitsCredited, int unitsEnrolled, int evaluations,
                        int unitsApproved, int unitsWithoutEvaluations)
    {
        UnitsCredited = unitsCredited;
        UnitsEnrolled = unitsEnrolled;
        Evaluations = evaluations;
        UnitsApproved = unitsApproved;
        UnitsWithoutEvaluations = unitsWithoutEvaluations;
    }

    /// <summary>
    /// Gets default units input (to display before user input).
    /// </summary>
    public static UnitsInput DefaultOption { get; } = new(
        unitsCredited: 6,
        unitsEnrolled: 6,
        evaluations: 10,
        unitsApproved: 6,
        unitsWithoutEvaluations: 0
    );
}
