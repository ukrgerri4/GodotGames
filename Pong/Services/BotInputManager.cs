using System;
using Godot;

public class BotInputManager : IPlayerInputManager
{
	public Device Device { get; set; }
	public float Deadzone { get; set; } = 0.15f;

	public BotInputManager(Device device)
	{
		Device = device;
	}

	public bool IsPauseButtonPressed() => false;
	public bool IsRocketLaunchButtonPressed() => false;
	public bool IsRotateButtonPressed() => false;
	public bool IsPadAccelerateButtonPressed() => false;
	public float GetRightXStrength() => 0;
	public float GetRightYStrength() => 0;

	public float GetLeftXStrength()
	{
		return 1;
	}

	public float GetLeftYStrength()
	{
		return 1;
	}

}