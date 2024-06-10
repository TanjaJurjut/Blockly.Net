

using BlocklyNet.Core.Model;

namespace BlocklyNet.Core.Blocks.Text;

/// <summary>
/// 
/// </summary>
public class ColourRandom : Block
{
  private readonly Random random = new Random();

  /// <inheritdoc/>
  public override Task<object?> Evaluate(Context context)
  {
    var bytes = new byte[3];

    random.NextBytes(bytes);

    return Task.FromResult((object?)string.Format("#{0}", string.Join("", bytes.Select(x => string.Format("{0:x2}", x)))));
  }
}