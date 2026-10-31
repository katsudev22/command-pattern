using command_pattern.CommandPattern.Objects;
using Microsoft.Xna.Framework;

namespace command_pattern.CommandPattern.Command;

public class MoveToRightCommand : ICommand
{
    public MoveToRightCommand() { }
    public Vector2 PreviousPosition;
    
    public void Execute(GameActor actor)
    {
        PreviousPosition = actor.Position;
        Vector2 direction = new(1, 0);
        actor.Position += direction * actor.Speed;
    }

    public void Undo(GameActor actor)
    {
        actor.Position = PreviousPosition;
    }
}