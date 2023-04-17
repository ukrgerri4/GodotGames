using Godot;

public class PlayerDefaultOptions
{
	public string Nickname { get; set; }
	public Vector2 AppearPosition { get; set; }
	public int ApperRotationAngle { get; set; }
	public int JoyPadId { get; set; }
	public StaticBody2D? Stub { get; set; }
}