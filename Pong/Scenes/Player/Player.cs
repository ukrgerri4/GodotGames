using Godot;
using System;

public partial class Player : CharacterBody2D
{
    private PackedScene _roocketTemplate;
    private MapEventsBus _mapEventsBus;
    public int DeviceId = 0;
    public string Nickname;

    private PlayerInputManager _inputManager;
    public float PanelWidth
    {
        get
        {
            return ((RectangleShape2D)GetNode<CollisionShape2D>("CollisionShape2D").Shape).Size.X;
        }
    }

    public float Speed = 300f;

    public override void _Ready()
    {
        _roocketTemplate = GD.Load<PackedScene>("res://Scenes/Rocket/Rocket.tscn");
        _inputManager = new PlayerInputManager(DeviceId);
    }

    public override void _Input(InputEvent @event)
    {
        if (_inputManager.IsRocketLaunchButtonPressed())
        {
            LaunchRocket();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        var motion = new Vector2(_inputManager.GetLeftXStrength(), 0);
        if (motion.LengthSquared() > 0.05)
        {
            Move(delta, motion);
        }
    }

    private void Move(double delta, Vector2 motion)
    {
        motion = motion.Normalized();
        motion = motion * Speed * (float)delta;
        if (_inputManager.IsPadAccelerateButtonPressed())
        {
            motion = motion * 2;
        }
        // TODO: handle collision
        MoveAndCollide(motion);
    }

    public void TouchedByBall()
    {
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
            GetTree().Paused = false;
        }
    }

    private void LaunchRocket()
    {
        var rocket = _roocketTemplate.Instantiate<Rocket>();
        rocket.Init(_inputManager);
        AddChild(rocket);
        rocket.TopLevel = true;
        rocket.GlobalPosition = new Vector2(400, 400);
    }
}
