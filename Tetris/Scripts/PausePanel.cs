using Godot;

public class PausePanel : Panel
{
    private bool gamePaused;

    public override void _Ready()
    {
        Visible = false;
    }

    public override void _Input(InputEvent @event)
    {
        if (gamePaused && Input.IsActionJustPressed("ui_accept"))
        {
            UnPause();
            return;
        }

        if (@event is InputEventKey eventKey && eventKey.Pressed && eventKey.Scancode == (int)KeyList.P)
        {
            Pause();
        }
    }

    private void Pause()
    {
        gamePaused = true;
        Visible = true;
        GetTree().Paused = true;
    }

    private void UnPause()
    {
        gamePaused = false;
        Visible = false;
        GetTree().Paused = false;
    }
}
