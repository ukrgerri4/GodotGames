using System.Collections.Generic;
using System.Linq;
using Godot;

/*
    3. First page
    4. Save stats
    6. Optimization
    8. Dissapear animation
    10. Main sound theme
*/
public class Tetris : Node2D
{
    #region Properties
    private const int initFrameTickRate = 60; // 55 max!

    private const int mapSizeX = 10;
    private const int mapSizeY = 20;
    private const int mapOffset = 25;
    private const int mapUnit = 50;
    private const int marginX = 110;
    private const int marginY = 280;
    private TetromiconFactory tetromiconFactory;
    private int frame = 0;
    public int frameTickRate = initFrameTickRate;
    public int collectedLines = 0;
    public int speedIncreaseDelta = 1;
    public int speedIncreasePoint = 0;
    private AudioStreamPlayer rotationSound;

    private bool shouldRotate = false;
    private bool gameEnded = false;
    private BaseBlock[,] map;
    private Tetromicon currentTetromicon;
    public Tetromicon nextTetromicon;

    private Coordinate yMoveCoorditane = Coordinate.Zero;
    private Coordinate xMoveCoordinate = Coordinate.Zero;

    private PackedScene block;
    private BaseBlock[] blocks;

    public delegate void LinesDestroyed(int linesDestroyed);
    public event LinesDestroyed LinesDestroyedEvent;

    public delegate void NextTetromiconCreated();
    public event NextTetromiconCreated NextTetromiconCreatedEvent;

    public delegate void GameStarted();
    public event GameStarted GameStartedEvent;
    public delegate void GameEnded();
    public event GameEnded GameEndedEvent;

    private Node2D mapContainer;

    #endregion
    public override void _Ready()
    {
        GD.Randomize();
        tetromiconFactory = GetNode<TetromiconFactory>("/root/TetromiconFactory");
        block = GD.Load<PackedScene>("res://Scenes/BaseBlock.tscn");
        mapContainer = GetNode<Node2D>("MapContainer");
        rotationSound = GetNode<AudioStreamPlayer>("RotationSound");

        InitMap();
        InitBlocks();
        AddBlocksToNode();
        Reload();
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionPressed("ui_right"))
            xMoveCoordinate.x = 1;

        if (Input.IsActionPressed("ui_left"))
            xMoveCoordinate.x = -1;

        if (Input.IsActionPressed("ui_down"))
            yMoveCoorditane.y = 1;

