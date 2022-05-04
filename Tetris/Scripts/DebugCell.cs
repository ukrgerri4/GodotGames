using Godot;
using System;

public class DebugCell : Node2D
{
    public void SetText(string text)
    {
        var label = GetNode<Label>("Label");
        label.Text = text;
    }
}
