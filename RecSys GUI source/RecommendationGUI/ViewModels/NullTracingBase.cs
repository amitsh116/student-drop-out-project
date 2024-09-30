using System;

namespace RecommendationGUI.ViewModels;

/// <summary>
/// Base class for null-tracing ViewModel.
/// </summary>
public class NullTracingBase : ViewModelBase
{
    /// <summary>
    /// Function to call when one (or more) field(s) is null.
    /// </summary>
    private readonly Action _onHasNull;

    /// <summary>
    /// Function to call when has no null fields.
    /// </summary>
    private readonly Action _onNoNull;

    /// <summary>
    /// Counter of null fields.
    /// </summary>
    private int _nullFieldsCount;

    /// <summary>
    /// Constructor of `NullTracingBase`.
    /// </summary>
    /// <param name="onHasNull">Function to call when one (or more) field(s) is null.</param>
    /// <param name="onNoNull">Function to call when has no null fields.</param>
    protected NullTracingBase(Action? onHasNull = null, Action? onNoNull = null)
    {
        _onHasNull = onHasNull ?? (() => { });
        _onNoNull = onNoNull ?? (() => { });
        _nullFieldsCount = 0;
    }

    /// <summary>
    /// Registers that a field became null.
    /// </summary>
    protected void RegisterNullInc()
    {
        if (0 == _nullFieldsCount)
            _onHasNull(); // now has null field
        _nullFieldsCount++;
    }

    /// <summary>
    /// Registers that a field is no longer null.
    /// </summary>
    protected void RegisterNullDec()
    {
        if (_nullFieldsCount <= 0)
        {
            _nullFieldsCount = 0;
            return; // no increase below 0
        }

        _nullFieldsCount--;
        if (0 == _nullFieldsCount)
            _onNoNull(); // now has no null field
    }

    /// <summary>
    /// Updates null tracing based on a single field's old and new value.
    /// </summary>
    /// <param name="oldFieldVal">Old field value.</param>
    /// <param name="newFieldVal">New field value.</param>
    private void UpdateNullTracing(object? oldFieldVal, object? newFieldVal)
    {
        if (oldFieldVal is not null && newFieldVal is null) // added one null field
            RegisterNullInc();
        else if (oldFieldVal is null && newFieldVal is not null) // reduced one null field
            RegisterNullDec();
    }

    /// <summary>
    /// Sets property value, and applies null tracing based on its value change.
    /// </summary>
    /// <typeparam name="T">Type of property to set.</typeparam>
    /// <param name="prop">Property to set (by ref).</param>
    /// <param name="value">New value for property.</param>
    protected void SetAndNullTraceIfChanged<T>(ref T prop, T value)
    {
        if (Equals(prop, value))
            return;
        UpdateNullTracing(prop, value);
        prop = value;
    }
}
