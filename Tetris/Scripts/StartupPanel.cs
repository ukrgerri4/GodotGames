using Godot;
using System.Net.Http;

public class StartupPanel : Panel
{
    private bool startButtonVisible;
    private bool exitButtonVisible;

    public override void _Ready()
    {
        startButtonVisible = true;
        exitButtonVisible = false;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
