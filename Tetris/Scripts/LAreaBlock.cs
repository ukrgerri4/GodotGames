public class LAreaBlock : BaseAreaBlock
{
    public override void RotateBlock()
    {
        RotationDegrees = rotateBlockHelper.GetRoundRotation(RotationDegrees);
    }
}
