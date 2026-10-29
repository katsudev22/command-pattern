using command_pattern.CommandPattern.Objects;
using Microsoft.Xna.Framework;

namespace command_pattern.CommandPattern.Command;

public class MoveToUpCommand : ICommand
{
    public MoveToUpCommand() { }
    
    public void Execute(GameActor actor)
    {
        Vector2 direction = new(0,-1);
        actor.Position += direction * actor.Speed;
    }
}