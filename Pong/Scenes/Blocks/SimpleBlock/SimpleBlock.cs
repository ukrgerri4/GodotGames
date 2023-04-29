using Godot;
using System;

public partial class SimpleBlock : StaticBody2D
{
	private Map _map;
	private PackedScene _modifier;

	[Export]
	public int HitsToDestroy { get; set; } = 3;

	public override void _Ready()
	{
		_map = GetNode<Map>("/root/Main/Game/Map");
		_modifier = GD.Load<PackedScene>("res://Scenes/Modifiers/Modifier.tscn");
	}

	public void TouchedByBall()
	{
		// event to get points to player
		HitsToDestroy--;
		if (HitsToDestroy == 0)
		{
			var modifier = _modifier.Instantiate<Modifier>();
			modifier.GlobalPosition = GlobalPosition;
			_map.AddChild(modifier);
			QueueFree();
		}
	}
}
