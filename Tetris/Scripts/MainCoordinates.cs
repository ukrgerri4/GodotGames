using System;
using System.Linq;
using Godot;
public class MainCoordinates : Node2D
{
    #region Properties
    private TetromiconFactory tetromiconFactory;
    private int mapUnit = 50;
    private int frame = 0;
    private int speed = 60;
    private bool shouldRotate = false;
    private CellType[,] map;
    private Tetromicon currentTetromicon;
    private Vector2 yMove = Vector2.Zero;
    private Vector2 xMove = Vector2.Zero;

    private BaseBlock[] blocks; 
    private bool debugStop = false;

    #endregion

    public override void _Ready()
    {
        GD.Randomize();
        tetromiconFactory = GetNode<TetromiconFactory>("/root/TetromiconFactory");
        var block = GD.Load<PackedScene>("res://Scenes/Blocks/BaseBlock.tscn");
        blocks = new BaseBlock[] {
            block.Instance<BaseBlock>(),
            block.Instance<BaseBlock>(),
            block.Instance<BaseBlock>(),
            block.Instance<BaseBlock>()
        };

        foreach (var b in blocks)
        {
            AddChild(b);
        }

        currentTetromicon = tetromiconFactory.BuildInPosition(new Coordinate(1,5));
        map = new CellType [10,20];
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionPressed("ui_right"))
            xMove.x = 1;

        if (Input.IsActionPressed("ui_left"))
            xMove.x = -1;

        if (Input.IsActionPressed("ui_down"))
            yMove.y = 1;

        if (Input.IsActionPressed("ui_accept"))
            shouldRotate = true;
        
        /* For SPACE only handler */
        // if (@event is InputEventKey eventKey && eventKey.Pressed && eventKey.Scancode == (int)KeyList.Space)
        //     shouldRotate = true;

        if (@event is InputEventKey eventKey && eventKey.Pressed && eventKey.Scancode == (int)KeyList.Control)
            debugStop = true;
    }
    public override void _Process(float delta)
    {
        frame++;
        HandleRotation();
        HandleFalling();
        HandleChangeActiveTetromiconPosition();
        SetNextStepDefaults();
    }

    private void HandleRotation()
    {
        if (shouldRotate) {
            var rotadedCoordinates = currentTetromicon.TryRotate();
            var nextCoordinates = currentTetromicon.TryMove(xMove);

            shouldRotate = false;
        }
    }

    private void HandleFalling()
    {
        if (frame % speed == 0) {
            yMove.y += 1;
        }
    }

    private void HandleChangeActiveTetromiconPosition()
    {
        if (yMove != Vector2.Zero) {
            var nextCoordinates = currentTetromicon.TryMove(yMove);
            for (int i = 0; i < nextCoordinates.Length; i++)
            {
                var x = (int)nextCoordinates[i].x;
                var y = (int)nextCoordinates[i].y;
                if (map[x,y] != 0) {

                }

                blocks[i].Position = new Vector2(x * 50 + 25, y * 50 + 25);
            }
        }
    }

    private void SetNextStepDefaults() {
        yMove.x = 0;
        yMove.y = 0;
    }

    private void ClearMap() {
        for (int i = 0; i < map.GetLength(0); i++)
            for (int j = 0; j < map.GetLength(1); j++)
                if (map[i,j] == CellType.Moving)
                    map[i,j] = 0;
    }
}
