using System;
using System.Linq;
using System.Reflection;
using Godot;

public class TetromiconFactory: Node {
    private Type[] tetromiconTypes;
    public TetromiconFactory()
    {
        tetromiconTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => !x.IsAbstract)
            .Where(x => x.IsSubclassOf(typeof(Tetromicon)))
            .ToArray();
    }

    public Tetromicon Build() {
        return new TetromiconO();
    }

    public Tetromicon BuildInPosition(Coordinate point) {
        var randomIndex = GD.Randi() % tetromiconTypes.Length;
        var randomType = tetromiconTypes[randomIndex];
        return (Tetromicon)Activator.CreateInstance(randomType, point);
    }


}