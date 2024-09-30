using System;
using System.Collections.Generic;

namespace RecommendationGUI.BackEnd;

/// <summary>
/// Used for parsing CLI recommendation from JSON.
/// </summary>
internal struct RecsCliJsonOutput
{
    /// <summary>
    /// Original student's estimated graduation chance
    /// </summary>
    public required float OrgChance { get; set; }

    /// <summary>
    /// List of action recommendations (each is description, improveRate).
    /// </summary>
    public required List<Tuple<string, float>> ActionsRec { get; set; }

    /// <summary>
    /// List of course recommendations (each is description, improveRate).
    /// </summary>
    public required List<Tuple<string, float>> CoursesRec { get; set; }
}
