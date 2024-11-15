using Mamavon.Funcs;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

/*
 * 私のほぼ師匠が制作されていたものを参考にInputをIObservableのKeyCodeを待って
 * 入力終了時にActionにkeycodeを渡し、Actionで実行する。
*/
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

        #region プレイヤーの入力テーブル
        protected static readonly Dictionary<KeyCode, char> keyCode_vs_char = new()
        {
            {KeyCode.Space,' '},
            {KeyCode.A, 'a'},
            {KeyCode.B, 'b'},
            {KeyCode.C, 'c'},
            {KeyCode.D, 'd'},
            {KeyCode.E, 'e'},
            {KeyCode.F, 'f'},
            {KeyCode.G, 'g'},
            {KeyCode.H, 'h'},
            {KeyCode.I, 'i'},
            {KeyCode.J, 'j'},
            {KeyCode.K, 'k'},
            {KeyCode.L, 'l'},
            {KeyCode.M, 'm'},
            {KeyCode.N, 'n'},
            {KeyCode.O, 'o'},
            {KeyCode.P, 'p'},
            {KeyCode.Q, 'q'},
            {KeyCode.R, 'r'},
            {KeyCode.S, 's'},
            {KeyCode.T, 't'},
            {KeyCode.U, 'u'},
            {KeyCode.V, 'v'},
            {KeyCode.W, 'w'},
            {KeyCode.X, 'x'},
            {KeyCode.Y, 'y'},
            {KeyCode.Z, 'z'},

            {KeyCode.Alpha0, '0' },
            {KeyCode.Alpha1, '1' },
            {KeyCode.Alpha2, '2' },
            {KeyCode.Alpha3, '3' },
            {KeyCode.Alpha4, '4' },
            {KeyCode.Alpha5, '5' },
            {KeyCode.Alpha6, '6' },
            {KeyCode.Alpha7, '7' },
            {KeyCode.Alpha8, '8' },
            {KeyCode.Alpha9, '9' },
        };
        #endregion

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
                    //Dicからcharを取得して加算
                    try { myKey += keyCode_vs_char[key]; }
                    catch { }
                })
                .AddTo(this);
        }
    }
}