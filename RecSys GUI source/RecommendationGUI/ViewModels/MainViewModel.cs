using ReactiveUI;
using RecommendationGUI.BackEnd;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RecommendationGUI.ViewModels;

/// <summary>
/// ViewModel of main view.
/// </summary>
public class MainViewModel : ViewModelBase
{
    private bool _hasNoRecs = false;
    /// <summary>
    /// Whether has no recommendations to show (after trying to load).
    /// </summary>
    public bool HasNoRecs
    {
        get => _hasNoRecs;
        private set => this.RaiseAndSetIfChanged(ref _hasNoRecs, value);
    }

    private bool _isRecommendButtonEnabled = true;
    /// <summary>
    /// Whether or not "recommend" button is enabled.
    /// </summary>
    public bool IsRecommendButtonEnabled
    {
        get => _isRecommendButtonEnabled;
        private set => this.RaiseAndSetIfChanged(ref _isRecommendButtonEnabled, value);
    }

    /// <summary>
    /// ViewModel for student input view.
    /// </summary>
    public StudentInputViewModel StudentInputViewModel { get; }

    /// <summary>
    /// Command of "recommend" button.
    /// </summary>
    public ICommand RecommendCommand { get; }

    /// <summary>
    /// ViewModel for recommendations display.
    /// </summary>
    public RecommendationsViewModel RecommendationsViewModel { get; }

    /// <summary>
    /// Constructs MainViewModel with a given IBackend instance.
    /// </summary>
    /// <param name="backend">IBackend instance used for retreiving data and recommendations.</param>
    public MainViewModel(IBackEnd backend)
    {
        StudentInputViewModel = new(backend, onHasNull: DisableRecommendButton, onNoNull: EnableRecommendButton);
        RecommendCommand = ReactiveCommand.Create(UpdateRecommendations);
        RecommendationsViewModel = new(backend);
    }

    /// <summary>
    /// Updates displayed recommendations.
    /// </summary>
    private async Task UpdateRecommendations()
    {
        HasNoRecs = false;
        HasNoRecs = !await RecommendationsViewModel.Update(StudentInputViewModel.GetStudent());
    }

    /// <summary>
    /// Enables "recommend" button.
    /// </summary>
    public void EnableRecommendButton() => IsRecommendButtonEnabled = true;

    /// <summary>
    /// Disables "recommend" button.
    /// </summary>
    public void DisableRecommendButton() => IsRecommendButtonEnabled = false;
}
