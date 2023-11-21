using Godot;

public partial class Main : Node
{
	private EventsBus _eventsBus;

	private Node2D _userInterface;
	private Node2D _game;

	public override void _Ready()
	{
		_userInterface = GetNode<Node2D>("UserInterface");
		_game = GetNode<Node2D>("Game");

		_eventsBus = GetNode<EventsBus>("/root/EventsBus");
		_eventsBus.Ui.StartButtonPressed += OnStartButtonPressed;
		_eventsBus.Ui.QuitButtonPressed += OnQuitButtonPressed;
	}

	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustPressed(InputAction.GamePause))
		{
			GetTree().Paused = !GetTree().Paused;
		}

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
		_userInterface.Visible = false;
		_game.Visible = true;
	}

	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
