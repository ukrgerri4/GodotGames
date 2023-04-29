using Godot;
using System;

public partial class Modifier : Area2D
{
	private Vector2 velocity = Vector2.Zero;
	private float _speed = 300.0f;
	public override void _PhysicsProcess(double delta)
	{
		Position = Position + Vector2.Left * _speed * (float)delta;
	}

	private void _on_body_entered(Node2D body)
	{
		GD.Print("B");
		if (body is Player player)
		{
			QueueFree();
		}
	}
}
