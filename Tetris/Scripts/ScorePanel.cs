using Godot;
using System;

public class ScorePanel : Panel
{
    private Tetris Parent;
    private Label ScoreValueLabel;
    private Label LineCountValueLabel;
    private Label MultiplyValueLabel;

    private int lineCost = 10;
    private int score = 0;
    private int multiply = 1;
    private int maxSpeed = 60;

    private int _speed = 0;
    public int Speed
    {
        get => _speed;
        set
        {
            _speed = value;
            SpeedChangedEvent?.Invoke();
        }
    }

    public delegate void SpeedChanged();
    public event SpeedChanged SpeedChangedEvent;

    public override void _Ready()
    {
        ScoreValueLabel = GetNode<Label>(nameof(ScoreValueLabel));
        LineCountValueLabel = GetNode<Label>(nameof(LineCountValueLabel));
        MultiplyValueLabel = GetNode<Label>(nameof(MultiplyValueLabel));

        Parent = GetNode<Tetris>("/root/Main/Games/Tetris");
        Parent.GameStartedEvent += Refresh;
        Parent.LinesDestroyedEvent += LinesDestroyed;

        var timer = new Timer();
        timer.Autostart = true;
        timer.WaitTime = 15;
        timer.Connect("timeout", this, nameof(UpdateMultiply));
        AddChild(timer);
    }

    private void Refresh()
    {
        score = 0;
        multiply = 1;
        Speed = 0;
        UpdatePanelValues();
    }

    public void LinesDestroyed(int linesDestroyed)
    {
        multiply += linesDestroyed / 2;
        score += linesDestroyed * lineCost * multiply;
        Speed = (maxSpeed - Parent.frameTickRate) % maxSpeed;
        UpdatePanelValues();
    }

    private void UpdatePanelValues()
    {
        ScoreValueLabel.Text = score.ToString();
        LineCountValueLabel.Text = Parent.collectedLines.ToString();
        MultiplyValueLabel.Text = $"x{multiply}";
    }

    private void UpdateMultiply()
    {
        if (multiply > 1)
        {
            multiply--;
        }
    }
}
