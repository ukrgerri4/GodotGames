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
    private int marginX = 110;
    private int marginY = 280;
    private TetromiconFactory tetromiconFactory;
    private int frame = 0;
    private int speed = 60;
    private bool shouldRotate = false;
    private Cell[,] map;
    private Tetromicon currentTetromicon;
    private Coordinate yMoveCoorditane = Coordinate.Zero;
    private Coordinate xMoveCoordinate = Coordinate.Zero;

    private PackedScene block;
    private Node2D[] blocks;

    #endregion

    public override void _Ready()
    {
        if (OS.IsDebugBuild()) {
            var debugCellTemplate = GD.Load<PackedScene>("res://Scenes/DebugCell.tscn");
            for (int x = 0; x < mapSizeX; x++)
            {
                for (int y = 0; y < mapSizeY; y++)
                {
                    var debugCell = debugCellTemplate.Instance<DebugCell>();
                    debugCell.SetText($"{x}:{y}");
                    debugCell.Position = GetMappedCoordinates(x, y);
                    AddChild(debugCell);
                }
            }
        }
        GD.Randomize();
        tetromiconFactory = GetNode<TetromiconFactory>("/root/TetromiconFactory");
        block = GD.Load<PackedScene>("res://Scenes/BaseBlock.tscn");

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
            xMoveCoordinate.x = 1;

        if (Input.IsActionPressed("ui_left"))
            xMoveCoordinate.x = -1;

        /* for debug */
        if (Input.IsActionPressed("ui_up"))
            yMoveCoorditane.y = -1;
        /* for debug */

        if (Input.IsActionPressed("ui_down"))
            yMoveCoorditane.y = 1;

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
        // TODO: fix rotation problem when space and one of up/down/left/right buttons pressed simutaneously
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
        blocks = new Node2D[] {
            block.Instance<Node2D>(),
            block.Instance<Node2D>(),
            block.Instance<Node2D>(),
            block.Instance<Node2D>()
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
        currentTetromicon = tetromiconFactory.Build();
        currentTetromicon.MoveToCoordinate(new Coordinate(6, 0));
        currentTetromicon.MoveToPositiveCoordinates();
        // currentTetromicon.RandomRotate(?);
        // currentTetromicon.Rotate(?);
    }

    private void HandleRotation()
    {
        if (shouldRotate)
        {
            var coordinates = currentTetromicon.GetNextRotationCoordinates();
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
        if (xMoveCoordinate != Coordinate.Zero)
        {
            var coordinates = currentTetromicon.GetCoordinatesIfMovedTo(xMoveCoordinate);
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
        // if (frame % speed == 0)
        // {
        //     yMove.y = 1;
        // }

        if (yMoveCoorditane != Coordinate.Zero)
        {
            var coordinates = currentTetromicon.GetCoordinatesIfMovedTo(yMoveCoorditane);
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
                    map[x, y].Block = block.Instance<Node2D>();
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
        xMoveCoordinate.x = 0;
        yMoveCoorditane.y = 0;
    }

    private Vector2 GetMappedCoordinates(int x, int y)
    {
        return new Vector2(
            x * mapUnit + mapOffset + marginX,
            y * mapUnit + mapOffset + marginY
        );
    }
}
