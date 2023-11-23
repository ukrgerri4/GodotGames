using Godot;
using System;

public partial class PauseScreen : Panel
{
	[Export]
	public Button ContinueBtn;
	public override void _Ready()
	{
		VisibilityChanged += OnVisibilityChanged;
	}

	private void OnVisibilityChanged()
	{
		if (Visible)
		{
			ContinueBtn.GrabFocus();
		}
	}
}
