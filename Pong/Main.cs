using Godot;

public partial class Main : Node
{
	private EventsBus _eventsBus;

	private Node2D _userInterface;
	private Game _game;

	public override void _Ready()
	{
		_eventsBus = GetNode<EventsBus>("/root/EventsBus");
		_userInterface = GetNode<Node2D>("UserInterface");
		_game = GetNode<Game>("Game");

		_eventsBus.Ui.StartButtonPressed += OnStartButtonPressed;
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
			GetTree().Paused = !GetTree().Paused;
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
		_userInterface.Visible = false;
		_game.Visible = true;
	}

	private void OnMultPlayerButtonPressed()
	{
	}

	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
