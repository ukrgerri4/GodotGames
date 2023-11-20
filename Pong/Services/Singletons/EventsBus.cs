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
    public MapEvents Map { get; set; } = new MapEvents();
    public BallEvents Ball { get; set; } = new BallEvents();
    public PlayerEvents Player { get; set; } = new PlayerEvents();
    public ModifierEvents Modifier { get; set; } = new ModifierEvents();

}

// public class BallEvents
// {
//     public delegate void SpeedChangedDelegate(float speed);
//     private event SpeedChangedDelegate SpeedChanged;

//     public delegate void RadiusChangedDelegate(float radius);
//     private event RadiusChangedDelegate RadiusChanged;
// }

// public class PlayerEvents
// {
//     public delegate void SpeedChangedDelegate(float speed);
//     private event SpeedChangedDelegate SpeedChanged;

//     public delegate void ScoreChangedDelegate(int score);
//     private event ScoreChangedDelegate ScoreChanged;

//     public delegate void WidthChangedDelegate(float width);
//     private event WidthChangedDelegate WidthChanged;
// }

public class MapEvents
{
    public event Action<MapRotateState> RotateStateChanged;
    public void NotifyRotateStateChanged(MapRotateState state) => RotateStateChanged?.Invoke(state);

}

public class BallEvents
{
    public event Action<float> SpeedChanged;
    public void NotifySpeedChanged(float speed) => SpeedChanged?.Invoke(speed);


    public event Action<float> RadiusChanged;
    public void NotifyRadiusChanged(float radius) => RadiusChanged?.Invoke(radius);
}

public class PlayerEvents
{
    public event Action<int, float> SpeedChanged;
    public void NotifySpeedChanged(int playerId, float speed) => SpeedChanged?.Invoke(playerId, speed);

    public event Action<int, int> ScoreChanged;
    public void NotifyScoreChanged(int playerId, int score) => ScoreChanged?.Invoke(playerId, score);

    public event Action<int, float> WidthChanged;
    public void NotifyWidthChanged(int playerId, float width) => WidthChanged?.Invoke(playerId, width);
}

public class ModifierEvents
{
    public event Action<float> SpeedChanged;
    public void NotifySpeedChanged(float speed) => SpeedChanged?.Invoke(speed);
}
