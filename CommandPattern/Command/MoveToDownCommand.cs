using command_pattern.CommandPattern.Objects;
using Microsoft.Xna.Framework;

namespace command_pattern.CommandPattern.Command;

public class MoveToDownCommand : ICommand
{
    public MoveToDownCommand() { }

    public Vector2 PreviousPosition;
    
    public void Execute(GameActor actor)
    {
        PreviousPosition = actor.Position;
        Vector2 direction = new(0,1);
        actor.Position += direction * actor.Speed;
    }

    public void Undo(GameActor actor)
    {
        actor.Position = PreviousPosition;
    }
}