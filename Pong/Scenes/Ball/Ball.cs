using Godot;
using System;

public partial class Ball : CharacterBody2D
{
	public const float Speed = 300.0f;
	public Vector2 velocity = Vector2.Zero;
	private RayCast2D _rayCast2D;
	private Line2D _line2D;

	public override void _Ready()
	{
		_rayCast2D = GetNode<RayCast2D>("RayCast2D");
		_line2D = GetNode<Line2D>("Line2D");
		Reset();
	}

	public override void _PhysicsProcess(double delta)
	{
		var motion = velocity * Speed * (float)delta;
		if (Input.IsActionPressed("accelerate"))
		{
			motion = motion * 10;
		}
		var collision = MoveAndCollide(motion);

		if (collision is not null)
		{
			var collider = collision.GetCollider();

			if (collider is Player player)
			{
				player.TouchedByBall();
				velocity = velocity.Bounce(collision.GetNormal());
				velocity.X = ((collision.GetPosition().X - player.GlobalPosition.X + player.PanelWidth / 2) / player.PanelWidth - 0.5f) * 2;
				velocity = velocity.Normalized();
			}
			else if (collider is Corner corner)
			{
				corner.TouchedByBall();
				velocity = velocity.Bounce(collision.GetNormal()).Normalized();
			}
			else if (collider is PlayerStub stub)
			{
				stub.TouchedByBall();
				velocity = velocity.Bounce(collision.GetNormal()).Normalized();
			}

			return;
		}

		if (OS.IsDebugBuild())
		{
			_rayCast2D.LookAt(ToGlobal(velocity));
			var debugCollider = _rayCast2D.GetCollider();
			if (debugCollider is not null)
			{
				var end = velocity.Bounce(_rayCast2D.GetCollisionNormal()) * 500;
				var point = _rayCast2D.GetCollisionPoint();

				_line2D.ClearPoints();
				_line2D.Points = new Vector2[] { point, new Vector2(end.X + point.X, end.Y + point.Y) };
			}
		}
	}

	public void Reset()
	{
		Position = Vector2.Zero;
		RandomizeDirection();
	}

	private void RandomizeDirection()
	{
		var random = GD.Randf();
		float angle = random * Mathf.Pi * 2;
		velocity = new Vector2(Mathf.Cos(angle) * 75, Mathf.Sin(angle) * 75).Normalized();
	}
}
