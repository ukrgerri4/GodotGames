using System.Collections.Generic;
using System.Linq;
using Godot;

/*
    1. Next figure
    2. Random rotation on start
    3. First page
    4. Save stats
    5. Decrease speed after 4 lines collect by 1 time (4 lines - 5 speed, 3 lines - 2 speed)
    6. Optimization
    7. End game
*/
public class MainCoordinates : Node2D
{
    #region Properties
    private int initSpeed = 60;
    private int mapSizeX = 10;
    private int mapSizeY = 20;
    private int mapOffset = 25;
    private int mapUnit = 50;
    private int marginX = 110;
    private int marginY = 280;
    private TetromiconFactory tetromiconFactory;
    private int frame = 0;
    public int frameTickRate = 60;
    public int collectedLines = 0;
    private bool shouldRotate = false;
    private bool gameEnded = false;
    private Cell[,] map;
    private Tetromicon currentTetromicon;
    public Tetromicon nextTetromicon;

    private Coordinate yMoveCoorditane = Coordinate.Zero;
    private Coordinate xMoveCoordinate = Coordinate.Zero;

    private PackedScene block;
    private Node2D[] blocks;

    public delegate void LinesDestroyed(int linesDestroyed);
    public event LinesDestroyed LinesDestroyedEvent;

    public delegate void NextTetromiconCreated();
    public event NextTetromiconCreated NextTetromiconCreatedEvent;

    public delegate void GameStarted();
    public event GameStarted GameStartedEvent;
    public delegate void GameEnded();
    public event GameEnded GameEndedEvent;
    #endregion

    public override void _Ready()
    {
        GD.Randomize();
        tetromiconFactory = GetNode<TetromiconFactory>("/root/TetromiconFactory");
        block = GD.Load<PackedScene>("res://Scenes/BaseBlock.tscn");

        // InitDebugMode();
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

        // if (OS.IsDebugBuild())
        // {
        //     if (Input.IsActionPressed("ui_up"))
        //         yMoveCoorditane.y = -1;
        // }

        if (Input.IsActionPressed("ui_down"))
            yMoveCoorditane.y = 1;

        if (Input.IsActionJustPressed("ui_accept") || Input.IsActionJustPressed("ui_up"))
            shouldRotate = true;

        /* For SPACE only handler */
        // if (@event is InputEventKey eventKey && eventKey.Pressed && eventKey.Scancode == (int)KeyList.Space)
        //     shouldRotate = true;

        // if (@event is InputEventKey eventKey && eventKey.Pressed && eventKey.Scancode == (int)KeyList.Control)
        //     debugStop = true;
    }
    public override void _Process(float delta)
    {
        if (gameEnded) {
            HandleEndGame();
            return;
        }

        frame++;
        HandleRotation();
        HandleInputXAxisShift();
        HandleInputYAxisShift();
        SetNextStepDefaults();
    }

    private void InitDebugMode()
    {
        if (OS.IsDebugBuild())
        {
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
    }

    private void Reload()
    {
        frameTickRate = initSpeed;
        collectedLines = 0;
        frame = 0;
        ChangeBlocksVisibility(false);
        RefreshMap();
        InitNextTetromicon();
        InitNewTetromicon();
        UpdateBlocksPositions();
        ChangeBlocksVisibility(true);
        LinesDestroyedEvent?.Invoke(0);
        NextTetromiconCreatedEvent?.Invoke();
        gameEnded = false;
        GameStartedEvent?.Invoke();
    }

    private void InitMap()
    {
        map = new Cell[mapSizeX, mapSizeY];
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                map[x, y] = new Cell();
                map[x, y].Block = block.Instance<Node2D>();
                map[x, y].Block.Position = GetMappedCoordinates(x, y);
                map[x, y].Disable();
                AddChild(map[x, y].Block);
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

    private void InitNextTetromicon()
    {
        nextTetromicon = tetromiconFactory.Build();
        // TODO: nextTetromicon.RandomRotate(?);
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
            if (map[currentTetromicon.Coordinates[i].x, currentTetromicon.Coordinates[i].y].Type == CellType.Filled) {
                gameEnded = true;
                GameEndedEvent?.Invoke();
                return;
            }
        }
    }

    private void HandleEndGame()
    {
        if (Input.IsActionJustPressed("ui_accept")) {
            Reload();
        }
    }

    private void HandleRotation()
    {
        if (!shouldRotate) { return; }

        var coordinates = currentTetromicon.GetNextRotationCoordinates();
        if (ShiftAllowed(coordinates) && ShiftAllowed(currentTetromicon.RotationCoordinates))
        {
            currentTetromicon.Rotate();
        }
        UpdateBlocksPositions();
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
            for (int i = 0; i < currentTetromicon.Coordinates.Length; i++)
            {
                var x = currentTetromicon.Coordinates[i].x;
                var y = currentTetromicon.Coordinates[i].y;
                map[x, y].Enable();
            }
            CheckLines();
            InitNewTetromicon();
            CheckIfGameEnded();
        }
        UpdateBlocksPositions();
    }

    private void CheckLines()
    {
        var lineIndexesToDelete = new List<int>();

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

            if (lineCount == map.GetLength(0))
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
                    map[columnIndex, lineIndex].Disable();
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
                            map[columnIndex, lineIndex].Disable();
                            map[columnIndex, newLineIndex].Enable();
                        }
                    }
                }
            }

            collectedLines += lineIndexesToDelete.Count;
            frameTickRate -= lineIndexesToDelete.Count;
            LinesDestroyedEvent?.Invoke(lineIndexesToDelete.Count);
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
