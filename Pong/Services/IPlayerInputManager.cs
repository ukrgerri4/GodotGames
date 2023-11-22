public interface IPlayerInputManager
{
	float Deadzone { get; set; }
	Device Device { get; set; }

	float GetLeftXStrength();
	float GetLeftYStrength();
	float GetRightXStrength();
	float GetRightYStrength();
	bool IsPadAccelerateButtonPressed();
	bool IsPauseButtonPressed();
	bool IsRocketLaunchButtonPressed();
	bool IsRotateButtonPressed();
}