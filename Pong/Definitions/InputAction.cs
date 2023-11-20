using Godot;

public static class InputAction
{
    public static readonly StringName GamePause = new StringName("game_pause");
    public static readonly StringName GameFullScreen = new StringName("game_full_screen");

    public static readonly StringName MoveUp = new StringName("move_up");
    public static readonly StringName MoveDown = new StringName("move_down");
    public static readonly StringName MoveLeft = new StringName("move_left");
    public static readonly StringName MoveRight = new StringName("move_right");

    public static readonly StringName RocketUp = new StringName("rocket_up");
    public static readonly StringName RocketDown = new StringName("rocket_down");
    public static readonly StringName RocketLeft = new StringName("rocket_left");
    public static readonly StringName RocketRight = new StringName("rocket_right");
    public static readonly StringName RocketLaunch = new StringName("rocket_launch");

    public static readonly StringName PadAccelerate = new StringName("pad_accelerate");

    public static readonly StringName MapRotate = new StringName("map_rotate");
}