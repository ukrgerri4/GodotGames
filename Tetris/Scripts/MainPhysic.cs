using Godot;

public class Main : Node2D
{
    #region Properties
    private int mapUnit = 50;
    private int frame = 0;
    private int speed = 60;
    private bool shouldRotate = false;

    private RandomNumberGenerator rng;
    private PackedScene[] blocks;
    private BaseAreaBlock activeTeromicon;
    private Vector2 nextMove = Vector2.Zero;
    #endregion
    public override void _Ready()
    {
        rng = new RandomNumberGenerator();
        rng.Randomize(); 
        blocks = new PackedScene[] {
            GD.Load<PackedScene>("res://Scenes/Blocks/IAreaBlock.tscn"),
            GD.Load<PackedScene>("res://Scenes/Blocks/LAreaBlock.tscn"),
            GD.Load<PackedScene>("res://Scenes/Blocks/JAreaBlock.tscn"),
            GD.Load<PackedScene>("res://Scenes/Blocks/SAreaBlock.tscn"),
            GD.Load<PackedScene>("res://Scenes/Blocks/ZAreaBlock.tscn"),
            GD.Load<PackedScene>("res://Scenes/Blocks/OAreaBlock.tscn"),
            GD.Load<PackedScene>("res://Scenes/Blocks/TAreaBlock.tscn")
        };
        InitNewBlock();
    }

    public override void _Process(float delta)
    { 
        frame++;
        HandleRotation();
        HandleFalling();
        HandleChangeActiveTetromiconPosition();
        HandleStop();
        SetNextStepDefaults();
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionPressed("ui_right"))
            nextMove.x = mapUnit;

        if (Input.IsActionPressed("ui_left"))
            nextMove.x = -1 * mapUnit;

        if (Input.IsActionPressed("ui_down"))
            nextMove.y = mapUnit;

        if (Input.IsActionPressed("ui_accept"))
            shouldRotate = true;
        
        /* For SPACE only handler */
        // if (@event is InputEventKey eventKey && eventKey.Pressed && eventKey.Scancode == (int)KeyList.Space)
        //     shouldRotate = true;

        if (@event is InputEventKey eventKey && eventKey.Pressed && eventKey.Scancode == (int)KeyList.Control)
            stop = true;
    }
    
    private void InitNewBlock() {
        var tetromiconIndex = rng.RandiRange(0, 6);
        var iBlockInstance = blocks[tetromiconIndex].Instance<BaseAreaBlock>();
        iBlockInstance.Position = new Vector2(335, 175);
        AddChild(iBlockInstance);
        activeTeromicon = iBlockInstance;
    }

    private void HandleFalling()
    {
        if (frame % speed == 0) {
            nextMove.y = mapUnit;
        }
    }

    private void HandleRotation()
    {
        if (shouldRotate) {
            // activeTeromicon.RotationDegrees = activeTeromicon.RotationDegrees > 0 ? 0 : rotationDegrees;
            activeTeromicon.RotateBlock();
            shouldRotate = false;
        }
    }

    private void HandleChangeActiveTetromiconPosition()
    {
        activeTeromicon.Position += nextMove;
    }

    private bool stop = false;
    private void HandleStop()
    {
        if (stop) {
            InitNewBlock();
            stop = false;
        }   
    }

    private void SetNextStepDefaults() {
        nextMove.x = 0;
        nextMove.y = 0;
    }
}
