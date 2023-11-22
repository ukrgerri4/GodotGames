using Godot;

public partial class GameManager : Node
{
	private Node2D _mapObjects;
	private Node2D _balls;
	public PackedScene RocketTemplate;
	public PackedScene ModifierTemplate;
	public PackedScene BallTemplate;

	public override void _Ready()
	{
		RocketTemplate = GD.Load<PackedScene>("res://Scenes/Rocket/Rocket.tscn");
		ModifierTemplate = GD.Load<PackedScene>("res://Scenes/Modifiers/Modifier.tscn");
		BallTemplate = GD.Load<PackedScene>("res://Scenes/Ball/Ball.tscn");

		_mapObjects = GetNode<Node2D>("/root/Main/Game/MapObjects");
		_balls = GetNode<Node2D>("/root/Main/Game/Balls");
	}

	public void AddModifier(SimpleBlock block)
	{
		var modifier = ModifierTemplate.Instantiate<Modifier>();
		modifier.GlobalPosition = block.GlobalPosition;
		modifier.Init(block.LastTouchedPlayer.ItemFallDirection); // TODO: add random direction if no last player
		_mapObjects.AddChild(modifier);
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
		_mapObjects.AddChild(rocket);
	}
}