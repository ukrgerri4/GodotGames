using Godot;

public class DefaultInputAction
{
    public StringName ActionName { get; set; }
    public InputEventKey KeyButton { get; set; }
    public InputEventJoypadButton JoyButton { get; set; }
    public InputEventJoypadMotion JoyMotion { get; set; }

}