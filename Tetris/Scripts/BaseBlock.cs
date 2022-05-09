using Godot;

public class BaseBlock : Node2D
{
    public CellType Type { get; set; } = CellType.Empty;

    public bool Filled => Type == CellType.Filled;
    public bool Empty => Type == CellType.Empty;

    private Tween tween;
    private ColorRect bgColor;

    public override void _Ready()
    {
        bgColor = GetNode<ColorRect>("BgColor");
        tween = GetNode<Tween>("Tween");
    }

    public void AnimateDissapearing() {
        tween.InterpolateProperty(bgColor, "color", bgColor.Color, new Color(255,255,255), 0.1f);
        tween.Start();
    }

    private void OnTweenCompleted(Object @object, NodePath nodePath) {
        GD.Print("Tween end");
        Disable();
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
