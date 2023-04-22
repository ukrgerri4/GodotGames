using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public ControllerType ControllerType;
	private PackedScene _roocketTemplate;
	private MapEventsBus _mapEventsBus;
	public int JoyPadId = -1;
	public string Nickname;

	public float PanelWidth
	{
		get
		{
			return ((RectangleShape2D)GetNode<CollisionShape2D>("CollisionShape2D").Shape).Size.X;
		}
	}

	public float Speed = 300f;

	public override void _Ready()
	{
		ControllerType = ControllerType.Keyboard;
		_roocketTemplate = GD.Load<PackedScene>("res://Scenes/Rocket/Rocket.tscn");
	}

	public override void _Process(double delta)
	{

	}

	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionJustPressed("rocket_launch"))
		{
			LaunchRocket();
		}

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
		motion = motion * Speed * (float)delta;
		if (Input.IsActionPressed("accelerate"))
		{
			motion = motion * 2;
		}
		// TODO: handle collision
		MoveAndCollide(motion);
	}

	public void TouchedByBall()
	{
		// add points
		// activete post hit ball
	}

	private void OnMapRotating(MapRotateState state)
	{
		if (state == MapRotateState.Started)
		{
			GetTree().Paused = true;
		}
		else if (state == MapRotateState.Ended)
		{
			GetTree().Paused = false;
		}
	}

	private void LaunchRocket()
	{
		var rocket = _roocketTemplate.Instantiate<Rocket>();
		AddChild(rocket);
		rocket.TopLevel = true;
		rocket.GlobalPosition = new Vector2(400, 400);
	}
}
