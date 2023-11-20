public struct Coordinate {
    public int x;
    public int y;

    public Coordinate(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public bool Equals(Coordinate c) => x == c.x && y == c.y;

    public override bool Equals(object obj) => obj is Coordinate c && this.Equals(c);

    public override int GetHashCode() => (x, y).GetHashCode();

    public override string ToString() => $"[{x},{y}]";


    public static Coordinate operator +(Coordinate a, Coordinate b) => new Coordinate(a.x + b.x, a.y + b.y);
    public static Coordinate operator -(Coordinate a, Coordinate b) => new Coordinate(a.x - b.x, a.y - b.y);
    public static bool operator ==(Coordinate a, Coordinate b) => a.Equals(b);
    public static bool operator !=(Coordinate a, Coordinate b) => !a.Equals(b);


    public static Coordinate Zero => new Coordinate(0,0);
}