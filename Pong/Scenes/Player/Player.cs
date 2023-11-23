using Godot;
using System;
using System.Linq;

public partial class Player : CharacterBody2D
{
	private Configuration _configuration;
	private EventsBus _eventsBus;
	private GameManager _gameManager;

	private RectangleShape2D _rectangleShape2D;
	private ActionArea _actionArea;
	private Marker2D _marker2D;
	private PlayerInputManager _inputManager;


	public int Id { get; set; } = 0;
	public string NickName { get; set; } = "";
	private Device _device = Device.Keyboard;
	public Device Device
	{
		get => _device;
		set
		{
			_device = value;
			if (_inputManager is not null)
			{
				_inputManager.Device = _device;
			}
		}
	}

	public bool IsBot { get; set; } = false;

	public float PanelWidth
	{
		get
		{
			return _rectangleShape2D.Size.X;
		}
	}

	private float _speed;
	public float Speed
	{
		get => _speed;
		set
		{
			_speed = value;
			_eventsBus.Player.NotifySpeedChanged(Id, _speed);
		}
	}

	private int _score;
	public int Score
	{
		get => _score;
		set
		{
			_score = value;
			_eventsBus.Player.NotifyScoreChanged(Id, _score);
		}
	}

	//TODO refactop to map position based
	public bool IsHorizontalPosition => Mathf.RoundToInt(GlobalRotationDegrees) % 180 == 0;


	private bool _rocketExist = false;

	private bool CanLaunchRocket => !_rocketExist && _actionArea.ActionAllowed;

	public MapPosition MapPosition { get; set; } = MapPosition.Undefined;

	public Vector2 ItemFallDirection => MapPosition switch
	{
		MapPosition.Top => Vector2.Up,
		MapPosition.Down => Vector2.Down,
		MapPosition.Left => Vector2.Left,
		MapPosition.Right => Vector2.Right,
		_ => Vector2.Zero,
	};

	public override void _Ready()
	{
		_configuration = GetNode<Configuration>("/root/Configuration");
		_eventsBus = GetNode<EventsBus>("/root/EventsBus");
		_rectangleShape2D = (RectangleShape2D)GetNode<CollisionShape2D>("CollisionShape2D")?.Shape;
		_actionArea = GetNode<ActionArea>("../ActionArea");
		_marker2D = GetNode<Marker2D>("Marker2D");
		_gameManager = GetNode<GameManager>("/root/GameManager");

		_inputManager = new PlayerInputManager(Device);

		Speed = _configuration.Player.DefaultSpeed;
		Score = _configuration.Player.StartScore;
	}

	public override void _Input(InputEvent @event)
	{
		if (IsBot)
		{
			return;
		}

		if (_inputManager.IsRocketLaunchButtonPressed() && CanLaunchRocket)
		{
			LaunchRocket();
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (IsBot)
		{
			HanleBotMovement(delta);
			return;
		}

		Vector2 motion = GetMotionVector();

		if (motion.LengthSquared() > _inputManager.Deadzone)
		{
			Move(delta, motion);
		}
	}

	private Vector2 GetMotionVector()
	{
		return IsHorizontalPosition
			? new Vector2(_inputManager.GetLeftXStrength(), 0)
			: new Vector2(0, _inputManager.GetLeftYStrength());
	}

	private void Move(double delta, Vector2 motion)
	{
		motion = motion.Normalized();
		motion = motion * Speed * (float)delta;
		if (_inputManager.IsPadAccelerateButtonPressed())
		{
			motion *= 2;
		}
		// TODO: handle collision
		MoveAndCollide(motion);
	}

	public void UpdateScore(int points)
	{
		var value = Score + points;
		Score = value > 0 ? value : 0;
		_eventsBus.Player.NotifyScoreChanged(Id, Score);
	}

	private void LaunchRocket()
	{
		var rocket = _gameManager.RocketTemplate.Instantiate<Rocket>();
		rocket.TopLevel = true;
		rocket.GlobalRotationDegrees = GlobalRotationDegrees;
		rocket.GlobalPosition = GlobalPosition * 0.9f;
		rocket.Init(_inputManager, (_marker2D.GlobalPosition - GlobalPosition).Normalized());
		rocket.TreeExited += OnRocketDestroyed;
		_rocketExist = true;
		_gameManager.AddRocket(rocket);
	}

	private void OnRocketDestroyed()
	{
		GD.Print($"[{Id}] The rocket has been destroyed.");
		_rocketExist = false;
	}

	private void HanleBotMovement(double delta)
	{
		var balls = GetTree().GetNodesInGroup("Balls");

		if (balls is not null && balls.Count > 0)
		{
			Ball firstBall = balls[0] as Ball;

			Vector2 motion;
			if (IsHorizontalPosition)
			{
				motion = new Vector2(firstBall.GlobalPosition.X - GlobalPosition.X, 0);
			}
			else
			{
				motion = new Vector2(0, firstBall.GlobalPosition.Y - GlobalPosition.Y);
			}

			motion = motion.Normalized();
			motion = motion * Speed * (float)delta;
			MoveAndCollide(motion);
		}
	}
}
