using Godot;
using System;

public interface IPlayer
{
	int Id { get; set; }
	string NickName { get; set; }
	Device Device { get; set; }
	MapPosition MapPosition { get; set; }

	Vector2 GlobalPosition { get; }
	float PanelWidth { get; }
	bool IsHorizontalPosition { get; }
	Vector2 ItemFallDirection { get; }

	void UpdateScore(int points);
	void InitBotInput();
}
