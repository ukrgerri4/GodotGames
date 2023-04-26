using Godot;

public partial class Main : Node
{
    private Configuration _configuration;
    private RotationArea _rotationArea;
    private Node2D _map;
    private PropertyTweener _tweener;
    private bool _canTween = true;

    public override void _Ready()
    {
        _configuration = GetNode<Configuration>("/root/Configuration");
        _rotationArea = GetNode<RotationArea>("RotationArea");
        _map = GetNode<Node2D>("Map");
    }

    public override void _Input(InputEvent @event)
    {
        /*
		if (_playerInputManager.IsActionJustPressed(InputAction.Pause))
		{
			
		}
		
		*/

        if (Input.IsActionJustPressed(InputAction.GamePause))
        {
            GetTree().Paused = !GetTree().Paused;
        }
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
        if (Input.IsActionJustPressed(InputAction.MapRotate) && _rotationArea.RotationAllowed)
        {
            if (_tweener is null || _canTween)
            {
                _canTween = false;
                GetTree().Paused = true;
                _tweener = CreateTween().TweenProperty(_map, "rotation_degrees", _map.RotationDegrees + 90, 1f);
                _tweener.Finished += () =>
                {
                    _canTween = true;
                    GetTree().Paused = false;
                };
            }
        }
    }
}
