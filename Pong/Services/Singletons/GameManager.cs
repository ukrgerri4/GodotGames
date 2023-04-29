using System;
using Godot;

public partial class GameManager : Node
{
	private Map _map;
	private PackedScene _roocketTemplate;
	private PackedScene _modifier;


	public override void _Ready()
	{
		_roocketTemplate = GD.Load<PackedScene>("res://Scenes/Rocket/Rocket.tscn");
		_modifier = GD.Load<PackedScene>("res://Scenes/Modifiers/Modifier.tscn");

		_map = GetNode<Map>("/root/Main/Game/Map");
	}

	public void CreateModifier(SimpleBlock block)
	{
		var modifier = _modifier.Instantiate<Modifier>();
		modifier.GlobalPosition = block.GlobalPosition;
		modifier.Init(block.LastTouchedPlayer.ItemFallDirection);
		_map.AddChild(modifier);
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