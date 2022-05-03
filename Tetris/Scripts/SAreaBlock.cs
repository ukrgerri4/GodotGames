public class SAreaBlock : BaseAreaBlock
{
    public override void RotateBlock()
    {
        RotationDegrees = rotateBlockHelper.GetTwoWayRotation(RotationDegrees);
    }
}
