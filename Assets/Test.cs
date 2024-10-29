using UnityEngine;
using UnityEngine.InputSystem;

public class DeviceIdExample : MonoBehaviour
{
    private void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added || change == InputDeviceChange.Reconnected)
        {
            int deviceId = device.deviceId;
            Debug.Log($"Device connected: {change}, ID: {deviceId}");
        }
        else if (change == InputDeviceChange.Removed || change == InputDeviceChange.Disconnected)
        {
            int deviceId = device.deviceId;
            Debug.Log($"Device disconnected: {change}, ID: {deviceId}");
        }
    }

    // 現在接続されているすべてのデバイスのIDを表示
    private void LogAllDeviceIds()
    {
        foreach (var device in InputSystem.devices)
        {
            Debug.Log($"Device: {device.name}, ID: {device.deviceId}");
        }
    }
}
