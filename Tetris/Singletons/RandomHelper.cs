using Godot;

public class RandomHelper : Node
{
    private RandomNumberGenerator rng;

    public RandomHelper() {
        rng = new RandomNumberGenerator();
        rng.Randomize();
    }

    public int RandiRange(int from, int to) {
        return rng.RandiRange(from, to); 
    }
}