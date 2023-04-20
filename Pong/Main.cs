using Godot;

public partial class Main : Node
{
    private Node2D _map;
    private RotationArea _rotationArea;
    private PropertyTweener _tweener;
    private bool _canTween = true;

    public override void _Ready()
    {
        _rotationArea = GetNode<RotationArea>("/root/Main/RotationArea");
        _map = GetNode<Node2D>("Map");
    }

    public override void _Input(InputEvent @event)
    {
        // if (@event is InputEventJoypadButton joypadButtonEvent)
        // {
        //     if (!_players.Any(x => x.JoyPadId == joypadButtonEvent.Device))
        //     {
        //         CallDeferred(nameof(AddPlayer), (int)ControllerType.JoyPad, joypadButtonEvent.Device);
        //     }
        // }
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("ui_accept") && _rotationArea.RotationAllowed)
        {
            if (_tweener is null || _canTween)
            {
                _canTween = false;
                GetTree().Paused = true;
                _tweener = CreateTween().TweenProperty(_map, "rotation", _map.Rotation + Mathf.DegToRad(90), 1f);
                _tweener.Finished += () =>
                {
                    _canTween = true;
                    GetTree().Paused = false;
                };
            }
        }
    }
}
