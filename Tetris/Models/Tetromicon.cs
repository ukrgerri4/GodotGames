using System.Linq;
using Godot;

public abstract class Tetromicon {
    public Coordinate[] Coordinates { get; set; } = new Coordinate[4];

    public abstract Coordinate[] TryRotate(Coordinate pivot);

    public Coordinate[] PositiveCoordinates
    {
        get {
            var minX = Coordinates.Min(coordinate => coordinate.x);
            var minY = Coordinates.Min(coordinate => coordinate.y);
            var offset = new Coordinate(
                minX < 0 ? minX * -1 : 0,
                minY < 0 ? minY * -1 : 0
            );

            var newCoordinates = Coordinates.Select(x => x + offset).ToArray();
            return newCoordinates;
        }
    }

    public Coordinate[] TryMove(Coordinate step) {
        return Coordinates.Select(c => c + step).ToArray();
        // for (int i = 0; i < Coordinates.Length; i++)
        // {
        //     Coordinates[i] = Coordinates[i] + step;
        // }
    }
}