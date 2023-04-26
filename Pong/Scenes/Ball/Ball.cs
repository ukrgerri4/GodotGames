using Godot;
using System;

public partial class Ball : CharacterBody2D
{
    public const float Speed = 500.0f;
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
        if (Input.IsKeyPressed(Key.Ctrl) || Input.GetJoyAxis(0, JoyAxis.TriggerRight) > 0.5)
        {
            motion = motion * 10;
        }
        var collision = MoveAndCollide(motion);

        if (collision is not null)
        {
            var collider = collision.GetCollider();

            if (collider is SimpleBlock block)
            {
                block.TouchedByBall();
                velocity = velocity.Bounce(collision.GetNormal()).Normalized();
            }
            if (collider is Player player)
            {
                player.TouchedByBall();
                velocity = velocity.Bounce(collision.GetNormal());
                if (player.IsHorizontalPosition)
                {
                    velocity.X = ((collision.GetPosition().X - player.GlobalPosition.X + player.PanelWidth / 2) / player.PanelWidth - 0.5f) * 2;
                }
                else
                {
                    velocity.Y = ((collision.GetPosition().Y - player.GlobalPosition.Y + player.PanelWidth / 2) / player.PanelWidth - 0.5f) * 2;
                }

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

        // if (OS.IsDebugBuild())
        // {
        //     _rayCast2D.LookAt(ToGlobal(velocity));
        //     var debugCollider = _rayCast2D.GetCollider();
        //     if (debugCollider is not null)
        //     {
        //         Vector2 end = Vector2.Zero;
        //         if (debugCollider is Player player)
        //         {
        //             end = velocity.Bounce(_rayCast2D.GetCollisionNormal());
        //             end.X = ((_rayCast2D.GetCollisionPoint().X - player.GlobalPosition.X + player.PanelWidth / 2) / player.PanelWidth - 0.5f) * 2;
        //             end = end.Normalized();
        //         }
        //         else
        //         {
        //             end = velocity.Bounce(_rayCast2D.GetCollisionNormal()).Normalized();
        //         }

        //         end = end * 500;
        //         var point = _rayCast2D.GetCollisionPoint();

        //         _line2D.ClearPoints();
        //         _line2D.Points = new Vector2[] { point, new Vector2(end.X + point.X, end.Y + point.Y) };
        //     }
        // }
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
        // velocity = Vector2.Down;
    }
}
