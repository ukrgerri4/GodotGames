using Godot;

public class EndGamePanel : Panel
{
    private Main parent;
    public override void _Ready()
    {
        this.Visible = false;
        parent = GetNode<Main>("/root/Main");
        parent.GameStartedEvent += () => this.Visible = false;
        parent.GameEndedEvent += () => this.Visible = true;
    }
}
