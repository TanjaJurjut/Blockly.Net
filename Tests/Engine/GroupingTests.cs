using BlocklyNet.Scripting.Engine;
using BlocklyNet.Scripting.Generic;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BlocklyNetTests.Engine;

[TestFixture]
public class GroupingTests : TestEnvironment
{
    /* Just request some user input. */
    private const string Script1 = @"
        <xml xmlns=""https://developers.google.com/blockly/xml"">
        <block type=""execute_group"" id=""Outer"">
            <value name=""RESULT"">                
                <block type=""group_execution_result"" id=""Outer.Result"">
                    <value name=""Result"">                
                        <block type=""text"" id=""Outer.Result.Value"">
                            <field name=""TEXT"">1</field>
                        </block>
                    </value>
                    <value name=""Type"">                
                        <block type=""group_execution_result_type"" id=""Outer.Result.Value.Type"">
                            <field name=""VALUE"">Success</field>
                        </block>
                    </value>
                </block>
            </value>
            <next>
                <block type=""execute_group"" id=""Inner"">
                    <value name=""RESULT"">                
                        <block type=""group_execution_result"" id=""Inner.Result"">
                            <value name=""Result"">                
                                <block type=""text"" id=""Inner.Result.Value"">
                                    <field name=""TEXT"">2</field>
                                </block>
                            </value>
                            <value name=""Type"">                
                                <block type=""group_execution_result_type"" id=""Inner.Result.Value.Type"">
                                    <field name=""VALUE"">Failed</field>
                                </block>
                            </value>
                        </block>
                    </value>
                </block>
            </next>
        </block>
        </xml>";

    /// <inheritdoc/>
    protected override void OnSetup(IServiceCollection services)
    {
        base.OnSetup(services);

        /* Register the broadcast sink. */
        services.AddSingleton<IScriptEngineNotifySink, Sink>();
    }

    [Test]
    public async Task Can_Run_Groups_In_Sequence_Async()
    {
        /* Termination helper. */
        var done = new TaskCompletionSource();

        ((Sink)GetService<IScriptEngineNotifySink>()).OnEvent = (method, arg) =>
        {
            /* See if script is done. */
            if (method == ScriptEngineNotifyMethods.Done)
                done.SetResult();
            else if (method == ScriptEngineNotifyMethods.Error)
                done.SetResult();
        };

        var jobId = await Engine.StartAsync(new StartGenericScript { Name = "Run Groups", ScriptId = AddScript("SCRIPT", Script1) }, "");

        /* Wait for the script to finish. */
        await done.Task;

        /* Check the result. */
        var result = (GenericResult)Engine.FinishScriptAndGetResult(jobId)!;

        Assert.That((IList<object?>)result.Result, Has.Count.EqualTo(2));
    }
}