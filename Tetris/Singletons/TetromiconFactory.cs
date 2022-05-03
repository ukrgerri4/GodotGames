using Godot;

public class TetromiconFactory: Node {
    public Tetromicon Build() {
        return new TetromiconO();
    }

    public Tetromicon BuildInPosition(Coordinate point) {
        return new TetromiconI(point);
    }
}