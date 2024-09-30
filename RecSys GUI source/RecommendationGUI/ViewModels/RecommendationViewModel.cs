using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using RecommendationGUI.Models;
using RecommendationGUI.Utils;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;


namespace RecommendationGUI.ViewModels;

/// <summary>
/// ViewModel used for displaying recommendations provided by a single origin course.
/// </summary>
/// <param name="orgCourse">Origin course.</param>
/// <param name="orgChance">Original graduation chance estimated with origin course.</param>
/// <param name="actionsRec">List of action recommendations.</param>
/// <param name="coursesRec">List of course recommendations.</param>
public class RecommendationViewModel(Course orgCourse, 
                                     float orgChance, 
                                     List<Recommendation> actionsRec, 
                                     List<Recommendation> coursesRec) : ViewModelBase
{
    /// <summary>
    /// Maps label to its series color in charts.
    /// </summary>
    private static readonly Dictionary<string, SKColor> _seriesColors = [];

    /// <summary>
    /// Original chance of graduation, as estimated with `OrgCourse`.
    /// </summary>
    public float GraduationChance { get; } = orgChance;

    /// <summary>
    /// Origin course from which the prediction was made.
    /// </summary>
    public Course OrgCourse { get; } = orgCourse;

    /// <summary>
    /// ViewModel for action recommendations textual display.
    /// </summary>
    public SuggestionsViewModel ActionSuggestionsViewModel { get; } = new("Genral Suggestions", actionsRec);

    /// <summary>
    /// ViewModel for course recommendations textual display.
    /// </summary>
    public SuggestionsViewModel CourseSuggestionsViewModel { get; } = new("Course Suggestions", coursesRec);

    /// <summary>
    /// ViewModel for action recommendations chart view.
    /// </summary>
    public RecChartViewModel ActionsChartViewModel { get; } = 
        new("Recommended Actions", CreatePieSeries(orgChance, actionsRec));

    /// <summary>
    /// ViewModel for course recommendations chart view.
    /// </summary>
    public RecChartViewModel CoursesChartViewModel { get; } =
        new("Recommended Courses", CreatePieSeries(orgChance, coursesRec));

    /// <summary>
    /// Creates a series for recommendations' chart.
    /// </summary>
    /// <param name="orgChance">Origin course from which estimations are made.</param>
    /// <param name="recommendations">List of recommendations to turn into a series</param>
    /// <returns>Result series for pie chart.</returns>
    private static IEnumerable<ISeries> CreatePieSeries(float orgChance, List<Recommendation> recommendations)
    {
        List<GaugeItem> items = [];

        // add the original graduation chance as the base slice
        items.Add(new(orgChance, series => SetStyle("Original Chance", series)));

        // add each recommendation as a layer
        foreach (var rec in recommendations)
        {
            // calculate new chance based on improve rate
            float newChance = orgChance + rec.ImproveRate;

            newChance = Math.Max(Math.Min(newChance, 100), 0); // ensure no overflow in chart in extreme cases
            items.Add(new(newChance, series => SetStyle(rec.Description, series)));
        }

        items.Add(new(GaugeItem.Background, series => series.InnerRadius = 20));

        return GaugeGenerator.BuildSolidGauge([..items]);
    }

    /// <summary>
    /// Sets style of a recommendation's series.
    /// </summary>
    /// <param name="name">Series' name.</param>
    /// <param name="series">Series to set styles for.</param>
    private static void SetStyle(string name, PieSeries<ObservableValue> series)
    {
        series.Name = name;
        series.DataLabelsPosition = PolarLabelsPosition.Start;
        series.DataLabelsFormatter = point => GenerateDisplayLabel(point.Context.Series.Name!);
        series.InnerRadius = 20;
        series.RelativeOuterRadius = 8;
        series.RelativeInnerRadius = 8;

        // assign color based on series name (label)
        series.Fill = new SolidColorPaint(GetColorForLabel(name));
    }

    /// <summary>
    /// Given a series' name, generates a (potentially) shortened version for compact display.
    /// </summary>
    /// <param name="name">Series name.</param>
    /// <returns>A (potentially) shortened version of `name` for compact display.</returns>
    private static string GenerateDisplayLabel(string name)
    {
        string[] words = name.Split([' ']);

        int andCount = 0;
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Equals("and"))
            {
                words[i] = "&";
                andCount++;
            }
        }

        if (words[1].Equals("nighttime") || words[1].Equals("daytime"))
            return string.Join(" ", words.Take(2 + andCount));
        return string.Join(" ", words.Take(3 + andCount));
    }

    /// <summary>
    /// Color of charts' background.
    /// </summary>
    private static readonly SKColor CHART_BACKGROUND = new(246, 246, 246);

    /// <summary>
    /// Gets color for a given label.
    /// </summary>
    /// <param name="label">Label to get color for.</param>
    /// <returns>Color for label.</returns>
    private static SKColor GetColorForLabel(string label)
    {
        // check if the label already has an assigned color
        if (!_seriesColors.TryGetValue(label, out SKColor resColor))
        {
            // if not, generate a new random color and save it for future use
            resColor = ColorUtils.GetRandomChartColor(background: CHART_BACKGROUND);
            _seriesColors[label] = resColor;
        }

        return resColor;
    }
}
