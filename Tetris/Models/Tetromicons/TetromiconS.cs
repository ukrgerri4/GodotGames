using Godot;

public class TetromiconS : Tetromicon
{
    public override Coordinate[] Coordinates { get; set; } =
        new Coordinate[] {
            new Coordinate(-1,0),
            new Coordinate(0,0),
            new Coordinate(0, -1),
            new Coordinate(1, -1)
        };
    public override Coordinate Pivot => Coordinates[1];
    private Coordinate[] _rotationCoordinates = new Coordinate[0];
    public override Coordinate[] RotationCoordinates => _rotationCoordinates;
    private enum RotationPosition
    {
        Horisontal,
        Vertical
    }

    private RotationPosition rotationPosition;

    public TetromiconS()
    {
        Initialize(Coordinate.Zero);
    }

    public TetromiconS(Coordinate pivot)
    {
        Initialize(pivot);
    }

    private void Initialize(Coordinate pivot)
    {
        // rotationPosition = (RotationPosition)(GD.Randi() % 2);
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
        var nextPosition = ((int)rotationPosition + 1) % 2;
        return (RotationPosition)nextPosition;
    }

    private Coordinate[] GetPivotBasedCoordinates(Coordinate pivot, RotationPosition position)
    {
        return TetromiconRotateHelper.Rotate90CounterClockwise(Coordinates, Pivot);
    }
}