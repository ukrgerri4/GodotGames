using Godot;

public class Cell
{
    public CellType Type { get; set; } = CellType.Empty;
    public Node2D Block { get; set; }

    public void Disable()
    {
        Type = CellType.Empty;
        Block.Visible = false;
    }

    public void Enable()
    {
        Type = CellType.Filled;
        Block.Visible = true;
    }
}