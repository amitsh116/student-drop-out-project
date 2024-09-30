using System.Windows.Input;

namespace RecommendationGUI.Models;

/// <summary>
/// Attaches an `ICommand` to an object.
/// </summary>
/// <typeparam name="T">Type of object to attach an `ICommand` to.</typeparam>
/// <param name="obj">Object to attach an `ICommand` to.</param>
/// <param name="command">`ICommand` instance to attach.</param>
public class AttachedCommand<T>(T obj, ICommand? command = null)
{
    /// <summary>
    /// Object itself.
    /// </summary>
    public T Obj { get; } = obj;

    /// <summary>
    /// Attached command.
    /// </summary>
    public ICommand? Command { get; set; } = command;
}
