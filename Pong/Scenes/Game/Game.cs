using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Game : Node2D
{
	private RotationArea _rotationArea;
	private Node2D _map;
	private PropertyTweener _tweener;
	private bool _canTween = true;

	public override void _Ready()
	{
		_map = GetNode<Node2D>("Map");
		_rotationArea = GetNode<RotationArea>("Map/RotationArea");

		GetNode<Node>("Balls").GetChild<Ball>(0).Reset();
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed(InputAction.MapRotate) && _rotationArea.RotationAllowed)
		{
			if (_tweener is null || _canTween)
			{
				_canTween = false;
				GetTree().Paused = true;
				_tweener = CreateTween().TweenProperty(_map, "rotation_degrees", _map.RotationDegrees + 90, 1f);
				_tweener.Finished += () =>
				{
					_canTween = true;
					GetTree().Paused = false;
				};
			}
		}
	}
}
