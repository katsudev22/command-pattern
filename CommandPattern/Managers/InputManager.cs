using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace command_pattern.CommandPattern.Managers;

public class InputManager
{
    public static InputManager Instance { get; private set; }

    private MouseState _currentMouseState;
    private KeyboardState _currentKeyboardState;
    private KeyboardState _previousKeyboardState;
    
    private InputManager() { }
    
    public static void Initialize()
    {
        if (Instance is null) Instance = new();
    }

    public static void Update()
    {
        if (Instance is null) return;

        Instance._previousKeyboardState = Instance._currentKeyboardState;
        Instance._currentKeyboardState = Keyboard.GetState();
        Instance._currentMouseState = Mouse.GetState();
    }

    public static bool IsKeyPressed(Keys key)
    {
        if (Instance is null) return false;
        return Instance._currentKeyboardState.IsKeyDown(key) &&
               Instance._previousKeyboardState.IsKeyUp(key);   
    }

    public static bool IsKeyDown(Keys key)
    {
        if (Instance is null) return false;
        return Instance._currentKeyboardState.IsKeyDown(key);
    }

    public static Point GetMousePosition() =>
        Instance._currentMouseState.Position;
}