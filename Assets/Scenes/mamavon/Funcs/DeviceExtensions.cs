using System;
using UnityEngine.InputSystem;
namespace Mamavon.Funcs
{
    public static class DeviceExtensions
    {
        public enum DeviceState
        {
            Connect,
            Disconnect,
            Both,
        }
        /// <summary>
        /// InputDeviceChange�ɑ΂��āA<br>
        /// --���̃R���g���[���[���ڑ�����<br>
        /// --���̃R���g���[���[���ؒf����<br>
        /// --���̃R���g���[���[���ڑ��Ɛڑ��ǂ��������<br>
        /// �����Ԃ��܂�
        /// </summary>
        /// <returns>
        /// Connect: �ڑ�
        /// Disconnect: �ؒf
        /// Both: ����
        /// </returns>
        public static DeviceState GetDevicesState(InputDeviceChange change)
        {
            return change switch
            {
                InputDeviceChange.Added or                              // Added: �V�����f�o�C�X���ǉ����ꂽ
                InputDeviceChange.Reconnected or                        // Reconnected: �f�o�C�X���Đڑ����ꂽ
                InputDeviceChange.Enabled or                            // Enabled: �f�o�C�X���L�������ꂽ
                InputDeviceChange.SoftReset => DeviceState.Connect,   // SoftReset: �f�o�C�X���\�t�g���Z�b�g���ꂽ(�o�b�N�O���E���h����̕��A�Ƃ�)

                InputDeviceChange.Removed or                            // Removed: �f�o�C�X���폜���ꂽ
                InputDeviceChange.Disconnected or                       // Disconnected: �f�o�C�X���ؒf���ꂽ
                InputDeviceChange.Disabled or                           // Disabled: �f�o�C�X�����������ꂽ
                InputDeviceChange.HardReset => DeviceState.Disconnect,  // HardReset: �f�o�C�X���n�[�h���Z�b�g���ꂽ

                InputDeviceChange.ConfigurationChanged or               // ConfigurationChanged: �ݒ肪�ύX���ꂽ(switch�݂�����)
                InputDeviceChange.UsageChanged => DeviceState.Both,     // UsageChanged: �g�p�󋵂��ύX���ꂽ(VR�Ƃ���)

                _ => throw new ArgumentException($"{change}�������ǂ�") // �܂��\������uDestory�v�AInputSystem���A�b�v�f�[�g�����炨�킿(^-^)-��
            };
        }
    }
}