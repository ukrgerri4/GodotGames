using Godot;
using System;

public partial class Rocket : CharacterBody2D
{
	// WHO launched?

	public Vector2 _velocity = Vector2.Zero;
	public float _speed = 370f;
	private float _rotationMultiplier = 3f;
	private float _minRotationAngle = Mathf.Pi / 6;
	private IPlayerInputManager _inputManager;

	// public delegate void AttackedEventHandler();
	// public AttackedEventHandler Destroyed;


	public override void _Ready()
	{

	}

	public void Init(IPlayerInputManager inputManager, Vector2 baseVelocity)
	{
		_inputManager = inputManager;
		_velocity = baseVelocity;
	}

	public override void _PhysicsProcess(double delta)
	{
		_velocity = GetVelocity(delta);

		var collision = MoveAndCollide(_velocity.Normalized() * _speed * (float)delta);

		if (collision is not null)
		{
			var collider = collision.GetCollider();

			QueueFree();
			return;
		}

		LookAt(GlobalPosition + _velocity.Normalized());
	}

	private Vector2 GetVelocity(double delta)
	{
		var moveDirection = new Vector2(
			_inputManager.GetRightXStrength(),
			_inputManager.GetRightYStrength()
		);

		if (moveDirection.LengthSquared() < 0.15f)
		{
			return _velocity;
		}

		var rotationAngle = _velocity.AngleTo(moveDirection);

		// TODO: refactoring
		var angle = Mathf.Abs(rotationAngle) >= _minRotationAngle
			? rotationAngle
			: rotationAngle >= 0
				? _minRotationAngle
				: _minRotationAngle * -1;

		return _velocity.Rotated(angle * (float)delta * _rotationMultiplier);
	}
}
