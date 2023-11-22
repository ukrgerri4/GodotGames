using Godot;
using System;

public partial class PlayerSection : Node2D
{
	[Export]
	public int PlayerId { get; set; }

	[Export]
	public bool IsStub { get; set; } = false;

	private Player _player;

	public override void _Ready()
	{
		_player = GetNode<Player>("Player");
		_player.Id = PlayerId;

		if (PlayerId == 1)
		{
			ActivatePlayer(Device.Keyboard);
		}
		else
		{
			_player.IsBot = true;
		}
	}

	public void ActivatePlayer(Device device)
	{
		_player.Device = device;
	}
}
