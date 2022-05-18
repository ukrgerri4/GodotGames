using Godot;
public class OptionsPanel : Panel
{
	private GameConfigurations gameConfigurations;
	private WindowDialog optionsDialog;

	private CheckBox musicOption;
	private CheckBox soundOption;


	public override void _Ready()
	{
		gameConfigurations = GetNode<GameConfigurations>("/root/GameConfigurations");
		optionsDialog = GetParent<WindowDialog>();
		musicOption = GetNode<CheckBox>("MusicOption");
		soundOption = GetNode<CheckBox>("SoundOption");

		musicOption.Pressed = gameConfigurations.MusicOn;
		soundOption.Pressed = gameConfigurations.SoundOn;
	}

	private void CloseOptionsPanel()
	{
		optionsDialog.Hide();
	}

	private void OnMusicCheckboxChanged(bool pressed)
	{
		gameConfigurations.MusicOn = pressed;
	}

	private void OnSoundCheckboxChanged(bool pressed)
	{
		gameConfigurations.SoundOn = pressed;
	}
}
