using System.Linq;

public abstract class Tetromicon
{
    public virtual Coordinate[] Coordinates { get; set; } = new Coordinate[4];
    public virtual Coordinate Pivot { get; set; }
    public virtual Coordinate[] RotationCoordinates => new Coordinate[0];

    public abstract void Rotate();
    public abstract Coordinate[] GetNextRotationCoordinates();
    public virtual Coordinate[] GetCoordinatesIfMovedTo(Coordinate to)
    {
        return Coordinates.Select(c => c + to).ToArray();
    }

    public virtual void MoveToCoordinate(Coordinate point)
    {
        for (int i = 0; i < Coordinates.Length; i++)
        {
            Coordinates[i] += point;
        }
    }

    public virtual void MoveToPositiveCoordinates()
    {
        var minX = Coordinates.Min(coordinate => coordinate.x);
        var minY = Coordinates.Min(coordinate => coordinate.y);
        var offset = new Coordinate(
            minX < 0 ? minX * -1 : 0,
            minY < 0 ? minY * -1 : 0
        );

        for (int i = 0; i < Coordinates.Length; i++)
        {
            Coordinates[i] += offset;
        }
    }
}