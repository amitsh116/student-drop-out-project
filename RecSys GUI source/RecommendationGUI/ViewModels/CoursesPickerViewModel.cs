using ReactiveUI;
using RecommendationGUI.BackEnd;
using RecommendationGUI.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RecommendationGUI.ViewModels;

/// <summary>
/// ViewModel used for inputting student's courses.
/// </summary>
public class CoursesPickerViewModel : ViewModelBase
{
    private bool _showAddedCourseSelector;
    /// <summary>
    /// Whether or not should display a blank selector for a being-added course.
    /// </summary>
    public bool ShowAddedCourseSelector
    {
        get => _showAddedCourseSelector;
        protected set => this.RaiseAndSetIfChanged(ref _showAddedCourseSelector, value);
    }

    /// <summary>
    /// Command of add course button.
    /// </summary>
    public ICommand AddCourseCommand { get; }

    /// <summary>
    /// List of all courses available (not selected yet).
    /// </summary>
    private readonly List<Course> _availableCourses;

    private SelectorViewModel<Course> _addedCourseSelectorVM;
    /// <summary>
    /// ViewModel of blank selector for being-added course.
    /// </summary>
    public SelectorViewModel<Course> AddedCourseSelectorVM
    {
        get => _addedCourseSelectorVM;
        private set => this.RaiseAndSetIfChanged(ref _addedCourseSelectorVM, value);
    }

    /// <summary>
    /// Command for remove being-added course button.
    /// </summary>
    public ICommand AddedCourseInputRemoveCommand { get; }

    /// <summary>
    /// A ViewModel for each selected course selector.
    /// </summary>
    public ObservableCollection<AttachedCommand<SelectorViewModel<Course>>> SelectedCourseVMs { get; }


    /// <summary>
    /// Generates a new ViewModel for the being-added course selector.
    /// </summary>
    /// <returns></returns>
    private SelectorViewModel<Course> MakeAddedCourseSelectorVM()
        => new(_availableCourses, compare: Course.CompareIDs) { OnSelectedItemChanged = AddCourseConfirm };
    // on selected item change, we call AddCourseConfirm to move course into its own selected course selector

    /// <summary>
    /// Constructs courses picker view model.
    /// </summary>
    /// <param name="backend">IBackEnd instance used for retreiving available courses.</param>
    public CoursesPickerViewModel(IBackEnd backend)
    {
        _showAddedCourseSelector = false;

        _availableCourses = backend.GetAllCourses();

        SelectedCourseVMs = [];

        AddCourseCommand = ReactiveCommand.Create(AddCourseInput);

        _addedCourseSelectorVM = MakeAddedCourseSelectorVM();

        AddedCourseInputRemoveCommand = ReactiveCommand.Create(() =>
        {
            AddedCourseSelectorVM.UpdateSelectedItem(null); // doesn't call OnSelectedItemChanged
            ShowAddedCourseSelector = false;
        });
    }

    /// <summary>
    /// Adds being-added course selector.
    /// </summary>
    private void AddCourseInput()
    {
        if (0 == _availableCourses.Count)
            return; // no more courses to add

        // show added course selector to select new course:
        ShowAddedCourseSelector = true;
        AddedCourseSelectorVM.UpdateSelectedItem(AddedCourseSelectorVM.Items[0]);
        // just to ensure a change, prevent non-existing option glitch
        AddedCourseSelectorVM.UpdateSelectedItem(null); // notify view
    }

