using Godot;
using System;

public partial class UserInterface : Node2D
{
	[Export]
	public StartScreen StartScreen;

	[Export]
	public PauseScreen PauseScreen;

	public override void _Ready()
	{
		StartScreen.Show();
		PauseScreen.Hide();
	}

	public void HideChildren()
	{
		StartScreen.Hide();
		PauseScreen.Hide();
	}
}
