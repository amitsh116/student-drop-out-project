using ReactiveUI;
using RecommendationGUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RecommendationGUI.ViewModels;

/// <summary>
/// ViewModel used for inputting a categorial feature, or anything that is multiple choice.
/// </summary>
/// <typeparam name="T">Input type.</typeparam>
/// <param name="items">All input options.</param>
/// <param name="selectedItem">Default selected item (optional).</param>
/// <param name="compare">Function comparing between options (optional).</param>
public class SelectorViewModel<T>(IEnumerable<T> items, 
                                  T? selectedItem = null, 
                                  Func<T, T, int>? compare = null) : ViewModelBase()
    where T : class, IDisplayable
{
    /// <summary>
    /// Function comparing between options.
    /// </summary>
    private readonly Func<T, T, int>? _compare = compare;

    /// <summary>
    /// All input options.
    /// </summary>
    public ObservableCollection<T> Items { get; } = new(items);


    private T? _selectedItem = selectedItem;
    /// <summary>
    /// Currently selected option (`null` for nothing).
    /// </summary>
    public T? SelectedItem
    {
        get => _selectedItem;
        set
        {
            if (Equals(_selectedItem, value))
                return;
            T? oldSelectedItem = _selectedItem;
            _selectedItem = value;
            if (OnSelectedItemChanged is not null)
                OnSelectedItemChanged(this, oldSelectedItem, _selectedItem);
        }
    }

    /// <summary>
    /// Updates selected item, without calling OnSelectedItemChanged.
    /// </summary>
    /// <param name="item">Item to select.</param>
    public void UpdateSelectedItem(T? item)
    {
        if (Equals(item, _selectedItem))
            return;
        _selectedItem = item;
        this.RaisePropertyChanged(nameof(SelectedItem));
    }

    /// <summary>
    /// Function called whenever selected item is changed via view.
    /// Receives selectore VM, old selected value, new selected value.
    /// </summary>
    public Action<SelectorViewModel<T>, T?, T?>? OnSelectedItemChanged { get; set; } = null;

    /// <summary>
    /// Adds an item to the selector, in a sorted way.
    /// Assumes selector items are sorted.
    /// </summary>
    /// <param name="item">Item to insert in a sorted way.</param>
    public void AddSorted(T item)
    {
        if (_compare is null)
        {
            Items.Add(item);
            return;
        }

        int i = 0;
        for (; i < Items.Count; i++)
        {
            if (_compare(Items[i], item) > 0)
                break;
        }
        Items.Insert(i, item);
    }

    /// <summary>
    /// Removes item from selector, unselects if selected (without calling OnSelectedItemChanged).
    /// </summary>
    /// <param name="item">Item to remove from selector options.</param>
    public void RemoveItem(T item)
    {
        // prevent call of OnSelectedItemChanged while removing
        var temp = OnSelectedItemChanged;
        OnSelectedItemChanged = null;

        // selected item should be null if is null already or will be removed
        bool selectedShouldBeNull = _selectedItem is null || Equals(item, _selectedItem);

        // remove item
        Items.Remove(item);

        // set selected to null if should be
        if (selectedShouldBeNull)
            UpdateSelectedItem(null);

        // restore OnSelectedItemChanged
        OnSelectedItemChanged = temp;
    }
}
