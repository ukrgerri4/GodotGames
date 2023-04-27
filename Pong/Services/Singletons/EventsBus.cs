using Godot;
using System;

public enum MapRotateState
{
    Started = 1,
    Rotating = 2,
    Ended = 3
}

public partial class EventsBus : Node
{
    public delegate void MapRotatingDelegate(MapRotateState state);
    private event MapRotatingDelegate MapRotating;

    public delegate void PlayerScoreNotificationDelegate(PlayerScoreNotificationEvent notification);
    private event PlayerScoreNotificationDelegate PlayerScoreNotification;

    public void AddMapRotatingHandler(MapRotatingDelegate handler)
    {
        MapRotating += handler;
    }

    public void AddPlayerScoreNotificationHandler(PlayerScoreNotificationDelegate handler)
    {
        PlayerScoreNotification += handler;
    }

    public void NotifyMapRotatingStarted()
    {
        MapRotating?.Invoke(MapRotateState.Started);
    }

    public void NotifyMapRotatingEnded()
    {
        MapRotating?.Invoke(MapRotateState.Ended);
    }

    public void NotifyPlayerScoreChanged(int playerId, int newScore)
    {
        PlayerScoreNotification?.Invoke(new PlayerScoreNotificationEvent
        {
            PlayerId = playerId,
            Score = newScore
        });
    }
}