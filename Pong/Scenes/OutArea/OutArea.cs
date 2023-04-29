using Godot;
using System;

public partial class OutArea : Area2D
{
	private void _on_body_entered(Node2D body)
	{
		if (body is Ball ball)
		{
			ball.Reset();
		}
		else if (body is Rocket rocket)
		{
			rocket.QueueFree();
		}
	}

	private void _on_area_entered(Area2D area)
	{
		if (area is Modifier modifier)
		{
			modifier.QueueFree();
		}
	}
}
