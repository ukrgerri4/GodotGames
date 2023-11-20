using Godot;

public class BaseBlock : Node2D
{
    private Color defaultColor = new Color("920000");
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

    public void AnimateDissapearing()
    {
        tween.InterpolateProperty(bgColor, "color", bgColor.Color, new Color(0, 0, 0), 0.4f);
        tween.Start();
    }

    private void OnTweenCompleted(Object @object, NodePath nodePath)
    {
        bgColor.Color = defaultColor;
        Disable();
        EmitSignal(nameof(AnimationEnded));
    }

    public void Disable()
    {
        Type = CellType.Empty;
        Visible = false;
        // EnableDefaultBgColor();

    }

    public void Enable()
    {
        Type = CellType.Filled;
        // EnableDefaultBgColor();
        Visible = true;
    }

    private void EnableDefaultBgColor()
    {
        if (bgColor != null)
        {
            bgColor.Color = defaultColor;
        }
    }
}
