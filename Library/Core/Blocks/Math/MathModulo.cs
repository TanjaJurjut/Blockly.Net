

using BlocklyNet.Core.Model;

namespace BlocklyNet.Core.Blocks.Math;

public class MathModulo : Block
{
  public override async Task<object?> Evaluate(Context context)
  {
    var dividend = await Values.Evaluate<double>("DIVIDEND", context);
    var divisor = await Values.Evaluate<double>("DIVISOR", context);

    return dividend % divisor;
  }
}