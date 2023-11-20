using Godot;
using System;

public partial class RotationArea : Area2D
{
    public bool RotationAllowed { get; set; }

    private void _on_body_entered(Node2D body)
    {
        RotationAllowed = true;
    }


    private void _on_body_exited(Node2D body)
    {
        RotationAllowed = false;
    }
}



