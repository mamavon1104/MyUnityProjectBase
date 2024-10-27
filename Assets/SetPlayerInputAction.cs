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
            m_playerInputManager.joinAction += AddPlayerInputAction;
            m_playerInputManager.leftAction += RemovePlayerInputAction;
            m_playerInputManager.deviceChangedAction += ReconnectDeviceAction;
        }

        private void AddPlayerInputAction(PlayerInput playerInput)
        {
            Debug.Log("追加!");
            if (playerInputDataList.Exists(data => data.playerInput == playerInput))
                return;

            PlayerInputData newData = new PlayerInputData
            {
                playerInput = playerInput,
                devices = playerInput.devices
            };
            playerInputDataList.Add(newData);
            playerInput.Debuglog("プレイヤーが参加", TextColor.Cyan);
        }

        private void RemovePlayerInputAction(PlayerInput playerInput)
        {
            int index = playerInputDataList.FindIndex(data => data.playerInput == playerInput);
            if (index != -1)
            {
                playerInputDataList.RemoveAt(index);
                playerInput.Debuglog("プレイヤーが退出", TextColor.Cyan);
            }
        }

        private void ReconnectDeviceAction(InputDevice device, InputDeviceChange deviceChange)
        {
            if (deviceChange != InputDeviceChange.Reconnected)
                return;

            var playerInputData = FindPlayerInputDataForDevice(device);
            if (playerInputData != null)
            {
                playerInputData.playerInput.SwitchCurrentControlScheme(device);
                Debug.Log($"リコネクト {device} to Player {playerInputData.playerInput}");
            }
        }

        private PlayerInputData FindPlayerInputDataForDevice(InputDevice device)
        {
            return playerInputDataList.Find(data => data.devices.Contains(device));
        }
    }
}
