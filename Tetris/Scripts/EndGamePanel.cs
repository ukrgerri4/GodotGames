using Godot;

public class EndGamePanel : Panel
{
    private MainCoordinates parent;
    public override void _Ready()
    {
        this.Visible = false;
        parent = GetParent<MainCoordinates>();
        parent.GameStartedEvent += () => this.Visible = false;
        parent.GameEndedEvent += () => this.Visible = true;
    }
}
