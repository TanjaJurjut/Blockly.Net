

using BlocklyNet.Core.Model;

namespace BlocklyNet.Core.Blocks.Math;

public class MathRandomFloat : Block
{
  private static readonly Random rand = new Random();

  public override Task<object?> Evaluate(Context context)
  {
    return Task.FromResult((object?)rand.NextDouble());
  }

}