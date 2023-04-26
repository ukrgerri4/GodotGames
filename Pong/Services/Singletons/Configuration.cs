using System.Collections.Generic;
using Godot;

public partial class Configuration : Node
{
    public List<ConnectedDevice> ConnectedDevices = new List<ConnectedDevice>();
    public override void _Ready()
    {
        InitConnectedDevices();
    }

    private void InitConnectedDevices()
    {
        var deviceIds = Input.GetConnectedJoypads();
        if (deviceIds is not null)
        {
            foreach (var deviceId in deviceIds)
            {
                var newDevice = GetConnectedDevice(deviceId);
                ConnectedDevices.Add(newDevice);
                GD.Print($"Initialized DeviceId: {newDevice.DeviceId}, Guid: {newDevice.Guid}, Name: {newDevice.Name}");
            }
        }

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
            Name = Input.GetJoyName(deviceId)
        };
    }
}