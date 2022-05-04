using Godot;

public class TetromiconS : Tetromicon
{
    public override Coordinate Pivot => Coordinates[2];
    private Coordinate[] _rotationCoordinates;
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
        rotationPosition = (RotationPosition)(GD.Randi() % 2);
        Coordinates = GetPivotBasedCoordinates(pivot, rotationPosition);
    }

    public override Coordinate[] TryRotate()
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
        var coordinates = new Coordinate[4];
        if (position == RotationPosition.Horisontal)
        {
            coordinates[0] = new Coordinate(pivot.x - 1, pivot.y + 1);
            coordinates[1] = new Coordinate(pivot.x, pivot.y + 1);
            coordinates[2] = new Coordinate(pivot.x, pivot.y);
            coordinates[3] = new Coordinate(pivot.x + 1, pivot.y);

            _rotationCoordinates = new Coordinate[]
            {
                new Coordinate(pivot.x + 1, pivot.y + 1)
            };
        }
        else if (position == RotationPosition.Vertical)
        {
            coordinates[0] = new Coordinate(pivot.x - 1, pivot.y - 1);
            coordinates[1] = new Coordinate(pivot.x - 1, pivot.y);
            coordinates[2] = new Coordinate(pivot.x, pivot.y);
            coordinates[3] = new Coordinate(pivot.x, pivot.y + 1);

            _rotationCoordinates = new Coordinate[]
            {
                new Coordinate(pivot.x, pivot.y - 1),
                new Coordinate(pivot.x + 1, pivot.y - 1)
            };
        }

        return coordinates;
    }
}