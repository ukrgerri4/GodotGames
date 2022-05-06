public class TetromiconRotateHelper
{
    public static Coordinate[] Rotate90Clockwise(Coordinate[] coordinates, Coordinate pivot)
    {
        // Rotation rule: (x,y) -> (y, -x)
        var newCoordinates = new Coordinate[coordinates.Length];
        for (int i = 0; i < coordinates.Length; i++)
        {
            newCoordinates[i] = coordinates[i] - pivot;
            var x = newCoordinates[i].x;
            var y = newCoordinates[i].y;
            newCoordinates[i].x = y;
            newCoordinates[i].y = x * -1;
            newCoordinates[i] += pivot;
        }

        return newCoordinates;
    }

    public static Coordinate[] Rotate180Clockwise(Coordinate[] coordinates, Coordinate pivot)
    {
        // Rotation rule: (x,y) -> (-x, -y)
        var newCoordinates = new Coordinate[coordinates.Length];
        for (int i = 0; i < coordinates.Length; i++)
        {
            newCoordinates[i] = coordinates[i] - pivot;
            var x = newCoordinates[i].x;
            var y = newCoordinates[i].y;
            newCoordinates[i].x = x * -1;
            newCoordinates[i].y = y * -1;
            newCoordinates[i] += pivot;
        }

        return newCoordinates;
    }

    public static Coordinate[] Rotate270Clockwise(Coordinate[] coordinates, Coordinate pivot)
    {
        // Rotation rule: (x,y) -> (-y, x)
        var newCoordinates = new Coordinate[coordinates.Length];
        for (int i = 0; i < coordinates.Length; i++)
        {
            newCoordinates[i] = coordinates[i] - pivot;
            var x = newCoordinates[i].x;
            var y = newCoordinates[i].y;
            newCoordinates[i].x = y * -1;
            newCoordinates[i].y = x;
            newCoordinates[i] += pivot;
        }

        return newCoordinates;
    }

    public static Coordinate[] Rotate90CounterClockwise(Coordinate[] coordinates, Coordinate pivot)
    {
        return Rotate270Clockwise(coordinates, pivot);
    }

}