using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RecommendationGUI.Utils;

/// <summary>
/// Static class holding command-line related utilities.
/// </summary>
public static class ScriptUtils
{
    /// <summary>
    /// Path where CLI scripts are located.
    /// </summary>
    public static string ScriptsFolder { get; set; } = ".";

    /// <summary>
    /// Runs given command-line command in the background.
    /// </summary>
    /// <param name="command">Command-line command to run.</param>
    /// <returns>Exit code of the process.</returns>
    public static int RunCommand(string command)
    {
        Process process = new();
        ProcessStartInfo startInfo = new()
        {
            WindowStyle = ProcessWindowStyle.Hidden,
            FileName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "cmd.exe" : "/bin/sh",
            Arguments = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? $"/C {command}" : $"-c \"{command}\""
        };
        process.StartInfo = startInfo;
        process.Start();
        process.WaitForExit();
        return process.ExitCode;
    }

    /// <summary>
    /// Runs given python script with given argument list.
    /// </summary>
    /// <param name="script">Name of script to run (without ".py" extention).</param>
    /// <param name="argsList">List of arguments to run script with.</param>
    public static int RunPythonScript(string script, IEnumerable<string> argsList)
    {
        string pythonCommand = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "python" : "python3";
        return RunCommand($"{pythonCommand} \"{ScriptsFolder}/{script}.py\" {string.Join(" ", argsList)}");
    }

    /// <summary>
    /// Runs given python script with given argument dictionary - each key with "--" prefix.
    /// </summary>
    /// <param name="script">Name of script to run (without ".py" extention).</param>
    /// <param name="argsDict">Arguments dict to run with.</param>
    public static int RunPythonScript(string script, Dictionary<string, object> argsDict)
    {
        List<string> argList = [];
        foreach (var pair in argsDict)
            argList.Add($"--{pair.Key} {pair.Value}");
        return RunPythonScript(script, argList);
    }
}
