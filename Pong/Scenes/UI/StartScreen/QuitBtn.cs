using Godot;
using System;

public partial class QuitBtn : Button
{
	private EventsBus _eventsBus;
	public override void _Ready()
	{
		_eventsBus = GetNode<EventsBus>("/root/EventsBus");
	}

	private void _on_button_up()
	{
		_eventsBus.Ui.NotifyQuitButtonPressed();
	}
}
