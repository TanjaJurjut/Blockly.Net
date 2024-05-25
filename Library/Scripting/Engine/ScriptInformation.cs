using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BlocklyNet.Scripting.Engine;

/// <summary>
/// Describe a script.
/// </summary>
public class ScriptInformation
{
    /// <summary>
    /// The unique identifier of the script while it is active.
    /// </summary>
    [NotNull, Required]
    public required string JobId { get; set; }

    /// <summary>
    /// The name of the script.
    /// </summary>
    [NotNull, Required]
    public required string Name { get; set; }

    /// <summary>
    /// The type of the script.
    /// </summary>
    [NotNull, Required]
    public required string ModelType { get; set; }
}
