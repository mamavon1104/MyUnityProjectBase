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
        //[Header("参加時に入力するキーとかボタンとか"), SerializeField] InputActionReference _joinKeyAction;

        [Header("参加時のイベント"), SerializeField] UnityEvent m_joinEvent;
        [Header("退出時のイベント"), SerializeField] UnityEvent m_leftEvent;
        [Header("コントローラー接続時のイベント"), SerializeField] UnityEvent m_deviceChangedEvent;

        /// <summary>
        /// プレイヤーが参加した時に発火されるAction<br/>
        /// <br/>
        /// 引数 : PlayerInput
        /// </summary>
        public Action<PlayerInput> joinAction { get; set; }

        /// <summary>
        /// プレイヤーが退出した時に発火されるAction<br/>
        /// <br/>
        /// 引数 : PlayerInput
        /// </summary>
        public Action<PlayerInput> leftAction { get; set; }

        /// <summary>
        /// コントローラーが接続、切断された時に発火されるAction<br/>
        /// <br/>
        /// 引数 : InputDevice, InputDeviceChange
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
        }
        /// <summary>
        /// コントローラーが接続されたときに発火するイベント <br/>
        /// コード上で設定されたイベントとUnityEvent、どちらとも発火する
        /// </summary>
        private void InvokeDeviceAction(InputDevice device, InputDeviceChange change)
        {
            //change.Debuglog($"デバイスが変更されました{device} デバイスは{DeviceExtensions.GetDevicesState(change)}詳細だと{change}", TextColor.Orangered);

            deviceChangedAction?.Invoke(device, change);
            m_deviceChangedEvent?.Invoke();
        }
    }
}