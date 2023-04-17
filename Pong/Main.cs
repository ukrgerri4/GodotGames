using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Main : Node2D
{
	private Map _map;
	private PackedScene _playerTemplate;
	private List<PlayerDefaultOptions> _playersOptions = new List<PlayerDefaultOptions>();
	private List<Player> _players = new List<Player>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_playerTemplate = GD.Load<PackedScene>("res://Scenes/Player/Player.tscn");
		_map = GetNode<Map>("Map");
		_playersOptions.AddRange(new[] {
			new PlayerDefaultOptions { Nickname = "Trooper1", AppearPosition = new Vector2(0,536), JoyPadId = -1, Stub = null },
			new PlayerDefaultOptions { Nickname = "Trooper2", AppearPosition = new Vector2(-536,0), ApperRotationAngle = 90, JoyPadId = 0, Stub = GetNode<StaticBody2D>("Map/Player2Stub") },
			new PlayerDefaultOptions { Nickname = "Trooper3", AppearPosition = new Vector2(0,-536), JoyPadId = 1, Stub = GetNode<StaticBody2D>("Map/Player3Stub") },
			new PlayerDefaultOptions { Nickname = "Trooper4", AppearPosition = new Vector2(536,0), ApperRotationAngle = 90, JoyPadId = 2, Stub = GetNode<StaticBody2D>("Map/Player4Stub") },
		});

		CallDeferred(nameof(AddPlayer), (int)ControllerType.Keyboard, -1);
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventJoypadButton joypadButtonEvent)
		{
			if (!_players.Any(x => x.JoyPadId == joypadButtonEvent.Device))
			{
				CallDeferred(nameof(AddPlayer), (int)ControllerType.JoyPad, joypadButtonEvent.Device);
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	private void AddPlayer(ControllerType controllerType, int joyPadId)
	{
		var playerOptions = _playersOptions.First(x => x.JoyPadId == joyPadId);
		var player = _playerTemplate.Instantiate<Player>();
		player.Nickname = playerOptions.Nickname;
		player.ControllerType = controllerType;
		player.JoyPadId = playerOptions.JoyPadId;
		player.Position = playerOptions.AppearPosition;
		player.Rotation = Mathf.DegToRad(playerOptions.ApperRotationAngle);
		_map.AddChild(player);
		if (playerOptions.Stub is not null)
		{
			playerOptions.Stub.QueueFree();

		}
		_players.Add(player);
	}
}
