using Godot;
using System;

public partial class Ball : CharacterBody2D
{
	public const float Speed = 500.0f;
	public Vector2 velocity = new Vector2(0, -1);

	public override void _Ready()
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		var collide = MoveAndCollide(velocity * Speed * (float)delta);
		if (collide is not null)
		{
			velocity = velocity.Bounce(collide.GetNormal());
		}
	}
}
