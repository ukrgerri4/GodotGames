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

        ActivatePlayer((int)Device.Keyboard);

    }

    public void ActivatePlayer(int deviceId)
    {
        _player.ProcessMode = ProcessModeEnum.Inherit;
        _player.Visible = true;
        _player.DeviceId = deviceId;
    }
}
