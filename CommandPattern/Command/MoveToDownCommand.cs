using command_pattern.CommandPattern.Objects;
using Microsoft.Xna.Framework;

namespace command_pattern.CommandPattern.Command;

public class MoveToDownCommand : ICommand
{
    public MoveToDownCommand() { }
    
    public void Execute(GameActor actor)
    {
        Vector2 direction = new(0,1);
        actor.Position += direction * actor.Speed;
    }
}