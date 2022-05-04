using System.Linq;
using Godot;

public class TetromiconO: Tetromicon
{
    public TetromiconO()
    {
        InitCoordinates(Pivot);
    }

    public TetromiconO(Coordinate pivot)
    {
        Pivot = pivot;
        InitCoordinates(pivot);
    }

    public override Coordinate[] GetNextRotationCoordinates()
    {
        return Coordinates.Select(c => c).ToArray();
    }

    public override void Rotate() {}

    private void InitCoordinates(Coordinate pivot)
    {
        Coordinates[0] = new Coordinate(pivot.x, pivot.y);
        Coordinates[1] = new Coordinate(pivot.x + 1, pivot.y);
        Coordinates[2] = new Coordinate(pivot.x, pivot.y + 1);
        Coordinates[3] = new Coordinate(pivot.x + 1, pivot.y + 1);
    }
}
