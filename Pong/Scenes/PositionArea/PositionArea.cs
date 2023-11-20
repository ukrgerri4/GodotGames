using Godot;
using System;

public enum MapPosition
{
	Undefined = 0,
	Top = 1,
	Down = 2,
	Left = 3,
	Right = 4
}

public partial class PositionArea : Area2D
{
	[Export]
	public MapPosition MapPosition { get; set; }

	private void _on_body_entered(Node2D body)
	{
		GD.Print($"Map position player {MapPosition}");
		if (body is Player player)
		{
			player.MapPosition = MapPosition;
		}
	}
}
