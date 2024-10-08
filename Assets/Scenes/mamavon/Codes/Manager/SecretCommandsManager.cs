using Mamavon.Funcs;
using System;
using UniRx;
using UnityEngine;

namespace Mamavon.Useful
{
    public class SecretCommandsManager : SingletonMonoBehaviour<SecretCommandsManager>
    {
        [SerializeField] private string myKey = string.Empty;
        [SerializeField] private bool isControlPressed = false;

        protected Action<string> _keyCodeCommandEvent;
        public static event Action<string> KeyCodeCommandEvent
        {
            add => Instance._keyCodeCommandEvent += value;
            remove => Instance._keyCodeCommandEvent -= value;
        }

        //Emptyで初期化できるんだ。
        private void Start()
        {
            InputObservableUtility.OnKeyDown(KeyCode.LeftControl).Subscribe(_ =>
            {
                isControlPressed = true;
                myKey = string.Empty; // Ctrlが押されたときにmyKeyをリセット
            });

            InputObservableUtility.OnKeyUp(KeyCode.LeftControl).Subscribe(_ =>
            {
                isControlPressed = false;
                if (!string.IsNullOrEmpty(myKey))
                {
                    _keyCodeCommandEvent?.Invoke(myKey);
                }
            });

            InputObservableUtility.OnAnyKeyDown()
                .Where(_ => isControlPressed)　//ctrlPressed == trueで
                .Where(key => key != KeyCode.LeftControl) //Leftコントロールじゃねえのなら
                .Subscribe(key =>
                {
                    myKey += key.ToString(); //ToString();にします。
                })
                .AddTo(this);
        }
    }
}