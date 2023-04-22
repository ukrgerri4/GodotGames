using Godot;
using System;

public partial class Rocket : CharacterBody2D
{
	public Vector2 velocity = Vector2.Up;
	public float Speed = 300f;

	private float _rotationMultiplier = 3f;

	public override void _PhysicsProcess(double delta)
	{
		velocity = GetVelocityByJoypad(delta);
		// velocity = GetVelocityByKeyboard(delta);

		var collision = MoveAndCollide(velocity.Normalized() * Speed * (float)delta);

		if (collision is not null)
		{
			var collider = collision.GetCollider();

			QueueFree();
			return;
		}


		LookAt(GlobalPosition + velocity.Normalized());
	}

	private Vector2 GetVelocityByJoypad(double delta)
	{
		var moveDirection = new Vector2(
			Input.GetJoyAxis(0, JoyAxis.RightX),
			Input.GetJoyAxis(0, JoyAxis.RightY)
		);

		if (moveDirection.LengthSquared() > 0.25f)
		{
			var angle = velocity.AngleTo(moveDirection);
			return velocity.Rotated(angle * (float)delta * _rotationMultiplier);
		}

		return velocity;
	}

	private Vector2 GetVelocityByKeyboard(double delta)
	{
		var rotationStrength = Input.GetActionStrength("rocket_right") - Input.GetActionStrength("rocket_left");
		return velocity.Rotated(Mathf.Pi / 6 * rotationStrength * (float)delta * 3);
	}
}
