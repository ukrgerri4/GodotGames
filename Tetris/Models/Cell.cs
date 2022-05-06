using Godot;

public class Cell
{
    public CellType Type { get; set; } = CellType.Empty;
    public Node2D Block { get; set; }

    public bool Filled => Type == CellType.Filled;
    public bool Empty => Type == CellType.Empty;

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