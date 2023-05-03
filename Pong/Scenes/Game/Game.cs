using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Game : Node
{
    private Configuration _configuration;
    private RotationArea _rotationArea;
    private Node2D _map;
    private PropertyTweener _tweener;
    private bool _canTween = true;

    private List<PlayerSection> _payerSections = new List<PlayerSection>();

    public override void _Ready()
    {
        _configuration = GetNode<Configuration>("/root/Configuration");
        _map = GetNode<Node2D>("Map");
        _rotationArea = GetNode<RotationArea>("Map/RotationArea");

        _payerSections.Add(GetNode<PlayerSection>("Map/PlayerSection1"));
        _payerSections.Add(GetNode<PlayerSection>("Map/PlayerSection2"));
        _payerSections.Add(GetNode<PlayerSection>("Map/PlayerSection3"));
        _payerSections.Add(GetNode<PlayerSection>("Map/PlayerSection4"));

        if (_configuration.ConnectedDevices.Count() == 4)
        {
            for (int i = 0; i < _configuration.ConnectedDevices.Count(); i++)
            {
                var playerSection = _payerSections[i];
                var deviceId = _configuration.ConnectedDevices[i].DeviceId;
                playerSection.ActivatePlayer(deviceId);
            }
        }
        else
        {
            var firstPlayerSection = _payerSections[0];
            firstPlayerSection.ActivatePlayer((int)Device.Keyboard);

            for (int i = 0; i < _configuration.ConnectedDevices.Count(); i++)
            {
                var playerSection = _payerSections[i + 1]; // start with 2 player
                var deviceId = _configuration.ConnectedDevices[i].DeviceId;
                playerSection.ActivatePlayer(deviceId);
            }
        }

        GetNode<Node>("Balls").GetChild<Ball>(0).Reset();
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed(InputAction.MapRotate) && _rotationArea.RotationAllowed)
        {
            if (_tweener is null || _canTween)
            {
                _canTween = false;
                GetTree().Paused = true;
                _tweener = CreateTween().TweenProperty(_map, "rotation_degrees", _map.RotationDegrees + 90, 1f);
                _tweener.Finished += () =>
                {
                    _canTween = true;
                    GetTree().Paused = false;
                };
            }
        }
    }
}
