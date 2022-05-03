using Godot;

public class TetromiconFactory: Node {
    public Tetromicon Build() {
        return new ITetromicon();
    }

    public Tetromicon BuildInPosition(Coordinate point) {
        return new ITetromicon(point);
    }
}