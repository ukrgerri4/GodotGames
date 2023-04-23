using System.Linq;
using Godot;

public static class InputAction
{
	// public static readonly object x = new {
	// 	ActionName = new StringName("move_left"),
	// 	Key = new InputEventKey { PhysicalKeycode = Key.Shift },
	// 	Joy = new InputEventJoypadButton{ ButtonIndex = JoyButton.RightStick },
	// };

	public static readonly StringName MoveLeft = new StringName("move_left");
	public static readonly StringName MoveRight = new StringName("move_right");
	public static readonly StringName MoveUp = new StringName("move_up");
	public static readonly StringName MoveDown = new StringName("move_down");

	public static readonly StringName Accelerate = new StringName("accelerate");
	public static readonly StringName Pause = new StringName("pause");
	public static readonly StringName MapRotate = new StringName("map_rotate");

	public static readonly StringName RocketLaunch = new StringName("rocket_launch");
	public static readonly StringName RocketLeft = new StringName("rocket_left");
	public static readonly StringName RocketRight = new StringName("rocket_right");
	public static readonly StringName RocketUp = new StringName("rocket_up");
	public static readonly StringName RocketDown = new StringName("rocket_down");

	public static StringName[] Actions { get; set; } = new StringName[0];

	static InputAction()
	{
		var objType = typeof(InputAction);
		Actions = objType.GetFields()
			.Where(x => x.IsInitOnly)
			.Select(x => (StringName)x.GetValue(objType))
			.ToArray();
	}
}