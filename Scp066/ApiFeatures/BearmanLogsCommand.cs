using System;
using CommandSystem;

namespace Scp066.ApiFeatures;

[CommandHandler(typeof(GameConsoleCommandHandler))]
public class BearmanLogs999 : ICommand
{
    public string Command => "bearmanlogs999";

    public string[] Aliases { get; } = ["bmlogs999"];

    public string Description => "Sends collected plugin logs to the log server and returns the log id.";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        var getLogHistory = LogManager.GetLogHistory();
        response = getLogHistory.logResult;
        return getLogHistory.success;
    }
}