using System.Collections.Generic;
using command_pattern.CommandPattern.Command;
using Microsoft.Xna.Framework;

namespace command_pattern.CommandPattern.Objects;

public abstract class GameActor
{
    public Vector2 Position { get; set; }
    public float Speed { get; set; }
    public List<ICommand> Actions { get; set; }
}