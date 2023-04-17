using Godot;
using System;

public partial class Ball : CharacterBody2D
{
    private PhysicsDirectSpaceState2D _directSpaceState;

    public const float Speed = 500.0f;
    public Vector2 velocity = new Vector2(0, -1);

    public override void _Ready()
    {
        _directSpaceState = GetWorld2D().DirectSpaceState;
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
