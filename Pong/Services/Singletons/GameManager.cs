using Godot;

public partial class GameManager : Node
{
	private Node2D _levelObjects;
	private Node2D _balls;
	public PackedScene RocketTemplate;
	public PackedScene ModifierTemplate;
	public PackedScene BallTemplate;
	public PackedScene Level0;

	public override void _Ready()
	{
		RocketTemplate = GD.Load<PackedScene>("res://Scenes/Rocket/Rocket.tscn");
		ModifierTemplate = GD.Load<PackedScene>("res://Scenes/Modifiers/Modifier.tscn");
		BallTemplate = GD.Load<PackedScene>("res://Scenes/Ball/Ball.tscn");
		Level0 = GD.Load<PackedScene>("res://Scenes/Levels/Level0/Level0.tscn");

		_levelObjects = GetNode<Node2D>("/root/Main/Game/LevelObjects");
		_balls = GetNode<Node2D>("/root/Main/Game/Balls");
	}

	public void AddLevel()
	{
		var children = _levelObjects.GetChildren();
		foreach (var child in children)
		{
			child.QueueFree();
		}

		_levelObjects.AddChild(Level0.Instantiate<Node2D>());
	}

	public void AddModifier(SimpleBlock block)
	{
		var modifier = ModifierTemplate.Instantiate<Modifier>();
		modifier.GlobalPosition = block.GlobalPosition;
		modifier.Init(block.LastTouchedPlayer.ItemFallDirection); // TODO: add random direction if no last player
		_levelObjects.AddChild(modifier);
	}

	public void AddBall()
	{
		var balls = _balls.GetChildren();
		if (balls.Count > 0)
		{
			var ball = BallTemplate.Instantiate<Ball>();
			ball.GlobalPosition = ((Ball)balls[0]).GlobalPosition;
			ball.MoveVelocity = ((Ball)balls[0]).MoveVelocity * -1;
			_balls.CallDeferred(Node.MethodName.AddChild, ball);
		}
	}

	// public Rocket LaunchRocket(Player player)
	// {
	// 	var rocket = _roocketTemplate.Instantiate<Rocket>();
	// 	rocket.TopLevel = true;
	// 	rocket.GlobalRotationDegrees = player.GlobalRotationDegrees;
	// 	rocket.GlobalPosition = player.GlobalPosition * 0.9f;
	// 	rocket.Init(player.InputManager, (player.Marker2D.GlobalPosition - player.GlobalPosition).Normalized());
	// 	_map.AddChild(rocket);
	// 	return rocket;
	// }

	public void AddRocket(Rocket rocket)
	{
		_levelObjects.AddChild(rocket);
	}
}