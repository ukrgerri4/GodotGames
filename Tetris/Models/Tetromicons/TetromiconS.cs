public class TetromiconS : Tetromicon
{
    public override Coordinate[] Coordinates { get; set; } =
        new Coordinate[] {
            new Coordinate(-1, 1),
            new Coordinate(0, 1),
            new Coordinate(0, 0),
            new Coordinate(1, 0)
        };
    public override Coordinate Pivot => Coordinates[2];
    private Coordinate[] _rotationCoordinates = new Coordinate[0];
    public override Coordinate[] RotationCoordinates => _rotationCoordinates;

    public TetromiconS() {}
    public TetromiconS(Coordinate pivot) : base(pivot) {}
}