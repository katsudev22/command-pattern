using command_pattern.CommandPattern.Objects;
using Microsoft.Xna.Framework;

namespace command_pattern.CommandPattern.Command;

public class MoveToLeftCommand : ICommand
{
    public MoveToLeftCommand() { }

    public Vector2 PreviousPosition;
    
    public void Execute(GameActor actor)
    {
        PreviousPosition = actor.Position;
        Vector2 direction = new(-1,0);
        actor.Position += direction * actor.Speed;
    }

    public void Undo(GameActor actor)
    {
        actor.Position = PreviousPosition;
    }
}