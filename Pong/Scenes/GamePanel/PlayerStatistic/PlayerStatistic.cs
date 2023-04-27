using Godot;
using System;

public partial class PlayerStatistic : HBoxContainer
{
    [Export]
    public int PlayerId { get; set; }

    [Export]
    public string PlayerName { get; set; }

    private EventsBus _eventsBus;
    private Label _nameLabel;
    private Label _scoreLabel;

    public override void _Ready()
    {
        _eventsBus = GetNode<EventsBus>("/root/EventsBus");
        _nameLabel = GetNode<Label>("Name");
        _nameLabel.Text = (PlayerName ?? $"Player{PlayerId}") + " Score:";
        _scoreLabel = GetNode<Label>("Score");
        _scoreLabel.Text = "0";
        _eventsBus.AddPlayerScoreNotificationHandler(HandelePlayerScoreNotification);
    }

    private void HandelePlayerScoreNotification(PlayerScoreNotificationEvent notification)
    {
        if (notification.PlayerId == PlayerId)
        {
            _scoreLabel.Text = notification.Score.ToString();
        }
    }
}
