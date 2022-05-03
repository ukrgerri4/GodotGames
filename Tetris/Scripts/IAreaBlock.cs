public class IAreaBlock : BaseAreaBlock
{
    public override void RotateBlock()
    {
        RotationDegrees = rotateBlockHelper.GetTwoWayRotation(RotationDegrees);
    }
}
