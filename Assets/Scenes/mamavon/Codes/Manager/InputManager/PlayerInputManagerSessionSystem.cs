using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Mamavon.Useful
{
    [RequireComponent(typeof(PlayerInputManager))]
    //checked
    public class PlayerInputManagerSessionSystem : MonoBehaviour
    {
        [SerializeField] PlayerInputManager inputManager;
        //[Header("�Q�����ɓ��͂���L�[�Ƃ��{�^���Ƃ�"), SerializeField] InputActionReference _joinKeyAction;

        [Header("�Q�����̃C�x���g"), SerializeField] UnityEvent m_joinEvent;
        [Header("�ޏo���̃C�x���g"), SerializeField] UnityEvent m_leftEvent;
        [Header("�R���g���[���[�ڑ����̃C�x���g"), SerializeField] UnityEvent m_deviceChangedEvent;

        /// <summary>
        /// �v���C���[���Q���������ɔ��΂����Action<br/>
        /// <br/>
        /// ���� : PlayerInput
        /// </summary>
        public Action<PlayerInput> joinAction { get; set; }

        /// <summary>
        /// �v���C���[���ޏo�������ɔ��΂����Action<br/>
        /// <br/>
        /// ���� : PlayerInput
        /// </summary>
        public Action<PlayerInput> leftAction { get; set; }

        /// <summary>
        /// �R���g���[���[���ڑ��A�ؒf���ꂽ���ɔ��΂����Action<br/>
        /// <br/>
        /// ���� : InputDevice, InputDeviceChange
        /// </summary>
        public Action<InputDevice, InputDeviceChange> deviceChangedAction { get; set; }

        private void Awake()
        {
            if (inputManager == null) inputManager = GetComponent<PlayerInputManager>();
        }

        private void OnEnable()
        {
            inputManager.onPlayerJoined += InvokeJoinEvent;
            inputManager.onPlayerLeft += InvokeLeftAction;
            InputSystem.onDeviceChange += InvokeDeviceAction;
        }

        /// <summary>
        /// �������ɔ��΂����C�x���g�A player�̐e�����̃I�u�W�F�N�g�ɕς�����
        /// �R�[�h��Őݒ肳�ꂽ�C�x���g��UnityEvent�A�ǂ���Ƃ����΂���
        /// </summary>
        private void InvokeJoinEvent(PlayerInput input)
        {
            joinAction?.Invoke(input);
            m_joinEvent?.Invoke();
        }
        /// <summary>
        /// �ޏo���ɔ��΂����C�x���g�A
        /// �R�[�h��Őݒ肳�ꂽ�C�x���g��UnityEvent�A�ǂ���Ƃ����΂���
        /// </summary>
        private void InvokeLeftAction(PlayerInput input)
        {
            leftAction?.Invoke(input);
            m_leftEvent?.Invoke();
        }
        /// <summary>
        /// �R���g���[���[���ڑ����ꂽ�Ƃ��ɔ��΂���C�x���g <br/>
        /// �R�[�h��Őݒ肳�ꂽ�C�x���g��UnityEvent�A�ǂ���Ƃ����΂���
        /// </summary>
        private void InvokeDeviceAction(InputDevice device, InputDeviceChange change)
        {
            //change.Debuglog($"�f�o�C�X���ύX����܂���{device} �f�o�C�X��{DeviceExtensions.GetDevicesState(change)}�ڍׂ���{change}", TextColor.Orangered);

            deviceChangedAction?.Invoke(device, change);
            m_deviceChangedEvent?.Invoke();
        }
    }
}