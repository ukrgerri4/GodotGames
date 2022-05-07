using Godot;
public class NextTetromiconPanel : Panel
{
    private Tetris parent;
    private PackedScene block;
    private Node2D[] blocks;

    private ColorRect board;
    public override void _Ready()
    {
        parent = GetNode<Tetris>("/root/Main/Tetris");
        parent.NextTetromiconCreatedEvent += UpdateNextTetromiconView;

        block = GD.Load<PackedScene>("res://Scenes/BaseBlock.tscn");
        board = GetNode<ColorRect>("Board");
        blocks = new Node2D[4];
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i] = block.Instance<Node2D>();
            blocks[i].Visible = false;
            blocks[i].Scale = new Vector2((float)0.5,(float)0.5);
            board.AddChild(blocks[i]);
        }
    }

    public void UpdateNextTetromiconView() {
        for (int i = 0; i < blocks.Length; i++)
        {
            var x = parent.nextTetromicon.Coordinates[i].x;
            var y = parent.nextTetromicon.Coordinates[i].y;
            blocks[i].Position = new Vector2(
                x * 25 + (float)board.RectSize.x/2,
                y * 25 + (float)board.RectSize.y/2
            );
            blocks[i].Visible = true;
        }
    }
}
