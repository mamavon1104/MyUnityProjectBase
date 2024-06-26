using UnityEngine;

namespace Mamavon.Funcs
{
    public enum TextColor
    {
        White,
        Black,
        Red,
        Blue,
        Green,
        Yellow,
        Magenta,
        Cyan,
    }
    internal static class DebugClass
    {
        /// <summary>
        /// Enumの名前を小文字に変換して色として使用
        /// </summary>
        private static string ConvertEnumToColorCode(TextColor color)
        {
            return color.ToString().ToLower();
        }
        private static string GetColorString(TextColor color)
        {
            return color == TextColor.White ? "" : $"<color={ConvertEnumToColorCode(color)}>";
        }

        /// <summary>
        /// Debug.Logを行います。
        /// 引数なしの場合はクラス名を表示しません。
        /// </summary>
        public static T Debuglog<T>(this T value, TextColor color = TextColor.White)
        {
            Debug.Log($"{GetColorString(color)} {typeof(T).Name}: {value} </color>");
            return value;
        }

        /// <summary>
        /// Debug.LogWarningを行います。
        /// 引数なしの場合はクラス名を表示しません。
        /// </summary>
        public static T DebuglogWarning<T>(this T value, TextColor color = TextColor.Yellow)
        {
            Debug.LogWarning($"{GetColorString(color)} {typeof(T).Name}: {value} </color>");
            return value;
        }

        /// <summary>
        /// Debug.LogErrorを行います。
        /// 引数なしの場合はクラス名を表示しません。
        /// </summary>
        public static T DebuglogError<T>(this T value, TextColor color = TextColor.Red)
        {
            Debug.LogError($"{GetColorString(color)} {typeof(T).Name}: {value} </color>");
            return value;
        }
    }
}