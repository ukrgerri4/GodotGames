using Godot;
using System;

public enum MapRotateState
{
    Started = 1,
    Rotating = 2,
    Ended = 3
}

public partial class MapEventsBus : Node
{
    public delegate void MapRotatingDelegate(MapRotateState state);
    private event MapRotatingDelegate MapRotating;

    public void NotifyMapRotatingStarted()
    {
        MapRotating?.Invoke(MapRotateState.Started);
    }

    public void NotifyMapRotatingEnded()
    {
        MapRotating?.Invoke(MapRotateState.Ended);
    }

    public void AddMapRotatingHandler(MapRotatingDelegate handler)
    {
        MapRotating += handler;
    }
}