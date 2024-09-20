using Mamavon.Funcs;
using UnityEngine;
using UnityEngine.Events;

namespace Mamavon.Code
{
    public class InvokeEvents : MonoBehaviour
    {
        /// <summary>
        /// 設定されてるunityEvents;
        /// </summary>
        [Header("ボタンイベント"), SerializeField] UnityEvent events;
        [Header("デバッグするかどうか"), SerializeField] bool isDebug = false;

        [ContextMenu("Inspectorから実行")]
        public void InvokeMyEvents()
        {
            if (isDebug)
                events?.Debuglog().Invoke();
            else
                events?.Invoke();
        }
    }
}