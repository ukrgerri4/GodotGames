using Godot;

public class BaseBlock : Node2D
{
    private Color defaultColor = new Color(255,0,0,92);
    [Signal]
    public delegate void AnimationEnded();

    public CellType Type { get; set; } = CellType.Empty;

    public bool Filled => Type == CellType.Filled;
    public bool Empty => Type == CellType.Empty;

    private Tween tween;
    
    private ColorRect bgColor;

    private Tetris parent;

    public override void _Ready()
    {
        bgColor = GetNode<ColorRect>("BgColor");
        tween = GetNode<Tween>("Tween");
    }

    public void AnimateDissapearing() {
        tween.InterpolateProperty(bgColor, "color", new Color(15,15,15,120), defaultColor, 3f);
        // tween.Start();
    }

    private void OnTweenCompleted(Object @object, NodePath nodePath) {
        Disable();
        EmitSignal(nameof(AnimationEnded));
    }

    public void Disable()
    {
        Type = CellType.Empty;
        Visible = false;
    }

    public void Enable()
    {
        Type = CellType.Filled;
        Visible = true;
    }
}
