using Godot;

public class TetromiconI: Tetromicon
{
    public override Coordinate Pivot => Coordinates[2];
    public override Coordinate[] RotationCoordinates
    {
        get
        {
            return new Coordinate[]
            {
                new Coordinate(Pivot.x + 1, Pivot.y - 1),
                new Coordinate(Pivot.x - 1, Pivot.y + 1),
                new Coordinate(Pivot.x - 2, Pivot.y + 1),
                new Coordinate(Pivot.x - 1, Pivot.y + 2),
                new Coordinate(Pivot.x - 2, Pivot.y + 2)
            };
        }
    }
    private enum RotationPosition
    {
        Horisontal,
        Vertical
    }

    private RotationPosition rotationPosition;

    public TetromiconI()
    {
        Initialize(Coordinate.Zero);
    }

    public TetromiconI(Coordinate pivot)
    {
        Initialize(pivot);
    }

    private void Initialize(Coordinate pivot) {
        rotationPosition = (RotationPosition)(GD.Randi() % 2);
        Coordinates = GetPivotBasedCoordinates(pivot, rotationPosition);
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

    private RotationPosition GetNextPosition() {
        var nextPosition = ((int)rotationPosition + 1) % 2;
        return (RotationPosition)nextPosition;
    }

    private Coordinate[] GetPivotBasedCoordinates(Coordinate pivot, RotationPosition position) {
        var coordinates = new Coordinate[4];

        if (position == RotationPosition.Vertical) {
            coordinates[0] = new Coordinate(pivot.x, pivot.y + 2);
            coordinates[1] = new Coordinate(pivot.x, pivot.y + 1);
            coordinates[2] = new Coordinate(pivot.x, pivot.y);
            coordinates[3] = new Coordinate(pivot.x, pivot.y - 1);
        }
        else if (position == RotationPosition.Horisontal) {
            coordinates[0] = new Coordinate(pivot.x - 2, pivot.y);
            coordinates[1] = new Coordinate(pivot.x - 1, pivot.y);
            coordinates[2] = new Coordinate(pivot.x, pivot.y);
            coordinates[3] = new Coordinate(pivot.x + 1, pivot.y);
        }

        return coordinates;
    }
}