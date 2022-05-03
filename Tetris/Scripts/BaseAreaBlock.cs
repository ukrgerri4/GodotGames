using Godot;

public abstract class BaseAreaBlock: Node2D, IRotatebleBlock
{
    protected RotateBlockHelper rotateBlockHelper;
    protected Area2D[] blocks;

    public override void _Ready()
    {
        rotateBlockHelper = GetNode<RotateBlockHelper>("/root/RotateBlockHelper");
        blocks = new Area2D[] {
            GetNode<Area2D>("BaseBlock"),
            GetNode<Area2D>("BaseBlock2"),
            GetNode<Area2D>("BaseBlock3"),
            GetNode<Area2D>("BaseBlock4")
        };

        foreach (var block in blocks)
        {
            GD.Print(block.GlobalPosition.ToString());
        }
    }

    public virtual void RotateBlock() { }
}