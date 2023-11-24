using System;
using Godot;

public partial class Main : Node
{
	private EventsBus _eventsBus;
	private GameManager _gameManager;
	private UserInterface _userInterface;
	private Game _game;

	private ApplicationState _state = ApplicationState.MainMenu;

	public override void _Ready()
	{
		_eventsBus = GetNode<EventsBus>("/root/EventsBus");
		_gameManager = GetNode<GameManager>("/root/GameManager");
		_userInterface = GetNode<UserInterface>("UserInterface");
		_game = GetNode<Game>("Game");

		_eventsBus.Ui.StartButtonPressed += OnStartButtonPressed;
		_eventsBus.Ui.ContinueButtonPressed += OnContinueButtonPressed;
		_eventsBus.Ui.QuitButtonPressed += OnQuitButtonPressed;
	}

	public override void _Input(InputEvent @event)
	{
		HandlePauseInput();
		HandleWindowModeInput();
	}

	private void HandlePauseInput()
	{
		if (Input.IsActionJustPressed(InputAction.GamePause))
		{
			switch (_state)
			{
				case ApplicationState.Game:
					PauseGame();
					return;
				case ApplicationState.Pause:
					UnPauseGame();
					return;
				case ApplicationState.MainMenu:
				default:
					return;
			}
		}
	}

	private void HandleWindowModeInput()
	{
		if (Input.IsActionJustPressed(InputAction.GameFullScreen))
		{
			if (DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Fullscreen)
			{
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
			}
			else
			{
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
			}
		}
	}

	private void OnStartButtonPressed()
	{
		// TODO: setup game
		_gameManager.InitPlayerInputDevices();
		_userInterface.HideChildren();
		_game.Show();
		_state = ApplicationState.Game;
	}

	private void OnContinueButtonPressed()
	{
		UnPauseGame();
	}

	private void OnMultPlayerButtonPressed()
	{
	}

	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}

	private void PauseGame()
	{
		_state = ApplicationState.Pause;
		GetTree().Paused = true;
		_userInterface.PauseScreen.Show();
	}

	private void UnPauseGame()
	{
		_state = ApplicationState.Game;
		_userInterface.PauseScreen.Hide();
		GetTree().Paused = false;
	}
}
