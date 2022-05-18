using Godot;

public class GameConfigurations: Node
{
	public delegate void VolumeAdjustmentsChanged(bool enable);
	
	public event VolumeAdjustmentsChanged SoundAdjustmentsChangedEvent;
	public bool _soundOn = false;
	public bool SoundOn
	{
		get => _soundOn;
		set {
			_soundOn = value;
			SoundAdjustmentsChangedEvent?.Invoke(_soundOn);
		}
	}

	public event VolumeAdjustmentsChanged MusicAdjustmentsChangedEvent;
	public bool _musicOn = false;
	public bool MusicOn
	{
		get => _musicOn;
		set {
			_musicOn = value;
			MusicAdjustmentsChangedEvent?.Invoke(_musicOn);
		}
	}
}