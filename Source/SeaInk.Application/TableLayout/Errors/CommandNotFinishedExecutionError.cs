using FluentResults;

namespace SeaInk.Application.TableLayout.Errors
{
    public class CommandNotFinishedExecutionError : Error
    {
        public CommandNotFinishedExecutionError()
            : base("Command did not finish it's execution") { }
    }
}