    /// <summary>
    /// Confirms selection of being-added course, making it selected.
    /// </summary>
    /// <param name="addedCourseSelector">`AddedCourseSelector`.</param>
    /// <param name="nullCourse">Previously selected course in `AddCourseSelector`, expected to be `null`.</param>
    /// <param name="addedCourse">Course being added to selected courses.</param>
    private void AddCourseConfirm(SelectorViewModel<Course> addedCourseSelector, 
                                  Course? nullCourse, Course? addedCourse)
    {
        if (nullCourse is not null)
            return; // nullCourse not null meaning not actually a selection confirm

        // prepare for adding course as selected:
        SelectorViewModel<Course> selectorVM = 
            new(_availableCourses, selectedItem: addedCourse, compare: Course.CompareIDs)
            {
                OnSelectedItemChanged = UpdateAvailableCourses
                // on selected item change, old selected item will become available again,
                // new selected item will become unavailable
            };

        // course no longer available:
        RemoveAvailableCourse(AddedCourseSelectorVM, addedCourse);

        // add course as selected:
        SelectedCourseVMs.Add(MakeSelectorWithRemoveCommand(selectorVM));

        // reset added course selector:
        AddedCourseSelectorVM = MakeAddedCourseSelectorVM();

        // hide added course selector:
        ShowAddedCourseSelector = false;
    }

    /// <summary>
    /// Attaches a remove course command to a given course selector.
    /// </summary>
    /// <param name="selector">A course selector.</param>
    /// <returns>`selector` attached to its own remove course command.</returns>
    private AttachedCommand<SelectorViewModel<Course>> MakeSelectorWithRemoveCommand(SelectorViewModel<Course> selector)
    {
        AttachedCommand<SelectorViewModel<Course>> res = new(selector);
        res.Command = ReactiveCommand.Create(() =>
        {
            // remove command is to add `selector`'s selected item as an available course,
            // and to remove selector from list
            AddAvailableCourse(selector, selector.SelectedItem);
            SelectedCourseVMs.Remove(res);
        });
        return res;
    }

    /// <summary>
    /// Registers a given course as available.
    /// </summary>
    /// <param name="source">Selector causing this request, `null` if irrelevant.</param>
    /// <param name="course">Course to register as available, `null` to be ignored.</param>
    private void AddAvailableCourse(SelectorViewModel<Course> source, Course? course)
    {
        if (course is null)
            return;

        // add for each selector unless cause this request (then has course already):
        foreach (var selectorVM in SelectedCourseVMs)
            if (!Equals(selectorVM.Obj, source))
                selectorVM.Obj.AddSorted(course);
        if (!Equals(AddedCourseSelectorVM, source))
            AddedCourseSelectorVM.AddSorted(course);
        _availableCourses.Add(course);
    }

    /// <summary>
    /// Registers a given course as unavailable.
    /// </summary>
    /// <param name="source">Selector causing this request, `null` if irrelevant.</param>
    /// <param name="course">Course to register as unavailable, `null` to be ignored.</param>
    private void RemoveAvailableCourse(SelectorViewModel<Course> source, Course? course)
    {
        if (course is null)
            return;

        // remove from each selector unless cause this request (then has course already):
        foreach (var selectorVM in SelectedCourseVMs)
            if (!Equals(selectorVM.Obj, source))
                selectorVM.Obj.RemoveItem(course);
        if (!Equals(AddedCourseSelectorVM, source))
            AddedCourseSelectorVM.RemoveItem(course);
        _availableCourses.Remove(course);
    }

    /// <summary>
    /// Registers one course as available and another as unavailable.
    /// </summary>
    /// <param name="source">Selector causing this request, `null` if irrelevant.</param>
    /// <param name="nowAvailable">Course to register as available.</param>
    /// <param name="noLongerAvailable">Course to register as unavailable.</param>
    private void UpdateAvailableCourses(SelectorViewModel<Course> source,
                                        Course? nowAvailable, Course? noLongerAvailable)
    {
        RemoveAvailableCourse(source, noLongerAvailable);
        AddAvailableCourse(source, nowAvailable);
    }

    /// <summary>
    /// Gets all selected courses.
    /// </summary>
    /// <returns>An array of all user's selected courses.</returns>
    public Course[] GetCourses()
    {
        var res = new Course[SelectedCourseVMs.Count];
        for (int i = 0; i < res.Length; i++)
            res[i] = SelectedCourseVMs[i].Obj.SelectedItem!;
        return res;
    }
}
