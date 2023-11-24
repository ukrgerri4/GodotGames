using Godot;
using System;
using System.Linq;

public partial class PlayerSection : Node2D
{
	private Configuration _configuration;
	private Player _player;

	[Export]
	public int PlayerId { get; set; }

	[Export]
	public bool IsStub { get; set; } = false;

	public override void _Ready()
	{
		_player = GetNode<Player>("Player");
		_player.Id = PlayerId;
		_player.IsBot = true;
	}
}
