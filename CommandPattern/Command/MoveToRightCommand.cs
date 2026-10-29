using command_pattern.CommandPattern.Objects;
using Microsoft.Xna.Framework;

namespace command_pattern.CommandPattern.Command;

public class MoveToRightCommand : ICommand
{
    public MoveToRightCommand() { }

    public void Execute(GameActor actor)
    {
        Vector2 direction = new(1, 0);
        actor.Position += direction * actor.Speed;
    }
}