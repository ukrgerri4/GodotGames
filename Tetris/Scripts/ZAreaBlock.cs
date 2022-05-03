public class ZAreaBlock : BaseAreaBlock
{
    public override void RotateBlock()
    {
        RotationDegrees = rotateBlockHelper.GetTwoWayRotation(RotationDegrees);
    }
}
