using Godot;

public partial class GameManager : Node
{
    private Map _map;
    private Node _balls;
    private PackedScene _roocketTemplate;
    private PackedScene _modifierTemplate;
    private PackedScene _ballTemplate;

    public override void _Ready()
    {
        _roocketTemplate = GD.Load<PackedScene>("res://Scenes/Rocket/Rocket.tscn");
        _modifierTemplate = GD.Load<PackedScene>("res://Scenes/Modifiers/Modifier.tscn");
        _ballTemplate = GD.Load<PackedScene>("res://Scenes/Ball/Ball.tscn");

        _map = GetNode<Map>("/root/Main/Game/Map");
        _balls = GetNode<Node>("/root/Main/Game/Balls");
    }

    public void CreateModifier(SimpleBlock block)
    {
        var modifier = _modifierTemplate.Instantiate<Modifier>();
        modifier.GlobalPosition = block.GlobalPosition;
        modifier.Init(block.LastTouchedPlayer.ItemFallDirection);
        _map.AddChild(modifier);
    }

    public void AddBall()
    {
        var balls = _balls.GetChildren();
        if (balls.Count > 0)
        {
            var ball = _ballTemplate.Instantiate<Ball>();
            ball.GlobalPosition = ((Ball)balls[0]).GlobalPosition;
            ball.MoveVelocity = ((Ball)balls[0]).MoveVelocity * -1;
            _balls.CallDeferred(Node.MethodName.AddChild, ball);
        }
    }

    // public Rocket LaunchRocket(Player player)
    // {
    // 	var rocket = _roocketTemplate.Instantiate<Rocket>();
    // 	rocket.TopLevel = true;
    // 	rocket.GlobalRotationDegrees = player.GlobalRotationDegrees;
    // 	rocket.GlobalPosition = player.GlobalPosition * 0.9f;
    // 	rocket.Init(player.InputManager, (player.Marker2D.GlobalPosition - player.GlobalPosition).Normalized());
    // 	_map.AddChild(rocket);
    // 	return rocket;
    // }
}