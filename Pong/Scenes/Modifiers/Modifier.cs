using Godot;
using System;

public partial class Modifier : Area2D
{
	private Vector2 velocity = Vector2.Zero;
	private float _speed = 100.0f;
	public override void _PhysicsProcess(double delta)
	{
		Position = Position + velocity * _speed * (float)delta;
	}

	public void Init(Vector2 baseVelocity)
	{
		velocity = baseVelocity;
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
