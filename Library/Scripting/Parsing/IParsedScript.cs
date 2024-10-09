using BlocklyNet.Core.Model;
using BlocklyNet.Scripting.Engine;

namespace BlocklyNet.Scripting.Parsing;

/// <summary>
/// 
/// </summary>
public interface IParsedScript
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="presets"></param>
    /// <param name="engine"></param>
    /// <returns></returns>
    Task<object?> EvaluateAsync(Dictionary<string, object?> presets, IScriptSite engine);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <returns></returns>
    Task<object?> RunAsync(IScriptSite engine);

    /// <summary>
    /// 
    /// </summary>
    Task<int> GetGroupTreeAsync();
}
