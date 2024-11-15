using System;
using UnityEngine;

namespace Mamavon.Useful
{
    public class DeviceTypeManager : MonoBehaviour
    {
        [Flags]
        private enum DeviceType
        {
            KeyBoard_Mouse = 1 << 0,
            GamePad = 1 << 1,
        }
        [Header("認めるプレイヤーのデバイスタイプ"), SerializeField] DeviceType deviceType;
        bool IsThisDevice(DeviceType device)
        {
            return (deviceType & device) == device;
        }
    }
}