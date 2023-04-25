using Godot;

public partial class Configuration : Node
{
    public override void _Ready()
    {
        Input.Singleton.Connect("joy_connection_changed", new Callable(this, nameof(OnJoyConnectionChanged)));
    }

    public override void _Input(InputEvent @event)
    {
        // if (Input.IsActionJustPressed("exit"))
        // {
        // }

        // if (Input.IsActionJustPressed("change_mouse_caption"))
        // {
        // }

        // if (Input.IsActionJustPressed("change_mouse_mode"))
        // {
        // }

        // if (Input.IsActionJustPressed("full_screen"))
        // {
        // }
    }

    private void OnJoyConnectionChanged(int device, bool connected)
    {
        // generate input map for connected device
        // or create all input maps previously and use it
        // GD.Print(device, connected);
        // var key1 = new InputEventJoypadMotion();
        // var key = new InputEventKey();
        // key.PhysicalKeycode = Key.A;
        // InputMap.ActionAddEvent("dsd", key);
    }
}