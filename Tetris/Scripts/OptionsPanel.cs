using Godot;
public class OptionsPanel : Panel
{
	private GameConfigurations gameConfigurations;
	private WindowDialog optionsDialog;

	private CheckBox soundOption;
	private Button backButton;

	public override void _Ready()
	{
		gameConfigurations = GetNode<GameConfigurations>("/root/GameConfigurations");
		optionsDialog = GetParent<WindowDialog>();
		soundOption = GetNode<CheckBox>("SoundOption");
		backButton = GetNode<Button>("BackButton");

		soundOption.Pressed = gameConfigurations.SoundOn;
		backButton.GrabFocus();
	}

	private void CloseOptionsPanel()
	{
		optionsDialog.Hide();
	}

	private void OnSoundCheckboxChanged(bool pressed)
	{
		gameConfigurations.SoundOn = pressed;
	}
}
