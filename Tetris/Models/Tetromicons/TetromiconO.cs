using System.Linq;

public class TetromiconO : Tetromicon
{
    public override Coordinate[] Coordinates { get; set; } =
        new Coordinate[] {
            new Coordinate(0, 0),
            new Coordinate(1, 0),
            new Coordinate(0, 1),
            new Coordinate(1, 1),
        };
    public override Coordinate Pivot => Coordinates[0];

    public override Coordinate[] RotationCoordinates => new Coordinate[0];
    public TetromiconO() { }
    public TetromiconO(Coordinate pivot) : base(pivot) { }

    public override Coordinate[] GetNextRotationCoordinates()
    {
        return Coordinates.Select(c => c).ToArray();
    }

    public override void Rotate() { }
}
