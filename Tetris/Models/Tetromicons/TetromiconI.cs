public class TetromiconI : Tetromicon
{
    public override Coordinate[] Coordinates { get; set; } =
        new Coordinate[] {
            new Coordinate(-2, 0),
            new Coordinate(-1, 0),
            new Coordinate(0, 0),
            new Coordinate(1, 0),
        };
    public override Coordinate Pivot => Coordinates[2];
    public override Coordinate[] RotationCoordinates
    {
        get
        {
            return new Coordinate[]
            {
                new Coordinate(Pivot.x + 1, Pivot.y + 1),
                new Coordinate(Pivot.x - 1, Pivot.y - 1),
                new Coordinate(Pivot.x - 2, Pivot.y - 1),
                new Coordinate(Pivot.x - 1, Pivot.y - 2),
                new Coordinate(Pivot.x - 2, Pivot.y - 2)
            };
        }
    }
    
    private bool isHorisontal = true;

    public TetromiconI() {}
    public TetromiconI(Coordinate pivot) : base(pivot) { }

    public override Coordinate[] GetNextRotationCoordinates()
    {
        return isHorisontal 
            ? TetromiconRotateHelper.Rotate90CounterClockwise(Coordinates, Pivot)
            : TetromiconRotateHelper.Rotate90Clockwise(Coordinates, Pivot);
    }

    public override void Rotate()
    {
        Coordinates = GetNextRotationCoordinates();
        isHorisontal = !isHorisontal;
    }
}