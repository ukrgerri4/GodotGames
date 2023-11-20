public class TetromiconJ : Tetromicon
{
    public override Coordinate[] Coordinates { get; set; } =
        new Coordinate[] {
            new Coordinate(0, -1),
            new Coordinate(0, 0),
            new Coordinate(0, 1),
            new Coordinate(-1, 1)
        };
    public override Coordinate Pivot => Coordinates[1];
    private Coordinate[] _rotationCoordinates = new Coordinate[0];
    public override Coordinate[] RotationCoordinates => _rotationCoordinates;

    public TetromiconJ() { }
    public TetromiconJ(Coordinate pivot) : base(pivot) { }
}