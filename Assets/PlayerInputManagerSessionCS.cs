using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Mamavon.Useful
{
    [RequireComponent(typeof(PlayerInputManager))]
    //checked
    public class PlayerInputManagerSessionCS : MonoBehaviour
    {
        [SerializeField] PlayerInputManager inputManager;
        //[Header("�Q�����ɓ��͂���L�[�Ƃ��{�^���Ƃ�"), SerializeField] InputActionReference _joinKeyAction;

        [Header("�Q�����̃C�x���g"), SerializeField] UnityEvent m_joinEvent;
        [Header("�ޏo���̃C�x���g"), SerializeField] UnityEvent m_leftEvent;
        public Action<PlayerInput> joinAction { get; set; }
        public Action<PlayerInput> leftAction { get; set; }

        private void Awake()
        {
            //if (_joinKeyAction == null)
            //    "InputActionReference��null�ɂȂ��Ă��܂��B".DebuglogError();

            if (inputManager == null) inputManager = GetComponent<PlayerInputManager>();
        }

        private void OnEnable()
        {
            //_joinKeyAction.action.Debuglog();
            //_joinKeyAction.action.performed += JoinActionPerformed;
            //_joinKeyAction.action.Enable();

            inputManager.onPlayerJoined += InvokeJoinEvent;
            inputManager.onPlayerLeft += InvokeLeftAction;
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

            //foreach (InputDevice device in input.devices)
            //{
            //    if (!nowInputDevices.Contains(device))
            //        continue;

            //    input.Debuglog($"�v���C���[{input.playerIndex} ���ޏo���܂����A�f�o�C�X : {device}");
            //    nowInputDevices.Remove(device);
            //}
        }

        //private void DebugList()
        //{
        //    string a = "";
        //    foreach (var item in nowInputDevices)
        //    {
        //        a += item.ToString();
        //    }
        //    a.Debuglog(TextColor.DarkRed);
        //}
    }
}