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
        //[Header("参加時に入力するキーとかボタンとか"), SerializeField] InputActionReference _joinKeyAction;

        [Header("参加時のイベント"), SerializeField] UnityEvent m_joinEvent;
        [Header("退出時のイベント"), SerializeField] UnityEvent m_leftEvent;
        public Action<PlayerInput> joinAction { get; set; }
        public Action<PlayerInput> leftAction { get; set; }

        private void Awake()
        {
            //if (_joinKeyAction == null)
            //    "InputActionReferenceがnullになっています。".DebuglogError();

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
        /// 入室時に発火されるイベント、 playerの親をこのオブジェクトに変えた後
        /// コード上で設定されたイベントとUnityEvent、どちらとも発火する
        /// </summary>
        private void InvokeJoinEvent(PlayerInput input)
        {
            joinAction?.Invoke(input);
            m_joinEvent?.Invoke();
        }
        /// <summary>
        /// 退出時に発火されるイベント、
        /// コード上で設定されたイベントとUnityEvent、どちらとも発火する
        /// </summary>
        private void InvokeLeftAction(PlayerInput input)
        {
            leftAction?.Invoke(input);
            m_leftEvent?.Invoke();

            //foreach (InputDevice device in input.devices)
            //{
            //    if (!nowInputDevices.Contains(device))
            //        continue;

            //    input.Debuglog($"プレイヤー{input.playerIndex} が退出しました、デバイス : {device}");
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