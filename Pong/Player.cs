using Godot;
using System;

public partial class Player : CharacterBody2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        var motion = new Vector2(
            Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"),
            0
        );

        motion = motion.Normalized();

        MoveAndCollide(motion * 300 * (float)delta);
    }
}
