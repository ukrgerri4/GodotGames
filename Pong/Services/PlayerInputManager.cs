using Godot;

public class PlayerInputManager
{
    public int DeviceId { get; set; }
    public float Deadzone { get; set; } = 0.15f;

    public PlayerInputManager(int deviceId)
    {
        DeviceId = deviceId;
    }

    public bool IsPauseButtonPressed()
    {
        if (DeviceId < 0)
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
        if (DeviceId < 0)
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
        if (DeviceId < 0)
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
        if (DeviceId < 0)
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
        if (DeviceId < 0)
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
        if (DeviceId < 0)
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
        if (DeviceId < 0)
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
        if (DeviceId < 0)
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