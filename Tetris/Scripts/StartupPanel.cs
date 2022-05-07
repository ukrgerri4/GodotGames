using System;
using Godot;

public class StartupPanel : Panel
{
    private bool gameStarted = false;

    private bool startButtonVisible;
    private bool exitButtonVisible;

    private Panel optionsPanel;
    private Button startButton;
    private Button exitButton;

    public override void _Ready()
    {
        Visible = true;
        startButtonVisible = true;
        exitButtonVisible = false;

        optionsPanel = GetNode<Panel>("OptionsPanel");
        startButton = GetNode<Button>("StartButton");
        exitButton = GetNode<Button>("ExitButton");

        UpdateButtonVisibility();
    }

    private void UpdateButtonVisibility()
    {
       startButton.Visible = startButtonVisible;
       exitButton.Visible = exitButtonVisible;
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

    private void StartGame() {
        gameStarted = true;
        // load scene
        UpdateButtonVisibility();
        Visible = false;
    }

    private void ExitGame() {
        gameStarted = false;
        // free scene
        UpdateButtonVisibility();
        Visible = true;
    }

    private void OpenOptionsPanel() {
        optionsPanel.Visible = true;
    }
}
