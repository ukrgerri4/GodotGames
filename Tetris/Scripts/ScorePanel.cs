using Godot;
using System;

public class ScorePanel : Panel
{
    private Main parent;
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
        ScoreValueLabel = GetNode<Label>(nameof(ScoreValueLabel));
        LineCountValueLabel = GetNode<Label>(nameof(LineCountValueLabel));
        MultiplyValueLabel = GetNode<Label>(nameof(MultiplyValueLabel));
        SpeedValueLabel = GetNode<Label>(nameof(SpeedValueLabel));

        parent = GetNode<Main>("/root/Main");
        parent.GameStartedEvent += Refresh;
        parent.LinesDestroyedEvent += LinesDestroyed;
    }

    private void Refresh() {
        score = 0;
        multiply = 1;
        speed = 0;
        UpdatePanelValues();
    }

    public void LinesDestroyed(int linesDestroyed) {
        // TODO: make right score count
        multiply += linesDestroyed / 2;
        score = parent.collectedLines * lineCost * multiply;
        speed = (maxSpeed - parent.frameTickRate) % maxSpeed;
        UpdatePanelValues();
    }

    private void UpdatePanelValues() {
        ScoreValueLabel.Text = score.ToString();
        LineCountValueLabel.Text = parent.collectedLines.ToString();
        MultiplyValueLabel.Text = $"x{multiply}";
        SpeedValueLabel.Text = $"{speed}/{maxSpeed}";
    }
}
