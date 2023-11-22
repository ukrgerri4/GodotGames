using Godot;
using System;

public partial class Modifier : Area2D
{
	private GameManager _gameManager;

	private Vector2 velocity = Vector2.Zero;
	private float _speed = 100.0f;


	public override void _Ready()
	{
		_gameManager = GetNode<GameManager>("/root/GameManager");
	}

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
		if (body is IPlayer player)
		{
			_gameManager.AddBall();
			QueueFree();
		}
	}
}
