using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UnityEngine;
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

        public static UniTask FadeIn(this Image image, float duration)
        {
            // FadeInは透明から不透明へ
            return FadeAsync(image, 0f, 1f, duration);
        }

        public static UniTask FadeOut(this Image image, float duration)
        {
            // FadeOutは不透明から透明へ
            return FadeAsync(image, 1f, 0f, duration);
        }
        private static async UniTask FadeAsync(Image image, float startAlpha, float endAlpha, float duration)
        {
            image.gameObject.SetActive(true);
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                await UniTask.Yield();
            }

            image.color = new Color(image.color.r, image.color.g, image.color.b, endAlpha);

            if (endAlpha == 0f)
                image.gameObject.SetActive(false);
        }
    }
}
