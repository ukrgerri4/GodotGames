using Godot;
using System;

public class ScorePanel : Panel
{
    private MainCoordinates parent;
    private Label ScoreValueLabel;
    private Label LineCountValueLabel;
    private Label MultiplyValueLabel;
    private Label SpeedValueLabel;
    private int lineCost = 10;

    private int score = 0;
    private int multiply = 1;
    private int maxSpeed = 60;
    private int speed = 0;

    public override void _Ready()
    {
        parent = GetParent<MainCoordinates>();
        ScoreValueLabel = GetNode<Label>(nameof(ScoreValueLabel));
        LineCountValueLabel = GetNode<Label>(nameof(LineCountValueLabel));
        MultiplyValueLabel = GetNode<Label>(nameof(MultiplyValueLabel));
        SpeedValueLabel = GetNode<Label>(nameof(SpeedValueLabel));

        UpdatePanelValues();
    }

    public void LinesDestroyed() {
        multiply += parent.collectedLines / 2;
        score = parent.collectedLines * lineCost * multiply;
        speed = parent.frameTickRate % maxSpeed;
        
        UpdatePanelValues();
    }

    private void UpdatePanelValues() {
        ScoreValueLabel.Text = score.ToString();
        LineCountValueLabel.Text = parent.collectedLines.ToString();
        MultiplyValueLabel.Text = $"x{multiply}";
        SpeedValueLabel.Text = $"{speed}/{maxSpeed}";
    }
}
