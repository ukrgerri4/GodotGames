using Godot;
using System;

public partial class SimpleBlock : StaticBody2D
{
    [Export]
    public int HitsToDestroy { get; set; } = 3;

    public void TouchedByBall()
    {
        // event to get points to player
        HitsToDestroy--;
        if (HitsToDestroy == 0)
        {
            QueueFree();
        }
    }
}
