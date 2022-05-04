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
    private int marginX = 0;
    private int marginY = 0;
    private TetromiconFactory tetromiconFactory;
    private int frame = 0;
    private int speed = 60;
    private bool shouldRotate = false;
    private Cell[,] map;
    private Tetromicon currentTetromicon;
    private Coordinate yMove = Coordinate.Zero;
    private Coordinate xMove = Coordinate.Zero;

    private PackedScene block;
    private BaseBlock[] blocks;

    #endregion

    public override void _Ready()
    {
        GD.Randomize();
        tetromiconFactory = GetNode<TetromiconFactory>("/root/TetromiconFactory");
        block = GD.Load<PackedScene>("res://Scenes/Blocks/BaseBlock.tscn");

        InitMap();
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

        // if (@event is InputEventKey eventKey && eventKey.Pressed && eventKey.Scancode == (int)KeyList.Control)
        //     debugStop = true;
    }
    public override void _Process(float delta)
    {
        frame++;
        HandleRotation();
        HandleInputXAxisShift();
        HandleInputYAxisShift();
        SetNextStepDefaults();
    }

    private void InitMap()
    {
        map = new Cell[mapSizeX, mapSizeY];
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                map[x, y] = new Cell();
            }
        }
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
            blocks[i].Position = GetMappedCoordinates(currentTetromicon.Coordinates[i].x, currentTetromicon.Coordinates[i].y);
        }
    }

    private void InitNewTetromicon()
    {
        currentTetromicon = tetromiconFactory.BuildInPosition(new Coordinate(6, 0));
        currentTetromicon.ConvertToPositiveCoordinates();
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
            if (c[i].x >= mapSizeX || c[i].x < 0 || c[i].y >= mapSizeY || c[i].y < 0 || map[c[i].x, c[i].y].Type != CellType.Empty)
            {
                return false;
            }
        }
        return true;
    }

    private void HandleInputYAxisShift()
    {
        if (frame % speed == 0)
        {
            yMove.y = 1;
        }

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
                    map[x, y].Block = block.Instance<BaseBlock>();
                    map[x, y].Block.Position = GetMappedCoordinates(x, y);
                    AddChild(map[x, y].Block);
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

        var lx = map.GetLength(0); // 10
        var ly = map.GetLength(1); // 20

        for (int lineIndex = map.GetLength(1) - 1; lineIndex >= 0; lineIndex--)
        {
            int lineCount = 0;
            for (int columnIndex = 0; columnIndex < map.GetLength(0); columnIndex++)
            {
                if (map[columnIndex, lineIndex].Type == CellType.Filled)
                {
                    lineCount++;
                }
            }

            if (lineCount == 10)
            {
                lineIndexesToDelete.Add(lineIndex);
            }
        }

        if (lineIndexesToDelete.Count > 0)
        {
            for (int i = 0; i < lineIndexesToDelete.Count; i++)
            {
                var lineIndex = lineIndexesToDelete[i];
                for (int columnIndex = 0; columnIndex < map.GetLength(0); columnIndex++)
                {
                    map[columnIndex, lineIndex].FreeCell();
                }
            }

            for (int lineIndex = map.GetLength(1) - 1; lineIndex >= 0; lineIndex--)
            {
                var yOffset = lineIndexesToDelete.Count(x => x > lineIndex);
                if (yOffset > 0)
                {
                    for (int columnIndex = 0; columnIndex < map.GetLength(0); columnIndex++)
                    {
                        if (map[columnIndex, lineIndex].Type == CellType.Filled)
                        {
                            var newLineIndex = lineIndex + yOffset;
                            map[columnIndex, newLineIndex] = map[columnIndex, lineIndex];
                            map[columnIndex, newLineIndex].Block.Position = GetMappedCoordinates(columnIndex, newLineIndex);
                            map[columnIndex, lineIndex] = new Cell();
                        }
                    }
                }
            }
        }
    }

    private void SetNextStepDefaults()
    {
        xMove.x = 0;
        yMove.y = 0;
    }

    private Vector2 GetMappedCoordinates(int x, int y)
    {
        return new Vector2(
            x * mapUnit + mapOffset + marginX,
            y * mapUnit + mapOffset + marginY
        );
    }
}
