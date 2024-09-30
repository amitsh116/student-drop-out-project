using ReactiveUI;
using RecommendationGUI.Models;
using System;

namespace RecommendationGUI.ViewModels;

/// <summary>
/// ViewModel used for inputting curricular units information.
/// </summary>
/// <param name="title">Display title.</param>
/// <param name="registerNullInc">Function to call when a null input is added.</param>
/// <param name="registerNullDec">Function to call when a null input is removed.</param>
public class UnitsInputViewModel(string title,
                                 Action? registerNullInc = null,
                                 Action? registerNullDec = null) : ViewModelBase
{
    /// <summary>
    /// Function to call when a null input is added.
    /// </summary>
    private readonly Action _registerNullInc = registerNullInc ?? (() => { });

    /// <summary>
    /// Function to call when a null input is removed.
    /// </summary>
    private readonly Action _registerNullDec = registerNullDec ?? (() => { });

    /// <summary>
    /// Whether or not has registered nullInc last (otherwise nullDec).
    /// </summary>
    private bool _registeredNull = false;

    /// <summary>
    /// Display title.
    /// </summary>
    public string Title { get; } = title;

    /// <summary>
    /// Determines if input is in expanded mode (full details) or collapsed mode (just units number).
    /// </summary>
    public bool IsExpanded
    {
        get => _isExpanded;
        set
        {
            if (value == _isExpanded)
                return;
            _isExpanded = value;
            this.RaisePropertyChanged();
            this.RaisePropertyChanged(nameof(IsCollapsed));

            UpdateNullTracing();
        }
    }
    private bool _isExpanded = false;

    /// <summary>
    /// Determines if input is in collapsed mode (just units number) or expanded mode (full details).
    /// </summary>
    public bool IsCollapsed => !_isExpanded;

    private int? _units = 0;
    /// <summary>
    /// Units number inputted.
    /// </summary>
    public int? Units
    {
        get => _units;
        set
        {
            if (value == _units)
                return;
            _units = value;
            UpdateNullTracing();
        }
    }

    private int? _unitsCredited = 0;
    /// <summary>
    /// Units credited number inputted.
    /// </summary>
    public int? UnitsCredited
    {
        get => _unitsCredited;
        set
        {
            if (value == _unitsCredited)
                return;
            _unitsCredited = value;
            UpdateNullTracing();
        }
    }

    private int? _unitsEnrolled = 0;
    /// <summary>
    /// Units enrolled number inputted.
    /// </summary>
    public int? UnitsEnrolled
    {
        get => _unitsEnrolled;
        set
        {
            if (value == _unitsEnrolled)
                return;
            _unitsEnrolled = value;
            UpdateNullTracing();
        }
    }

    private int? _evaluations = 0;
    /// <summary>
    /// Evaluations number inputted.
    /// </summary>
    public int? Evaluations
    {
        get => _evaluations;
        set
        {
            if (value == _evaluations)
                return;
            _evaluations = value;
            UpdateNullTracing();
        }
    }

    private int? _unitsApproved = 0;
    /// <summary>
    /// Units approved number inputted.
    /// </summary>
    public int? UnitsApproved
    {
        get => _unitsApproved;
        set
        {
            if (value == _unitsApproved)
                return;
            _unitsApproved = value;
            UpdateNullTracing();
        }
    }

    private int? _unitsWithoutEvaluations = 0;
    /// <summary>
    /// Units without evaluations number inputted.
    /// </summary>
    public int? UnitsWithoutEvaluations
    {
        get => _unitsWithoutEvaluations;
        set
        {
            if (value == _unitsWithoutEvaluations)
                return;
            _unitsWithoutEvaluations = value;
            UpdateNullTracing();
        }
    }

    /// <summary>
    /// Updates units input null tracing.
    /// </summary>
    private void UpdateNullTracing()
    {
        if (IsExpanded)
            UpdateNullStatus(HasNullExpandedField);
        else
            UpdateNullStatus(Units is null);
    }

    /// <summary>
    /// Checks if all inputs are not null when expanded.
    /// </summary>
    private bool HasNullExpandedField => UnitsCredited is null ||
                                           UnitsEnrolled is null ||
                                           Evaluations is null ||
                                           UnitsApproved is null ||
                                           UnitsWithoutEvaluations is null;

    /// <summary>
    /// Updates null tracing status.
    /// <param name="hasNull">Whether or not has relevant (visible) null fields.</param>
    /// </summary>
    private void UpdateNullStatus(bool hasNull)
    {
        if (_registeredNull && !hasNull) // if registered as has null, yet doesn't have null
        {
            _registerNullDec(); // register has no null
            _registeredNull = false;
        }
        else if (!_registeredNull && hasNull) // if registered as has no nulls, yet has null
        {
            _registerNullInc(); // register has null
            _registeredNull = true;
        }
    }

    /// <summary>
    /// Gets user's input in `UnitsInput` form.
    /// </summary>
    /// <returns>User's units input.</returns>
    public UnitsInput GetInput() => _isExpanded ? new(
        unitsCredited: (int)UnitsCredited!,
        unitsEnrolled: (int)UnitsEnrolled!,
        evaluations: (int)Evaluations!,
        unitsApproved: (int)UnitsApproved!,
        unitsWithoutEvaluations: (int)UnitsWithoutEvaluations!
    ) : new(
        unitsCredited: (int)Units!,
        unitsEnrolled: (int)Units!,
        evaluations: (int)Units!,
        unitsApproved: (int)Units!,
        unitsWithoutEvaluations: 0
    );
}
