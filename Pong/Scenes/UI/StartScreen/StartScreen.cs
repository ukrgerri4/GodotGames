using Godot;
using System;

public partial class StartScreen : Panel
{
	[Export]
	public Button StartBtn;
	public override void _Ready()
	{
		VisibilityChanged += () => StartBtn.GrabFocus();
	}
}
