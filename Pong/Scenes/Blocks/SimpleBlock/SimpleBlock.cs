using Godot;
using System;

public partial class SimpleBlock : StaticBody2D
{
	public IPlayer LastTouchedPlayer { get; set; }

	private GameManager _gameManager;

	[Export]
	public int HitsToDestroy { get; set; } = 3;

	public override void _Ready()
	{
		_gameManager = GetNode<GameManager>("/root/GameManager");
	}

	public void TouchedByBall(IPlayer player)
	{
		LastTouchedPlayer = player;
		HitsToDestroy--;
		if (HitsToDestroy == 0)
		{
			if (LastTouchedPlayer is not null)
			{
				_gameManager.AddModifier(this);
			}
			QueueFree();
		}
	}
}
