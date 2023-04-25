using System.Collections.Generic;
using System.Linq;
using Godot;

public static class InputAction
{
    public static readonly StringName MoveLeft = new StringName("move_left");
    public static readonly StringName MoveRight = new StringName("move_right");
    public static readonly StringName MoveUp = new StringName("move_up");
    public static readonly StringName MoveDown = new StringName("move_down");

    public static readonly StringName Accelerate = new StringName("accelerate");
    public static readonly StringName Pause = new StringName("pause");
    public static readonly StringName MapRotate = new StringName("map_rotate");

    public static readonly StringName RocketLaunch = new StringName("rocket_launch");
    public static readonly StringName RocketLeft = new StringName("rocket_left");
    public static readonly StringName RocketRight = new StringName("rocket_right");
    public static readonly StringName RocketUp = new StringName("rocket_up");
    public static readonly StringName RocketDown = new StringName("rocket_down");

    public static Dictionary<StringName, DefaultInputAction> Actions { get; set; } = new Dictionary<StringName, DefaultInputAction>();

    static InputAction()
    {
        Actions = new Dictionary<StringName, DefaultInputAction> {
            {
                InputAction.MoveLeft,
                new DefaultInputAction {
                    KeyButton = new InputEventKey { PhysicalKeycode = Key.Left },
                    JoyButton = new InputEventJoypadButton { ButtonIndex = JoyButton.DpadLeft },
                    JoyMotion = new InputEventJoypadMotion { Axis = JoyAxis.LeftX }
                }
            },
            {
                InputAction.MoveRight,
                new DefaultInputAction {
                    KeyButton = new InputEventKey { PhysicalKeycode = Key.Right },
                    JoyButton = new InputEventJoypadButton { ButtonIndex = JoyButton.DpadRight },
                    JoyMotion = new InputEventJoypadMotion { Axis = JoyAxis.LeftX }
                }
            },
            {
                InputAction.MoveUp,
                new DefaultInputAction {
                    KeyButton = new InputEventKey { PhysicalKeycode = Key.Up },
                    JoyButton = new InputEventJoypadButton { ButtonIndex = JoyButton.DpadUp },
                    JoyMotion = new InputEventJoypadMotion { Axis = JoyAxis.LeftY }
                }
            },
            {
                InputAction.MoveDown,
                new DefaultInputAction {
                    KeyButton = new InputEventKey { PhysicalKeycode = Key.Down },
                    JoyButton = new InputEventJoypadButton { ButtonIndex = JoyButton.DpadDown },
                    JoyMotion = new InputEventJoypadMotion { Axis = JoyAxis.LeftY }
                }
            },
                        {
                InputAction.Accelerate,
                new DefaultInputAction {
                    KeyButton = new InputEventKey { PhysicalKeycode = Key.Shift },
                    JoyButton = new InputEventJoypadButton { ButtonIndex = JoyButton.RightShoulder }
                }
            }
        };
    }
}