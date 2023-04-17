using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public ControllerType ControllerType;
	public int JoyPadId = -1;
	public string Nickname;

	public float Speed = 500f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ControllerType = ControllerType.Keyboard;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public override void _PhysicsProcess(double delta)
	{
		if (JoyPadId == -1)
		{
			MoveByKeyboard(delta);
		}
		else
		{
			MoveByJoy(delta);
		}
	}

	private void MoveByKeyboard(double delta)
	{
		var motion = new Vector2(Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left"), 0);
		Move(delta, motion);
	}

	private void MoveByJoy(double delta)
	{
		Vector2 motion = Vector2.Zero;
		if (RotationDegrees != 0)
		{
			motion.Y = Input.GetJoyAxis(JoyPadId, JoyAxis.LeftY);
		}
		else
		{
			motion.X = Input.GetJoyAxis(JoyPadId, JoyAxis.LeftX);
		}

		if (motion.LengthSquared() > 0.05)
		{
			Move(delta, motion);
		}
	}

	private void Move(double delta, Vector2 motion)
	{
		motion = motion.Normalized();
		MoveAndCollide(motion * Speed * (float)delta);
	}
}
