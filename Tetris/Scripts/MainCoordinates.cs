using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
public class MainCoordinates : Node2D
{
    #region Properties
    private int mapSizeX = 10;
    private int mapSizeY = 20;
    private int mapOffset = 25;
    private int mapUnit = 50;
    private TetromiconFactory tetromiconFactory;
    private int frame = 0;
    private int speed = 60;
    private bool shouldRotate = false;
    private CellType[,] map;
    private Tetromicon currentTetromicon;
    private Coordinate yMove = Coordinate.Zero;
    private Coordinate xMove = Coordinate.Zero;

    private PackedScene block;
    private BaseBlock[] blocks;
    private bool debugStop = false;

    #endregion

    public override void _Ready()
    {
        GD.Randomize();
        tetromiconFactory = GetNode<TetromiconFactory>("/root/TetromiconFactory");
        block = GD.Load<PackedScene>("res://Scenes/Blocks/BaseBlock.tscn");
        map = new CellType[mapSizeX, mapSizeY];

        InitBlocks();
        ChangeBlocksVisibility(false);
        AddBlocksToNode();
        InitNewTetromicon();
        UpdateBlocksPositions();
        ChangeBlocksVisibility(true);
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionPressed("ui_right"))
            xMove.x = 1;

        if (Input.IsActionPressed("ui_left"))
            xMove.x = -1;

        /* for debug */
        if (Input.IsActionPressed("ui_up"))
            yMove.y = -1;
        /* for debug */

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
        HandleInputXAxisShift();
        HandleInputYAxisShift();
        SetNextStepDefaults();
    }

    private void InitBlocks()
    {
        blocks = new BaseBlock[] {
            block.Instance<BaseBlock>(),
            block.Instance<BaseBlock>(),
            block.Instance<BaseBlock>(),
            block.Instance<BaseBlock>()
        };
    }

    private void ChangeBlocksVisibility(bool isVisible)
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].Visible = isVisible;
        }
    }

    private void AddBlocksToNode()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            AddChild(blocks[i]);
        }
    }

    private void UpdateBlocksPositions()
    {
        for (int i = 0; i < currentTetromicon.Coordinates.Length; i++)
        {
            blocks[i].Position = new Vector2(
                currentTetromicon.Coordinates[i].x * mapUnit + mapOffset,
                currentTetromicon.Coordinates[i].y * mapUnit + mapOffset
            );
        }
    }

    private void InitNewTetromicon()
    {
        currentTetromicon = tetromiconFactory.BuildInPosition(new Coordinate(3, 3));
    }

    private void HandleRotation()
    {
        if (shouldRotate)
        {
            var coordinates = currentTetromicon.TryRotate();
            if (ShiftAllowed(coordinates) && ShiftAllowed(currentTetromicon.RotationCoordinates))
            {
                currentTetromicon.Rotate();
            }
            UpdateBlocksPositions();
            shouldRotate = false;
        }
    }

    private void HandleInputXAxisShift()
    {
        if (xMove != Coordinate.Zero)
        {
            var coordinates = currentTetromicon.TryMove(xMove);
            if (ShiftAllowed(coordinates))
            {
                currentTetromicon.Coordinates = coordinates;
                UpdateBlocksPositions();
            }
        }
    }

    private bool ShiftAllowed(Coordinate[] c)
    {
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i].x >= mapSizeX || c[i].x < 0 || c[i].y >= mapSizeY || c[i].y < 0 || map[c[i].x, c[i].y] != CellType.Empty)
            {
                return false;
            }
        }
        return true;
    }

    private void HandleInputYAxisShift()
    {
        // if (frame % speed == 0)
        // {
        //     yMove.y = 1;
        // }

        if (yMove != Coordinate.Zero)
        {
            var coordinates = currentTetromicon.TryMove(yMove);
            if (ShiftAllowed(coordinates))
            {
                currentTetromicon.Coordinates = coordinates;
            }
            else
            {
                for (int i = 0; i < currentTetromicon.Coordinates.Length; i++)
                {
                    var x = currentTetromicon.Coordinates[i].x;
                    var y = currentTetromicon.Coordinates[i].y;
                    var newBlock = block.Instance<BaseBlock>();
                    newBlock.Position = new Vector2(
                        x * mapUnit + mapOffset,
                        y * mapUnit + mapOffset
                    );
                    AddChild(newBlock);
                    map[x, y] = CellType.Filled;
                }
                CheckLines();
                InitNewTetromicon();

            }
            UpdateBlocksPositions();
        }
    }

    private void CheckLines()
    {
        var lineIndexesToDelete = new List<int>();
        for (int i = map.GetLength(0) - 1; i <= 0; i--)
        {
            int lineCount = 0;
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == CellType.Filled)
                {
                    lineCount++;
                }
            }

            if (lineCount == 10) {
                lineIndexesToDelete.Add(i);
            }
        }

        for (int i = map.GetLength(0) - 1; i <= 0; i--)
        {
            if (lineIndexesToDelete.Contains(i)){
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = CellType.Empty;
                }
            }
        }

        
        // for (int i = lineIndexesToDelete.Max(); i <= 0; i--)
        // {
        //     for (int j = 0; j < map.GetLength(1); j++)
        //     {
        //         if (map[i, j] == CellType.Filled) {

        //         }
        //     }
        // }
    }

    private void SetNextStepDefaults()
    {
        xMove.x = 0;
        yMove.y = 0;
    }

    // private void ClearMap()
    // {
    //     for (int i = 0; i < map.GetLength(0); i++)
    //         for (int j = 0; j < map.GetLength(1); j++)
    //             if (map[i, j] == CellType.Moving)
    //                 map[i, j] = 0;
    // }
}
