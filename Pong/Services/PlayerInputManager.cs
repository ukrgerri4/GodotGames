using Godot;

public class PlayerInputManager
{
    public PlayerInputManager(int deviceId)
    {
        DeviceId = deviceId;
    }

    public int DeviceId { get; set; }

    public bool IsPauseButtonPressed()
    {
        if (DeviceId < 0)
        {
            return Input.IsPhysicalKeyPressed(Key.P);
        }
        else
        {
            return Input.IsJoyButtonPressed(DeviceId, JoyButton.Start);
        }
    }

    public bool IsRocketLaunchButtonPressed()
    {
        if (DeviceId < 0)
        {
            return Input.IsPhysicalKeyPressed(Key.Space);
        }
        else
        {
            return Input.IsJoyButtonPressed(DeviceId, JoyButton.Y);
        }
    }

    public bool IsRotateButtonPressed()
    {
        if (DeviceId < 0)
        {
            return Input.IsPhysicalKeyPressed(Key.R);
        }
        else
        {
            return Input.IsJoyButtonPressed(DeviceId, JoyButton.A);
        }
    }

    public bool IsAccelerateButtonPressed()
    {
        if (DeviceId < 0)
        {
            return Input.IsPhysicalKeyPressed(InputAction.Actions[InputAction.Accelerate].KeyButton.Keycode);
        }
        else
        {
            return Input.IsJoyButtonPressed(DeviceId, JoyButton.RightShoulder);
        }
    }

    public float GetLeftXStrength()
    {
        if (DeviceId < 0)
        {
            if (Input.IsPhysicalKeyPressed(Key.D))
            {
                return 1;
            }
            else if (Input.IsPhysicalKeyPressed(Key.A))
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return Input.GetJoyAxis(DeviceId, JoyAxis.LeftX);
        }
    }

    public float GetLeftYStrength()
    {
        if (DeviceId < 0)
        {
            if (Input.IsPhysicalKeyPressed(Key.W))
            {
                return 1;
            }
            else if (Input.IsPhysicalKeyPressed(Key.S))
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return Input.GetJoyAxis(DeviceId, JoyAxis.LeftY);
        }
    }
}