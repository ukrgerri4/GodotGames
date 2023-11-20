using Godot;
using System;

public class SpeedProgress : ProgressBar
{
    private ScorePanel Parent;
    public override void _Ready()
    {
				MaxValue = 55;
				Step = 1;

        Parent = GetParent<ScorePanel>();
        Parent.SpeedChangedEvent += OnSpeedChanged;
    }

    private void OnSpeedChanged()
    {
        Value = MaxValue - Parent.Speed;
    // $ProgressBar.value = 10
    // var r = range_lerp($ProgressBar.value, 10, 100, 1, 0)
    // var g = range_lerp($ProgressBar.value, 10, 100, 0, 1)
    // var styleBox = $ProgressBar.get("custom_styles/fg")
    // styleBox.bg_color = Color(r, g, 0)
    }

    public float RangeLerp(float value, float istart, float istop, float ostart, float ostop)
    {
        return ostart + (ostop - ostart) * value / (istop - istart);
    }
}
