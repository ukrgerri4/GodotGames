using Godot;
using System;

public partial class PlayerSection : Node2D
{
    private Player _player;
    private PlayerStub _playerStub;

    [Export]
    public bool Active { get; set; } = false;

    public override void _Ready()
    {
        _player = GetNode<Player>("Player");
        _playerStub = GetNode<PlayerStub>("PlayerStub");

        if (Active)
        {
            ActivatePlayer((int)Device.Keyboard);
        }
        else
        {
            ActivateStub();
        }
    }

    public void ActivatePlayer(int deviceId)
    {
        _playerStub.ProcessMode = ProcessModeEnum.Disabled;
        _playerStub.Visible = false;

        _player.ProcessMode = ProcessModeEnum.Inherit;
        _player.Visible = true;
        _player.DeviceId = deviceId;

        Active = true;

    }

    public void ActivateStub()
    {
        _player.ProcessMode = ProcessModeEnum.Disabled;
        _player.Visible = false;
        // TODO: do somthing with player deviceId

        _playerStub.ProcessMode = ProcessModeEnum.Inherit;
        _playerStub.Visible = true;

        Active = false;
    }
}
