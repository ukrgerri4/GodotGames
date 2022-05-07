using Godot;

public class EndGamePanel : Panel
{
    private Tetris parent;
    public override void _Ready()
    {
        this.Visible = false;
        parent = GetNode<Tetris>("/root/Main/Tetris");
        parent.GameStartedEvent += () => this.Visible = false;
        parent.GameEndedEvent += () => this.Visible = true;
    }
}
