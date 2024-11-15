using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Mamavon.Func
{
    public static class ColorExtensions
    {
        public static async UniTask LerpToAsync(this Color now, Color set, float duration, Action<Color> act)
        {
            float elapsedTime = 0f;
            Color startColor = now;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                Color lerpedColor = Color.Lerp(startColor, set, t);

                act.Invoke(lerpedColor);
                await UniTask.Yield();
            }
            act.Invoke(set);
        }
    }
}
