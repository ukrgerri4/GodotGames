using System;
using Godot;

public class StartupPanel : Panel
{
    private bool gameStarted = false;

    private bool startButtonVisible;
    private bool exitButtonVisible;

    private Panel optionsPanel;
    private PackedScene tetrisTemplate;
    private Tetris tetris;
    private Button startButton;
    private Button exitButton;

    public override void _Ready()
    {
        optionsPanel = GetNode<Panel>("OptionsPanel");
        startButton = GetNode<Button>("StartButton");
        exitButton = GetNode<Button>("ExitButton");
        tetrisTemplate = GD.Load<PackedScene>("res://Scenes/Games/Tetris.tscn");

        Visible = true;
        ShowOnTop = true;
        startButton.Visible = true;
        startButton.GrabFocus();
        exitButton.Visible = false;
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("ui_cancel"))
        {
            if (optionsPanel.Visible)
            {
                optionsPanel.Visible = false;
                return;
            }

            if (!gameStarted)
            {
                return;
            }

            if (GetTree().Paused)
            {
                Visible = false;
                GetTree().Paused = false;
            }
            else
            {
                Visible = true;
                GetTree().Paused = true;
            }
        }
    }

    private void ToggleStartExitButtons() {
        startButton.Visible = !startButton.Visible;
        exitButton.Visible = !exitButton.Visible;
    }

    private void StartGame() {
        gameStarted = true;
        tetris = tetrisTemplate.Instance<Tetris>();
        GetNode("/root/Main/Games").AddChild(tetris);
        ToggleStartExitButtons();
        Visible = false;
        tetris.ShowOnTop = false;
        tetris.Visible = true;
    }

    private void ExitGame() {
        gameStarted = false;
        tetris.Visible = false;
        tetris.QueueFree();
        ToggleStartExitButtons();
        Visible = true;
        GetTree().Paused = false;
    }

    private void OpenOptionsPanel() {
        optionsPanel.Visible = true;
    }
}
