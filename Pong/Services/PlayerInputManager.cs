using Godot;

public class PlayerInputManager : IPlayerInputManager
{
    public Device Device { get; set; }
    public float Deadzone { get; set; } = 0.15f;

    private int DeviceId => (int)Device;
    private bool IsKeyboardDevice => DeviceId < 0;

    public PlayerInputManager(Device device)
    {
        Device = device;
    }

    public bool IsPauseButtonPressed()
    {
        if (IsKeyboardDevice)
        {
            return Input.IsActionPressed(InputAction.GamePause);
        }
        else
        {
            return Input.IsJoyButtonPressed(DeviceId, JoyInputMap.GamePause);
        }
    }

    public bool IsRocketLaunchButtonPressed()
    {
        if (IsKeyboardDevice)
        {
            return Input.IsActionPressed(InputAction.RocketLaunch);
        }
        else
        {
            return Input.IsJoyButtonPressed(DeviceId, JoyInputMap.RocketLaunch);
        }
    }

    public bool IsRotateButtonPressed()
    {
        if (IsKeyboardDevice)
        {
            return Input.IsActionPressed(InputAction.MapRotate);
        }
        else
        {
            return Input.IsJoyButtonPressed(DeviceId, JoyInputMap.MapRotate);
        }
    }

    public bool IsPadAccelerateButtonPressed()
    {
        if (IsKeyboardDevice)
        {
            return Input.IsActionPressed(InputAction.PadAccelerate);
        }
        else
        {
            return Input.IsJoyButtonPressed(DeviceId, JoyInputMap.PadAccelerate);
        }
    }

    public float GetLeftXStrength()
    {
        if (IsKeyboardDevice)
        {
            return Input.GetActionStrength(InputAction.MoveRight) - Input.GetActionStrength(InputAction.MoveLeft);
        }
        else
        {
            var strength = Input.GetJoyAxis(DeviceId, JoyAxis.LeftX);
            if (Mathf.Abs(strength) > Deadzone)
            {
                return strength;
            }

            if (Input.IsJoyButtonPressed(DeviceId, JoyInputMap.MoveRight))
            {
                return 1;
            }
            else if (Input.IsJoyButtonPressed(DeviceId, JoyInputMap.MoveLeft))
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

    public float GetLeftYStrength()
    {
        if (IsKeyboardDevice)
        {
            return Input.GetActionStrength(InputAction.MoveDown) - Input.GetActionStrength(InputAction.MoveUp);
        }
        else
        {
            var strength = Input.GetJoyAxis(DeviceId, JoyAxis.LeftY);
            if (Mathf.Abs(strength) > Deadzone)
            {
                return strength;
            }

            if (Input.IsJoyButtonPressed(DeviceId, JoyInputMap.MoveDown))
            {
                return 1;
            }
            else if (Input.IsJoyButtonPressed(DeviceId, JoyInputMap.MoveUp))
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

    public float GetRightXStrength()
    {
        if (IsKeyboardDevice)
        {
            return Input.GetActionStrength(InputAction.RocketRight) - Input.GetActionStrength(InputAction.RocketLeft);
        }
        else
        {
            var strength = Input.GetJoyAxis(DeviceId, JoyAxis.RightX);
            return Mathf.Abs(strength) > Deadzone ? strength : 0;
        }
    }

    public float GetRightYStrength()
    {
        if (IsKeyboardDevice)
        {
            return Input.GetActionStrength(InputAction.RocketDown) - Input.GetActionStrength(InputAction.RocketUp);
        }
        else
        {
            var strength = Input.GetJoyAxis(DeviceId, JoyAxis.RightY);
            return Mathf.Abs(strength) > Deadzone ? strength : 0;
        }
    }
}