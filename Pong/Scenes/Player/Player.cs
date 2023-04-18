using Godot;
using System;

public partial class Player : CharacterBody2D
{
    public ControllerType ControllerType;
    private MapEventsBus _mapEventsBus;
    public int JoyPadId = -1;
    public string Nickname;

    public float Speed = 300f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ControllerType = ControllerType.Keyboard;
        _mapEventsBus = GetNode<MapEventsBus>("/root/MapEventsBus");
        _mapEventsBus.AddMapRotatingHandler(OnMapRotating);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }

    public override void _PhysicsProcess(double delta)
    {
        if (JoyPadId == -1)
        {
            MoveByKeyboard(delta);
        }
        else
        {
            MoveByJoy(delta);
        }
    }

    private void MoveByKeyboard(double delta)
    {
        var motion = new Vector2(Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left"), 0);
        Move(delta, motion);
    }

    private void MoveByJoy(double delta)
    {
        Vector2 motion = Vector2.Zero;
        if (RotationDegrees != 0)
        {
            motion.Y = Input.GetJoyAxis(JoyPadId, JoyAxis.LeftY);
        }
        else
        {
            motion.X = Input.GetJoyAxis(JoyPadId, JoyAxis.LeftX);
        }

        if (motion.LengthSquared() > 0.05)
        {
            Move(delta, motion);
        }
    }

    private void Move(double delta, Vector2 motion)
    {
        motion = motion.Normalized();
        motion = motion * Speed * (float)delta;
        if (Input.IsActionPressed("accelerate"))
        {
            motion = motion * 2;
        }
        MoveAndCollide(motion);
    }

    public void TouchedByBall()
    {
        GD.Print("Player touched by Ball.");
        // add points
        // activete post hit ball
    }

    private void OnMapRotating(MapRotateState state)
    {
        if (state == MapRotateState.Started)
        {
            GetTree().Paused = true;
        }
        else if (state == MapRotateState.Ended)
        {
            GD.Print(GlobalRotationDegrees);
            GetTree().Paused = false;
        }
    }
}
