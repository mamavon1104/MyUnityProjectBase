using System;
using UniRx;
using UnityEngine.UI;
namespace Mamavon.Funcs
{
    public static class UIExtensions
    {
        /// <summary>
        /// ボタンの拡張メソッドで、連打防止を手っ取り早くしちゃいます。
        /// </summary>
        /// <param name="action">押された時に発火するAction</param>
        /// <param name="throttleSeconds">連打防止の時間、デフォルト0.1秒</param>
        /// <returns>IDisposable、UniRXのOncolickからSubscribeまで</returns>
        public static IDisposable OnClickThrottleSubscribe(this Button button, Action action, float throttleSeconds = 0.1f)
        {
            return button.OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(throttleSeconds))
                .Subscribe(_ => action());
        }
    }
}
