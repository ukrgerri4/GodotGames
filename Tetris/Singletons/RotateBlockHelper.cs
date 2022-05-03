using Godot;

public class RotateBlockHelper: Node {
    private float rotationDegreesStep = 90;
    
    public float GetRoundRotation(float currentRotationValue) {
        return currentRotationValue + rotationDegreesStep;
    }

    public float GetTwoWayRotation(float currentRotationValue) {
        return currentRotationValue > 0 ? 0 : rotationDegreesStep;
    }
}