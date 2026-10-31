using System;
using System.Collections.Generic;
using System.Linq;
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
    private bool _isUndoing = false;
    
    private Stack<ICommand> _executedHistory = new();
    
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
        _delayTimer += Globals.Time;
        UpdateBounds();

        if (_isUndoing)
        {
            ProcessUndoingQueue();
            return;
        }

        if (_isExecuting)
        {
            ProcessExecutionQueue();
            return;
        }
        
        InputHandler();
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

        if (InputManager.IsKeyPressed(Keys.Space) && !_isUndoing)
        {
            _isExecuting = true;
            _delayTimer = _delayBetweenCommands;
        }

        if (InputManager.IsKeyPressed(Keys.Z) && !_isExecuting)
        {
            _isUndoing = true;
            _delayTimer = _delayBetweenCommands;
        }
    }

    private void ProcessExecutionQueue()
    {
        if (_delayTimer >= _delayBetweenCommands)
        {
            if (Actions.Count == 0)
            {
                _isExecuting = false;
                return;
            }
            
            if (_isExecuting)
            {
                ICommand command = Actions[0];
                command.Execute(this);
                _executedHistory.Push(command);
                Actions.RemoveAt(0);
                _delayTimer = 0f;
            } 
        }
    }

    private void ProcessUndoingQueue()
    {
        if (_delayTimer >= _delayBetweenCommands)
        {
            if (_executedHistory.Count == 0)
            {
                _isUndoing = false;
                return;
            }

            if (_isUndoing)
            {
                ICommand command = _executedHistory.First();
                command.Undo(this);
                _executedHistory.Pop();
                _delayTimer = 0f;
            }
        }
    }
}