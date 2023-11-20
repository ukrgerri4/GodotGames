using System;
using Godot;

public class StartupPanel : Panel
{
	private bool gameStarted = false;

	private bool startButtonVisible;
	private bool exitButtonVisible;

	private WindowDialog optionsDialog;
	private PackedScene tetrisTemplate;
	private Tetris tetris;
	private Button startButton;
	private Button exitButton;
	private Button continueButton;

	public override void _Ready()
	{
		optionsDialog = GetNode<WindowDialog>("OptionsDialog");
		startButton = GetNode<Button>("CenterContainer/VBoxContainer/StartButton");
		exitButton = GetNode<Button>("CenterContainer/VBoxContainer/ExitButton");
		continueButton = GetNode<Button>("CenterContainer/VBoxContainer/ContinueButton");
		tetrisTemplate = GD.Load<PackedScene>("res://Scenes/Games/Tetris.tscn");

		Visible = true;
		ShowOnTop = true;
		startButton.Visible = true;
		startButton.GrabFocus();
		exitButton.Visible = false;
		continueButton.Visible = false;
	}

	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustPressed("ui_cancel"))
		{
			if (!gameStarted)
			{
				return;
			}

			if (GetTree().Paused)
			{
				GetTree().Paused = false;
				Visible = false;
			}
			else
			{
				GetTree().Paused = true;
				Visible = true;
			}
		}
	}

	private void ToggleStartExitButtons()
	{
		startButton.Visible = !startButton.Visible;
		exitButton.Visible = !exitButton.Visible;
		continueButton.Visible = !continueButton.Visible;
	}

	private void StartGame()
	{
		gameStarted = true;
		tetris = tetrisTemplate.Instance<Tetris>();
		GetNode("/root/Main/Games").AddChild(tetris);
		ToggleStartExitButtons();
		Visible = false;
		tetris.ShowOnTop = false;
		tetris.Visible = true;
	}

	private void ExitGame()
	{
		gameStarted = false;
		tetris.Visible = false;
		tetris.QueueFree();
		ToggleStartExitButtons();
		GetTree().Paused = false;
		OnVisibilityChanged();
	}

	private void ContinueGame()
	{
		GetTree().Paused = false;
		Visible = false;
	}

	private void OpenOptionsPanel()
	{
		optionsDialog.Popup_();
	}

	private void OnVisibilityChanged()
	{
		if (Visible)
		{
			if (GetTree().Paused)
			{
				continueButton.GrabFocus();
			}
			else
			{
				startButton.GrabFocus();
			}
		}
	}
}
