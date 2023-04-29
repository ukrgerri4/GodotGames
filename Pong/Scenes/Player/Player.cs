using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private RectangleShape2D _rectangleShape2D;
	private ActionArea _actionArea;
	private Marker2D _marker2D;
	private Map _map;
	private PackedScene _roocketTemplate;
	private PlayerInputManager _inputManager;

	public string Nickname;

	private int _deviceId = (int)Device.Keyboard;
	public int DeviceId
	{
		get => _deviceId;
		set
		{
			_deviceId = value;
			if (_inputManager is not null)
			{
				_inputManager.DeviceId = _deviceId;
			}
		}
	}

	public float PanelWidth
	{
		get
		{
			return _rectangleShape2D.Size.X;
		}
	}

	public float Speed = 400f;

	public bool IsHorizontalPosition => Mathf.RoundToInt(GlobalRotationDegrees) % 180 == 0;

	private int _score = 0;
	private EventsBus _eventsBus;

	public int PlayerId { get; set; } = 0;

	private bool _rocketExist { get; set; } = false;

	private bool CanLaunchRocket => !_rocketExist && _actionArea.ActionAllowed;

	public override void _Ready()
	{
		_eventsBus = GetNode<EventsBus>("/root/EventsBus");
		_rectangleShape2D = (RectangleShape2D)GetNode<CollisionShape2D>("CollisionShape2D")?.Shape;
		_actionArea = GetNode<ActionArea>("../ActionArea");
		_marker2D = GetNode<Marker2D>("Marker2D");
		_map = GetNode<Map>("/root/Main/Game/Map");

		_roocketTemplate = GD.Load<PackedScene>("res://Scenes/Rocket/Rocket.tscn");
		_inputManager = new PlayerInputManager(DeviceId);
	}

	public override void _Input(InputEvent @event)
	{
		if (_inputManager.IsRocketLaunchButtonPressed() && CanLaunchRocket)
		{
			LaunchRocket();
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 motion = Vector2.Zero;
		if (IsHorizontalPosition)
		{
			motion = new Vector2(_inputManager.GetLeftXStrength(), 0);
		}
		else
		{
			motion = new Vector2(0, _inputManager.GetLeftYStrength());
		}

		if (motion.LengthSquared() > 0.15f)
		{
			Move(delta, motion);
		}
	}

	private void Move(double delta, Vector2 motion)
	{
		motion = motion.Normalized();
		motion = motion * Speed * (float)delta;
		if (_inputManager.IsPadAccelerateButtonPressed())
		{
			motion = motion * 2;
		}
		// TODO: handle collision
		MoveAndCollide(motion);
	}

	public void UpdateScore(int points)
	{
		var value = _score + points;
		_score = value > 0 ? value : 0;
		_eventsBus.NotifyPlayerScoreChanged(PlayerId, _score);
	}

	private Rocket LaunchRocket()
	{
		var rocket = _roocketTemplate.Instantiate<Rocket>();
		rocket.TopLevel = true;
		rocket.GlobalRotationDegrees = GlobalRotationDegrees;
		rocket.GlobalPosition = GlobalPosition * 0.9f;
		rocket.Init(_inputManager, (_marker2D.GlobalPosition - GlobalPosition).Normalized());
		rocket.TreeExited += OnRocketDestroyed;
		_rocketExist = true;
		_map.AddChild(rocket);
		return rocket;
	}

	private void OnRocketDestroyed()
	{
		GD.Print("Rocket not exist.");
		_rocketExist = false;
	}
}
