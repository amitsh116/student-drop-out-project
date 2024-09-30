using RecommendationGUI.Models;
using RecommendationGUI.Models.StudentFeatures;
using RecommendationGUI.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace RecommendationGUI.BackEnd;

/// <summary>
/// Connects front end to application's python back end.
/// </summary>
public class PythonBackEnd : IBackEnd
{
    /// <summary>
    /// Temporary path used for exchanging output from CLI to GUI.
    /// </summary>
    private static readonly string TEMP_JSON_PATH = "cli_out.json";

    /// <summary>
    /// Temporary path used for exchanging error logging from CLI to GUI.
    /// </summary>
    private static readonly string TEMP_ERROR_PATH = "cli_error.txt";

    /// <summary>
    /// Ensures `TEMP_JSON_PATH` doesn't already exist.
    /// </summary>
    static PythonBackEnd()
    {
        // ensure temp files don't exist already

        if (File.Exists(TEMP_JSON_PATH))
            File.Delete(TEMP_JSON_PATH);

        if (File.Exists(TEMP_ERROR_PATH))
            File.Delete(TEMP_ERROR_PATH);
    }

    /// <summary>
    /// Retreives CLI error message.
    /// </summary>
    /// <returns>Retreived CLI error message, or `null` if not found.</returns>
    private static string? RetreiveCliError()
    {
        string? msg = null;
        if (File.Exists(TEMP_ERROR_PATH))
        {
            msg = File.ReadAllText(TEMP_ERROR_PATH);
            File.Delete(TEMP_ERROR_PATH);
        }
        return msg;
    }

    /// <summary>
    /// Retreives CLI's output.
    /// </summary>
    /// <typeparam name="T">CLI output type (to be parsed from JSON).</typeparam>
    /// <param name="script">Name of script to run (without ".py" extention).</param>
    /// <param name="argsDict">Dictionary of args to provide the CLI with.</param>
    /// <returns>CLI's output.</returns>
    /// <exception cref="BackEndException">In case of a backend error.</exception>
    private static T RetreiveFromCli<T>(string script, Dictionary<string, object>? argsDict = null)
    {
        // add OutputJson=TEMP_JSON_PATH to args dict
        argsDict ??= []; // ensure not null
        argsDict[CliArgs.OutputJson] = TEMP_JSON_PATH;

        // run CLI script
        int exitCode = ScriptUtils.RunPythonScript(script, argsDict);
        if (0 != exitCode)
            throw new BackEndException(RetreiveCliError() ?? "Unknown error.");

        // read JSON output
        string json = File.ReadAllText(TEMP_JSON_PATH);

        // deserialize JSON into CoursesCliJsonOutput
        var res = JsonUtils.DeserializeJson<T>(json)!;

        // delete JSON output file
        File.Delete(TEMP_JSON_PATH);

        // return read result
        return res;
    }

    public List<Course> GetAllCourses()
    {
        List<Course> res = [];
        var cliOutput = CliGetAllCourses();
        foreach (var (course, isEvening) in cliOutput.Courses)
        {
            res.Add(new(
                id: course.ID,
                name: course.Name,
                time: isEvening ? DaytimeEveningAttendance.Evening : DaytimeEveningAttendance.Daytime
            ));
        }
        return res;
    }

    /// <summary>
    /// Retreives all courses using python CLI.
    /// </summary>
    /// <returns>CLI's output of all courses.</returns>
    private static CoursesCliJsonOutput CliGetAllCourses()
        => RetreiveFromCli<CoursesCliJsonOutput>("get_all_courses");

    public Tuple<float, List<Recommendation>, List<Recommendation>>
        Recommend(Student student, Course orgCourse, int numActionsRec = 5, int numCoursesRec = 5)
    {
        // retreive outputs from CLI, go for each
        var cliOutput = CliRecommend(student, orgCourse, numActionsRec, numCoursesRec);
            
        // retreive action recommendations list
        List<Recommendation> actionsRec = [];
        foreach (var (description, improveRate) in cliOutput.ActionsRec)
            actionsRec.Add(new(description, improveRate));

        // retreive course recommendations list
        List<Recommendation> coursesRec = [];
        foreach (var (description, improveRate) in cliOutput.CoursesRec)
            coursesRec.Add(new(description, improveRate));

        return Tuple.Create(cliOutput.OrgChance, actionsRec, coursesRec);
    }

    /// <summary>
    /// Retreives recommendations from python CLI.
    /// </summary>
    /// <param name="student">Student to retreive recommendations for.</param>
    /// <param name="orgCourse">Origin course to recommend based on.</param>
    /// <param name="numActionsRec">Max count of action recommendations to generate.</param>
    /// <param name="numCoursesRec">Max count of course recommendations to generate.</param>
    /// <returns>CLI's output of each origin course and its graduation chance and recommendations</returns>
    private static RecsCliJsonOutput
        CliRecommend(Student student, Course orgCourse, int numActionsRec, int numCoursesRec)
    {
        // convert student to args dicts, work on each args dict
        var argsDict = student.ToCliArgsDict(orgCourse);

        // add non-student args to args dict
        argsDict[CliArgs.OutputJson] = TEMP_JSON_PATH;
        argsDict[CliArgs.NumActionsRec] = numActionsRec;
        argsDict[CliArgs.NumCoursesRec] = numCoursesRec;

        // retreive from CLI and add to res
        return RetreiveFromCli<RecsCliJsonOutput>("recommendation", argsDict);
    }
}
