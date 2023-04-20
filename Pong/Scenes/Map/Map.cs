using Godot;
using System;

public partial class Map : Node2D
{
    private MapEventsBus _mapEventsBus;
    private RotationArea _rotationArea;
    private PropertyTweener _tweener;
    private bool _canTween = true;

    public override void _Ready()
    {
        _mapEventsBus = GetNode<MapEventsBus>("/root/MapEventsBus");
        _rotationArea = GetNode<RotationArea>("/root/Main/RotationArea");

    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("ui_accept") && _rotationArea.RotationAllowed)
        {
            if (_tweener is null || _canTween)
            {
                _canTween = false;
                GetTree().Paused = true;
                _tweener = CreateTween().TweenProperty(this, "rotation", Rotation + Mathf.DegToRad(90), 2f);
                _tweener.Finished += () =>
                {
                    _canTween = true;
                    GetTree().Paused = false;
                };
            }
        }
    }
}
