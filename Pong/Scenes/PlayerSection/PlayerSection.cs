using Godot;
using System;

public partial class PlayerSection : Node2D
{
	[Export]
	public int PlayerId { get; set; }

	[Export]
	public bool IsStub { get; set; } = false;

	private IPlayer _player;

	public override void _Ready()
	{
		_player = GetNode<IPlayer>("Player");
		_player.Id = PlayerId;

		if (PlayerId == 1)
		{
			ActivatePlayer(Device.Keyboard);
		}
		else
		{
			_player.InitBotInput();
		}
	}

	public void ActivatePlayer(Device device)
	{
		_player.Device = device;
	}
}
