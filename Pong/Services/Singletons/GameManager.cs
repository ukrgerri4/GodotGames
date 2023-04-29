using Godot;

public partial class GameManager : Node
{
	private Map _map;
	private PackedScene _roocketTemplate;

	public Player PlayerTouchedBallLast { get; set; }

	public override void _Ready()
	{
		_map = GetNode<Map>("/root/Main/Game/Map");
		_roocketTemplate = GD.Load<PackedScene>("res://Scenes/Rocket/Rocket.tscn");
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
}