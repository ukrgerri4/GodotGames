using Godot;

public class ITetromicon: Tetromicon
{
    private enum RotationPosition
    {
        Horisontal,
        Vertical
    }

    private RotationPosition rotationPosition;

    public ITetromicon()
    {
        Initialize(Coordinate.Zero);
    }

    public ITetromicon(Coordinate pivot)
    {
        Initialize(pivot);
    }

    private void Initialize(Coordinate pivot) {
        rotationPosition = (RotationPosition)(GD.Randi() % 2);
        Coordinates = GetPivotBasedCoordinates(pivot, rotationPosition);
    }

    public override Coordinate[] TryRotate(Coordinate pivot)
    {
        var newPosition = GetNextPosition();
        var newCoordinates = GetPivotBasedCoordinates(pivot, newPosition);
        return newCoordinates;
    }

    private RotationPosition GetNextPosition() {
        var nextPosition = ((int)rotationPosition + 1) % 2;
        return (RotationPosition)nextPosition;
    }

    private Coordinate[] GetPivotBasedCoordinates(Coordinate pivot, RotationPosition position) {
        var coordinates = new Coordinate[4];

        if (rotationPosition == RotationPosition.Vertical) {
            coordinates[0] = new Coordinate(pivot.x + 1, pivot.y);
            coordinates[1] = new Coordinate(pivot.x, pivot.y);
            coordinates[2] = new Coordinate(pivot.x - 1, pivot.y);
            coordinates[3] = new Coordinate(pivot.x - 2, pivot.y);
        }
        else if (rotationPosition == RotationPosition.Horisontal) {
            coordinates[0] = new Coordinate(pivot.x, pivot.y - 1);
            coordinates[1] = new Coordinate(pivot.x, pivot.y);
            coordinates[2] = new Coordinate(pivot.x, pivot.y + 1);
            coordinates[3] = new Coordinate(pivot.x, pivot.y + 2);
        }

        return coordinates;
    }
}