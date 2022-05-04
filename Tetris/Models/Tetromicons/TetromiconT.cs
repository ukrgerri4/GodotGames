using Godot;

public class TetromiconT : Tetromicon
{
    public override Coordinate[] Coordinates { get; set; } = 
        new Coordinate[] {
            new Coordinate(-1, 0),
            new Coordinate(0, 0),
            new Coordinate(0, -1),
            new Coordinate(1, 0)
        };

    public override Coordinate Pivot => Coordinates[1];
    private Coordinate[] _rotationCoordinates = new Coordinate[0];
    public override Coordinate[] RotationCoordinates => _rotationCoordinates;
    private enum RotationPosition
    {
        Degrees0,
        Degrees90,
        Degrees180,
        Degrees270
    }

    private RotationPosition rotationPosition;

    public TetromiconT()
    {
        Initialize(Coordinate.Zero);
    }

    public TetromiconT(Coordinate pivot)
    {
        Initialize(pivot);
    }

    private void Initialize(Coordinate pivot)
    {
        rotationPosition = (RotationPosition)(GD.Randi() % 4);
        for (int i = 0; i < Coordinates.Length; i++)
        {
            Coordinates[i] += pivot;
        }
        // Coordinates = GetPivotBasedCoordinates(pivot, rotationPosition);
    }

    public override Coordinate[] GetNextRotationCoordinates()
    {
        var nextPosition = GetNextPosition();
        var newCoordinates = GetPivotBasedCoordinates(Pivot, nextPosition);
        return newCoordinates;
    }

    public override void Rotate()
    {
        var nextPosition = GetNextPosition();
        var nextCoordinates = GetPivotBasedCoordinates(Pivot, nextPosition);
        rotationPosition = nextPosition;
        Coordinates = nextCoordinates;
    }

    private RotationPosition GetNextPosition()
    {
        var nextPosition = ((int)rotationPosition + 1) % 4;
        return (RotationPosition)nextPosition;
    }

    private Coordinate[] GetPivotBasedCoordinates(Coordinate pivot, RotationPosition position)
    {
        return TetromiconRotateHelper.Rotate90Clockwise(Coordinates, Pivot);
    }
}