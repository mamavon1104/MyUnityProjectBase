using Mamavon.Funcs;
using Mamavon.Useful;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mamavon.Code
{
    public class SetPlayerDeviceType : MonoBehaviour
    {
        [SerializeField] PlayerInputManagerSessionSystem m_sessionSystem;

        [Flags]
        private enum DeviceType
        {
            KeyBoard_Mouse = 1 << 0,
            GamePad = 1 << 1,
        }
        [Header("認めるプレイヤーのデバイスタイプ"), SerializeField] DeviceType deviceType;


        private void OnEnable()
        {
            m_sessionSystem.joinAction += SetDeviceController;
        }

        private void OnDisable()
        {
            m_sessionSystem.joinAction -= SetDeviceController;
        }

        private void SetDeviceController(PlayerInput input)
        {
            foreach (var device in input.devices)
            {
                // デバイスタイプに応じた判定
                if (device is Gamepad && IsThisDevice(DeviceType.GamePad))
                {
                    // GamePadが選択されている場合は何もしない
                    "GamePadが選択されました。".Debuglog(TextColor.Blue);
                }
                else if ((device is Keyboard || device is Mouse) && IsThisDevice(DeviceType.KeyBoard_Mouse))
                {
                    // KeyboardまたはMouseが選択されている場合は何もしない
                    "KeyboardまたはMouseが選択されました。".Debuglog(TextColor.Blue);
                }
                else
                {
                    device.Debuglog("選択したタイプと違うデバイスです。");
                    Destroy(input.gameObject);
                }
            }
        }
        bool IsThisDevice(DeviceType device)
        {
            return (deviceType & device) == device;
        }
    }
}
