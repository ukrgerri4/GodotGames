public class TAreaBlock : BaseAreaBlock
{
    public override void RotateBlock()
    {
        RotationDegrees = rotateBlockHelper.GetRoundRotation(RotationDegrees);
    }
}
