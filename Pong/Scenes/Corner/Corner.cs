using Godot;
using System;

public partial class Corner : StaticBody2D
{
    public void TouchedByBall()
    {
        GD.Print("Corner touched by Ball.");
    }
}
