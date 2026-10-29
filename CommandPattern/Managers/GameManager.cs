using command_pattern.CommandPattern.Core;
using command_pattern.CommandPattern.Objects;

namespace command_pattern.CommandPattern.Managers;

public class GameManager
{
    private Player _player;
    
    public GameManager()
    {
        _player = new(new(Globals.GameWindow.X / 2, Globals.GameWindow.Y / 2));
    }

    public void Update()
    {
        _player.Update();
    }

    public void Draw()
    {
        _player.Draw();
    }
}