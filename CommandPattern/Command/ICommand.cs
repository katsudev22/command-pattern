using command_pattern.CommandPattern.Objects;

namespace command_pattern.CommandPattern.Command;

public interface ICommand
{
    public void Execute(GameActor actor);
}