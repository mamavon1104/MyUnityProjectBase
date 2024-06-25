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
        /// Debug.Logを行います。
        /// 引数ありの場合はクラス名を表示します。
        /// </summary>
        public static T Debuglog<T, O>(this T value, O orderClass, TextColor color = TextColor.White) where O : class
        {
            Debug.Log($"{GetColorString(color)} {orderClass} の {typeof(T).Name}: {value} </color>");
            return value;
        }
    }
}