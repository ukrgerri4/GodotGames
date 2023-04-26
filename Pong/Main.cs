using Godot;

public partial class Main : Node
{
    public override void _Ready()
    {
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(InputAction.GamePause))
        {
            GetTree().Paused = !GetTree().Paused;
        }
    }
}
