using System;
using UniRx;
using UnityEngine.UI;
namespace Mamavon.Funcs
{
    public static class UIExtensions
    {
        /// <summary>
        /// ボタンの拡張メソッドで、連打防止の準備をはやくしちゃいます。
        /// </summary>
        /// <param name="debounceTime">連打防止の時間、デフォルト0.1秒</param>
        /// <returns>IObservable<Unit>、ThrottleFirstまでの処理</returns>
        public static IObservable<Unit> OnClickThrottle(this Button button, float debounceTime = 0.1f)
        {
            return button.OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(debounceTime));
        }
    }
}
