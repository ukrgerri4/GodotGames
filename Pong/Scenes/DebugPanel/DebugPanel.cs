using Godot;
using System;

public partial class DebugPanel : VBoxContainer
{
    private EventsBus _eventsBus;
    private Label _ballSpeedLabel;
    private Label _ballRadiusLabel;
    private Label _modifierSpeedLabel;

    public override void _Ready()
    {
        // _gameManager = GetNode<GameManager>("/root/GameManager");
        _eventsBus = GetNode<EventsBus>("/root/EventsBus");

        _ballSpeedLabel = GetNode<Label>("BallSpeedContainer/Value");
        _ballRadiusLabel = GetNode<Label>("BallRadiusContainer/Value");
        _modifierSpeedLabel = GetNode<Label>("ModifierSpeedContainer/Value");

        _eventsBus.Ball.SpeedChanged += (float speed) => _ballSpeedLabel.Text = speed.ToString();
        _eventsBus.Ball.RadiusChanged += (float radius) => _ballRadiusLabel.Text = radius.ToString();
        _eventsBus.Modifier.SpeedChanged += (float speed) => _modifierSpeedLabel.Text = speed.ToString();
    }
}
