using System.Collections.Generic;
using Godot;

public partial class Configuration : Node
{
	public List<ConnectedDevice> ConnectedDevices = new List<ConnectedDevice>();

	public BallSettings Ball { get; set; }
	public PlayerSettings Player { get; set; }

	public RandomNumberGenerator Rng { get; set; } = new RandomNumberGenerator();

	public override void _Ready()
	{
		Ball = new BallSettings
		{
			DefaultSpeed = 350.0f,
			DefaultRadius = 6.0f
		};

		Player = new PlayerSettings
		{
			DefaultSpeed = 400.0f,
			DefaultWidth = 80.0f,
			MinWidth = 20.0f,
			MaxWidth = 160.0f,
			StartScore = 0
		};

		InitConnectedDevices();
	}

	private void InitConnectedDevices()
	{
		Input.Singleton.Connect("joy_connection_changed", new Callable(this, nameof(OnJoyConnectionChanged)));
	}

	private void OnJoyConnectionChanged(int deviceId, bool connected)
	{
		GD.Print($"Event: JoyConnectionChanged. Device: {deviceId}, Connected: {connected}");
		if (connected)
		{
			var newDevice = GetConnectedDevice(deviceId);
			ConnectedDevices.Add(newDevice);
			GD.Print($"Added DeviceId: {newDevice.DeviceId}, Guid: {newDevice.Guid}, Name: {newDevice.Name}");
		}
		else
		{
			var removedCount = ConnectedDevices.RemoveAll(x => x.DeviceId == deviceId);
			GD.Print($"Removed DeviceId: {deviceId}. Removed count: {removedCount}");
		}
	}

	private ConnectedDevice GetConnectedDevice(int deviceId)
	{
		return new ConnectedDevice
		{
			DeviceId = deviceId,
			Guid = Input.GetJoyGuid(deviceId),
			Name = Input.GetJoyName(deviceId),
			ConnectedToPlayer = false
		};
	}
}