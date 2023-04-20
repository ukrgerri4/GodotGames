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
            _playerStub.ProcessMode = ProcessModeEnum.Disabled;
            _playerStub.Visible = false;
        }
        else
        {
            _player.ProcessMode = ProcessModeEnum.Disabled;
            _player.Visible = false;
        }
    }
}
