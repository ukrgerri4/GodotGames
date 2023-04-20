using Godot;
using System;

public partial class ActionArea : Area2D
{
    public bool ActionAllowed { get; set; }
    private void _on_area_entered(Area2D area)
    {
        if (area is BallCenterArea)
        {
            ActionAllowed = true;
        }
    }

    private void _on_area_exited(Area2D area)
    {
        if (area is BallCenterArea)
        {
            ActionAllowed = false;
        }
    }
}
