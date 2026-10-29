using System;
using System.Threading.Tasks;
using command_pattern.CommandPattern.Command;
using command_pattern.CommandPattern.Core;
using command_pattern.CommandPattern.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace command_pattern.CommandPattern.Objects;

public class Player : GameActor
{
    public Texture2D Texture;
    public Rectangle Bounds;

    private float _delayTimer = 0f;
    private float _delayBetweenCommands = 0.8f;
    private bool _isExecuting = false;
    
    public Player(Vector2 position)
    {
        Texture = new(Globals.GraphicsDevice, 1, 1);
        Texture.SetData([Color.IndianRed]);
        Position = position;
        Speed = 200f;
        UpdateBounds();
        Actions = new();
    }

    public void Update()
    {
        UpdateBounds();

        if (!_isExecuting)
            InputHandler();
        else
            ProcessExecutionQueue();
    }

    public void Draw() =>
        Globals.SpriteBatch.Draw(Texture, Position, Bounds, Color.White, 0f, new(25, 25), 1f, SpriteEffects.None, 0f);
    
    private void UpdateBounds()
    {
        int width = 50;
        int height = 50;

        Bounds = new Rectangle(
            (int)Position.X - width / 2,
            (int)Position.Y - height / 2,
            width,
            height
        );
    }

    private void InputHandler()
    {
        if (InputManager.IsKeyPressed(Keys.W))
        { 
            Actions.Add(new MoveToUpCommand());
            Console.WriteLine("W key pressed! MoveUp Registered!");
        }

        if (InputManager.IsKeyPressed(Keys.A))
        {
            Actions.Add(new MoveToLeftCommand());
            Console.WriteLine("A key pressed! MoveLeft Registered!");
        }

        if (InputManager.IsKeyPressed(Keys.S))
        {
            Actions.Add(new MoveToDownCommand());
            Console.WriteLine("S key pressed! MoveDown Registered!");
        }

        if (InputManager.IsKeyPressed(Keys.D))
        {
            Actions.Add(new MoveToRightCommand());
            Console.WriteLine("D key pressed! MoveRight Registered!");
        }

        if (InputManager.IsKeyPressed(Keys.Space))
        {
            _isExecuting = true;
        }
    }

    private void ProcessExecutionQueue()
    {
        _delayTimer += Globals.Time;

        if (_delayTimer >= _delayBetweenCommands)
        {
            if (Actions.Count == 0)
                _isExecuting = false;
            
            if (_isExecuting)
            {
                ICommand command = Actions[0];
                command.Execute(this);
                Actions.RemoveAt(0);
                _delayTimer = 0f;
            } 
        }
    }
}