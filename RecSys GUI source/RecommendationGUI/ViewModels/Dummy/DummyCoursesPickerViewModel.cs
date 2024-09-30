using RecommendationGUI.BackEnd;

namespace RecommendationGUI.ViewModels.Dummy;

/// <summary>
/// Dummy version of `CoursesPickerViewModel`.
/// </summary>
public class DummyCoursesPickerViewModel : CoursesPickerViewModel
{
    /// <summary>
    /// Constructs a new `DummyCoursesPickerViewModel`.
    /// </summary>
    public DummyCoursesPickerViewModel() : base(new DummyBackEnd())
    {
        // // uncomment to see view with one added course:
        //AddedCourseSelectorVM.SelectedItem = AvailableCourses[0];
        //ShowAddedCourseSelector = false;
    }
}
