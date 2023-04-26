using Godot;
using System;

public partial class Rocket : CharacterBody2D
{
    // WHO launched?
    // Controller?

    public Vector2 velocity = Vector2.Up;
    public float Speed = 300f;
    private float _rotationMultiplier = 3f;
    private float _minRotationAngle = Mathf.Pi / 6;
    private PlayerInputManager _inputManager;

    public void Init(PlayerInputManager inputManager)
    {
        _inputManager = inputManager;
    }

    public override void _PhysicsProcess(double delta)
    {
        velocity = GetVelocity(delta);

        var collision = MoveAndCollide(velocity.Normalized() * Speed * (float)delta);

        if (collision is not null)
        {
            var collider = collision.GetCollider();

            QueueFree();
            return;
        }


        LookAt(GlobalPosition + velocity.Normalized());
    }

    private Vector2 GetVelocity(double delta)
    {
        var moveDirection = new Vector2(
            _inputManager.GetRightXStrength(),
            _inputManager.GetRightYStrength()
        );

        var rotationAngle = velocity.AngleTo(moveDirection);
        var angle = rotationAngle >= _minRotationAngle ? rotationAngle : _minRotationAngle;
        return velocity.Rotated(angle * (float)delta * _rotationMultiplier);
    }
}
