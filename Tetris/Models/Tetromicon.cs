using System.Linq;

public abstract class Tetromicon {
    public virtual Coordinate[] Coordinates { get; set; } = new Coordinate[4];
    public virtual Coordinate Pivot { get; set; }
    public virtual Coordinate[] RotationCoordinates => new Coordinate[0];

    public Coordinate[] PositiveCoordinates
    {
        get {
            var minX = Coordinates.Min(coordinate => coordinate.x);
            var minY = Coordinates.Min(coordinate => coordinate.y);
            var offset = new Coordinate(
                minX < 0 ? minX * -1 : 0,
                minY < 0 ? minY * -1 : 0
            );

            return Coordinates.Select(x => x + offset).ToArray();
        }
    }

    public abstract Coordinate[] TryRotate();
    public abstract void Rotate();

    public Coordinate[] TryMove(Coordinate step) {
        return Coordinates.Select(c => c + step).ToArray();
    }
}