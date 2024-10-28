using Mamavon.Funcs;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Mamavon.Useful
{
    public class SetPlayerInputAction : MonoBehaviour
    {
        [Serializable]
        private class PlayerInputData
        {
            [SerializeField] public PlayerInput playerInput;
            [SerializeField] public ReadOnlyArray<InputDevice> devices;
        }

        [SerializeField] private PlayerInputManagerSessionSystem m_playerInputManager;
        [SerializeField] private List<PlayerInputData> playerInputDataList = new List<PlayerInputData>();

        private void Awake()
        {
            m_playerInputManager.deviceChangedAction += ReconnectDeviceAction;
        }


        private void ReconnectDeviceAction(InputDevice device, InputDeviceChange deviceChange)
        {
            if (
                deviceChange == InputDeviceChange.Reconnected ||
                deviceChange == InputDeviceChange.Disconnected
                )

                device.deviceId.Debuglog(deviceChange.ToString());

        }

        private PlayerInputData FindPlayerInputDataForDevice(InputDevice device)
        {
            return playerInputDataList.Find(data => data.devices.Contains(device));
        }
    }
}
