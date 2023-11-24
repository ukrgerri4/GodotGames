using Godot;
using System;

public partial class ContinueBtn : Button
{
	private EventsBus _eventsBus;
	public override void _Ready()
	{
		_eventsBus = GetNode<EventsBus>("/root/EventsBus");
		ButtonUp += () => _eventsBus.Ui.NotifyContinueButtonPressed();
	}
}
