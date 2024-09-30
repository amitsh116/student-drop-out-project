using RecommendationGUI.BackEnd;
using RecommendationGUI.Models;
using System.Collections.ObjectModel;
using System;
using ReactiveUI;
using System.Collections.Generic;
using System.Threading.Tasks;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace RecommendationGUI.ViewModels;

/// <summary>
/// ViewModel used for displaying recommendations provided by multiple origin courses.
/// </summary>
/// <param name="backend">IBackend instance, used for retreiving recommendations.</param>
public class RecommendationsViewModel(IBackEnd backend) : ViewModelBase
{
    /// <summary>
    /// A ViewModel for each origin course's generated recommendations.
    /// </summary>
    public ObservableCollection<RecommendationViewModel> RecVMs { get; } = [];

    private bool _isLoading = false;
    /// <summary>
    /// Whether or not there are recommendations currently loading.
    /// </summary>
    public bool IsLoading
    {
        get => _isLoading;
        set => this.RaiseAndSetIfChanged(ref _isLoading, value);
    }

    /// <summary>
    /// IBackend instance used for retreiving recommendations.
    /// </summary>
    private readonly IBackEnd _backend = backend;

    /// <summary>
    /// Updates displayed recommendations (re-generates recommendations and displays them).
    /// </summary>
    /// <param name="student">Student to generate recommendations for.</param>
    /// <returns>`true` if has any recommendations to show, otherwise `false`.</returns>
    public async Task<bool> Update(Student student)
    {
        IsLoading = true;
        RecVMs.Clear();
        await Task.Run(async () =>
        {
            foreach (Course orgCourse in student.Courses)
            {
                try
                {
                    var (orgChance, actionsRec, coursesRec) = _backend.Recommend(student, orgCourse);
                    RecVMs.Add(new(orgCourse, orgChance, actionsRec, coursesRec));
                }
                catch (BackEndException e)
                {
                    await MessageBoxManager.GetMessageBoxStandard(
                        "Back End Error", e.Message, ButtonEnum.Ok
                    ).ShowAsync();
                }
            }
        });
        IsLoading = false;
        return RecVMs.Count > 0;
    }
}