        if (Input.IsActionJustPressed("ui_accept")
            || Input.IsActionJustPressed("ui_up")
            || (@event is InputEventMouse mouseEvent && mouseEvent.IsPressed())
        )
        {
            shouldRotate = true;
        }
    }
    public override void _Process(float delta)
    {
        if (gameEnded)
        {
            HandleEndGame();
            return;
        }

        frame++;
        HandleRotation();
        HandleInputXAxisShift();
        HandleInputYAxisShift();
        RefreshMoveData();
    }

    private void Reload()
    {
        frameTickRate = initFrameTickRate;
        collectedLines = 0;
        frame = 0;
        speedIncreasePoint = 0;
        ChangeBlocksVisibility(false);
        RefreshMap();
        InitNextTetromicon();
        InitNewTetromicon();
        UpdateBlocksPositions();
        ChangeBlocksVisibility(true);
        NextTetromiconCreatedEvent?.Invoke();
        gameEnded = false;
        GameStartedEvent?.Invoke();
    }

    private void InitMap()
    {
        map = new BaseBlock[mapSizeX, mapSizeY];
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                map[x, y] = new BaseBlock();
                map[x, y] = block.Instance<BaseBlock>();
                map[x, y].Position = GetMappedCoordinates(x, y);
                map[x, y].Disable();
                mapContainer.AddChild(map[x, y]);
            }
        }
    }

    private void RefreshMap()
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                map[x, y].Disable();
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
            mapContainer.AddChild(blocks[i]);
        }
    }

    private void UpdateBlocksPositions()
    {
        for (int i = 0; i < currentTetromicon.Coordinates.Length; i++)
        {
            blocks[i].Position = GetMappedCoordinates(currentTetromicon.Coordinates[i].x, currentTetromicon.Coordinates[i].y);
        }
    }

    private void InitNextTetromicon()
    {
        nextTetromicon = tetromiconFactory.Build();
        nextTetromicon.RandomRotate();
    }

    private void InitNewTetromicon()
    {
        currentTetromicon = nextTetromicon;
        currentTetromicon.MoveToCoordinate(new Coordinate(6, 0));
        currentTetromicon.MoveToPositiveCoordinates();
        InitNextTetromicon();
        NextTetromiconCreatedEvent?.Invoke();
    }

    private void CheckIfGameEnded()
    {
        for (int i = 0; i < currentTetromicon.Coordinates.Length; i++)
        {
            if (map[currentTetromicon.Coordinates[i].x, currentTetromicon.Coordinates[i].y].Filled)
            {
                gameEnded = true;
                GameEndedEvent?.Invoke();
                return;
            }
        }
    }

    private void HandleEndGame()
    {
        if (Input.IsActionJustPressed("ui_accept"))
        {
            Reload();
        }
    }

    private void HandleRotation()
    {
        if (!shouldRotate) { return; }

        var coordinates = currentTetromicon.GetNext90RotationCoordinates();
        if (ShiftAllowed(coordinates) && ShiftAllowed(currentTetromicon.RotationCoordinates))
        {
            rotationSound.Play();
            currentTetromicon.Rotate(coordinates);
            UpdateBlocksPositions();
            shouldRotate = false;
            return;
        }

        coordinates = currentTetromicon.GetNext270RotationCoordinates();
        if (ShiftAllowed(coordinates) && ShiftAllowed(currentTetromicon.RotationCoordinates))
        {
            rotationSound.Play();
            currentTetromicon.Rotate(coordinates);
            UpdateBlocksPositions();
            shouldRotate = false;
            return;
        }
        shouldRotate = false;
    }

    private void HandleInputXAxisShift()
    {
        if (xMoveCoordinate == Coordinate.Zero) { return; }

        var coordinates = currentTetromicon.GetCoordinatesIfMovedTo(xMoveCoordinate);
        if (ShiftAllowed(coordinates))
        {
            currentTetromicon.Coordinates = coordinates;
            UpdateBlocksPositions();
        }
    }

    private void HandleInputYAxisShift()
    {
        if (frame % frameTickRate == 0)
        {
            yMoveCoorditane.y = 1;
        }

        if (yMoveCoorditane == Coordinate.Zero) { return; }

        var coordinates = currentTetromicon.GetCoordinatesIfMovedTo(yMoveCoorditane);
        if (ShiftAllowed(coordinates))
        {
            currentTetromicon.Coordinates = coordinates;
        }
        else
        {
            FixStopedFigureOnMap();
            CollectLines();
            InitNewTetromicon();
            CheckIfGameEnded();
        }
        UpdateBlocksPositions();
    }

    private void CollectLines()
    {
        var filledLineIndexes = GetFilledLines();

        if (filledLineIndexes.Any())
        {
            RemoveFilledLines(filledLineIndexes);
            LowerCells(filledLineIndexes);

            collectedLines += filledLineIndexes.Count;
            HandleSpeedDecrease(filledLineIndexes.Count);
            HandleSpeedIncrease();

            LinesDestroyedEvent?.Invoke(filledLineIndexes.Count);
        }
    }

    private List<int> GetFilledLines()
    {
        var filledLineIndexes = new List<int>();
        var cellsInLine = map.GetLength(0);
        for (int lineIndex = map.GetLength(1) - 1; lineIndex >= 0; lineIndex--)
        {
            int lineCount = 0;
            for (int columnIndex = 0; columnIndex < map.GetLength(0); columnIndex++)
            {
                if (map[columnIndex, lineIndex].Filled)
                {
                    lineCount++;
                }
                else
                {
                    break;
                }
            }

            if (lineCount == cellsInLine)
            {
                filledLineIndexes.Add(lineIndex);
            }
        }
        return filledLineIndexes;
    }

    private void RemoveFilledLines(List<int> filledLineIndexes)
    {
        for (int i = 0; i < filledLineIndexes.Count; i++)
        {
            var lineIndex = filledLineIndexes[i];
            for (int columnIndex = 0; columnIndex < map.GetLength(0); columnIndex++)
            {
                map[columnIndex, lineIndex].AnimateDissapearing();
                // map[columnIndex, lineIndex].Disable();
            }
        }
    }

    private void LowerCells(List<int> filledLineIndexes)
    {
        for (int lineIndex = map.GetLength(1) - 1; lineIndex >= 0; lineIndex--)
        {
            var yOffset = filledLineIndexes.Count(x => x > lineIndex);
            if (yOffset > 0)
            {
                for (int columnIndex = 0; columnIndex < map.GetLength(0); columnIndex++)
                {
                    if (map[columnIndex, lineIndex].Filled)
                    {
                        var newLineIndex = lineIndex + yOffset;
                        map[columnIndex, lineIndex].Disable();
                        map[columnIndex, newLineIndex].Enable();
                    }
                }
            }
        }
    }

    private void HandleSpeedDecrease(int collectedLinesPerTick)
    {
        var ticks = frameTickRate;
        if (collectedLinesPerTick == 3)
        {
            ticks += 1;
        }
        else if (collectedLinesPerTick >= 4)
        {
            ticks += 3;
        }
        frameTickRate = ticks < initFrameTickRate ? ticks : initFrameTickRate;
    }

    private void HandleSpeedIncrease()
    {
        var currentDelta = collectedLines - speedIncreasePoint;
        if (currentDelta >= speedIncreaseDelta)
        {
            frameTickRate -= 1;
            speedIncreasePoint += speedIncreaseDelta;
        }
    }

    private void FixStopedFigureOnMap()
    {
        for (int i = 0; i < currentTetromicon.Coordinates.Length; i++)
        {
            var x = currentTetromicon.Coordinates[i].x;
            var y = currentTetromicon.Coordinates[i].y;
            map[x, y].Enable();
        }
    }

    private bool ShiftAllowed(Coordinate[] c)
    {
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i].x >= mapSizeX || c[i].x < 0 || c[i].y >= mapSizeY || c[i].y < 0 || map[c[i].x, c[i].y].Filled)
            {
                return false;
            }
        }
        return true;
    }

    private void RefreshMoveData()
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
