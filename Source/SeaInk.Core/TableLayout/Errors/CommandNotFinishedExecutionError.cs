using FluentResults;

namespace SeaInk.Core.TableLayout.Errors
{
    public class CommandNotFinishedExecutionError : Error
    {
        public CommandNotFinishedExecutionError()
            : base("Command did not finish it's execution") { }
    }
}