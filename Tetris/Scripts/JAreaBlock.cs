public class JAreaBlock : BaseAreaBlock
{
    public override void RotateBlock()
    {
        RotationDegrees = rotateBlockHelper.GetRoundRotation(RotationDegrees);
    }
}
