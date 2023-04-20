using Godot;
using System;

public partial class Ball : CharacterBody2D
{
    public const float Speed = 300.0f;
    public Vector2 velocity = new Vector2(0, -1);

    public override void _Ready()
    {
        Reset();
    }

    public override void _PhysicsProcess(double delta)
    {
        var collision = MoveAndCollide(velocity * Speed * (float)delta);

        if (collision is not null)
        {
            var collider = collision.GetCollider();

            if (collider is Player player)
            {
                player.TouchedByBall();
                velocity = velocity.Bounce(collision.GetNormal());
            }
            else if (collider is Corner corner)
            {
                corner.TouchedByBall();
                velocity = velocity.Bounce(collision.GetNormal());
            }
            else if (collider is PlayerStub stub)
            {
                stub.TouchedByBall();
                velocity = velocity.Bounce(collision.GetNormal());
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
