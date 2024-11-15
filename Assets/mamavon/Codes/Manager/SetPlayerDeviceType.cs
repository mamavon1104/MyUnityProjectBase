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
        [Header("�F�߂�v���C���[�̃f�o�C�X�^�C�v"), SerializeField] DeviceType deviceType;


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
                // �f�o�C�X�^�C�v�ɉ���������
                if (device is Gamepad && IsThisDevice(DeviceType.GamePad))
                {
                    // GamePad���I������Ă���ꍇ�͉������Ȃ�
                    "GamePad���I������܂����B".Debuglog(TextColor.Blue);
                }
                else if ((device is Keyboard || device is Mouse) && IsThisDevice(DeviceType.KeyBoard_Mouse))
                {
                    // Keyboard�܂���Mouse���I������Ă���ꍇ�͉������Ȃ�
                    "Keyboard�܂���Mouse���I������܂����B".Debuglog(TextColor.Blue);
                }
                else
                {
                    device.Debuglog("�I�������^�C�v�ƈႤ�f�o�C�X�ł��B");
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
