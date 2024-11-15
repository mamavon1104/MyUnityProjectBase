using Mamavon.Funcs;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Mamavon.Code
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public class ButtonMamavon : MonoBehaviour
    {
        /// <summary>
        /// 設定されてるunityEvents;
        /// </summary>
        [Header("ボタンイベント"), SerializeField] UnityEvent events;
        [Header("ボタン、自動で入れられる"), SerializeField] Button button;
        [Header("デバッグするかどうか"), SerializeField] bool isDebug = false;
        private void Start()
        {
            if (button == null)
                button = GetComponent<Button>();

            button.OnClickThrottle().Subscribe(_ =>
            {
                if (isDebug)
                    events?.Debuglog().Invoke();
                else
                    events?.Invoke();
            }).AddTo(button);
        }
    }
}