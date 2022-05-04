public class TetromiconRotateHelper {
    public static Coordinate[] Rotate90Degree(Coordinate[] coordinates, Coordinate pivot, bool clockwise = true) {
        if (clockwise) {
            return RotateClockwise(coordinates, pivot);
        }
        else {
            return RotateCounterClockwise(coordinates, pivot);
        }
    }

    private static Coordinate[] RotateClockwise(Coordinate[] coordinates, Coordinate pivot)
    {
        // Rotation rule: (x,y) -> (y, -x)
        for (int i = 0; i < coordinates.Length; i++)
        {
            var x = coordinates[i].x;
            var y = coordinates[i].y;
            coordinates[i] -= pivot;
            coordinates[i].x = y;
            coordinates[i].y = x * -1;
            coordinates[i] += pivot;
        }

        return coordinates;
    }

    private static Coordinate[] RotateCounterClockwise(Coordinate[] coordinates, Coordinate pivot)
    {
        // Rotation rule: (x,y) -> (-y, x)
        for (int i = 0; i < coordinates.Length; i++)
        {
            var x = coordinates[i].x;
            var y = coordinates[i].y;
            coordinates[i] -= pivot;
            coordinates[i].x = y * -1;
            coordinates[i].y = x;
            coordinates[i] += pivot;
        }

        return coordinates;
    }

}