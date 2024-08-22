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
    public static class DebugExtensions
    {
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
        /// 引数strがある場合、クラス名などは表示せずstr + 値で返します
        /// </summary>
        public static T Debuglog<T>(this T value, string str, TextColor color = TextColor.White)
        {
            Debug.Log($"{str} : {GetColorString(color)}{value}。 </color>");
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

        /// <summary>
        /// TextColorに合った色を返します。
        /// </summary>
        /// <param name="color">TextColor</param>
        /// <returns>Color型の色を返す</returns>
        public static Color ConvertEnumToColor(TextColor color)
        {
            string colorCode = ConvertEnumToColorCode(color);
            if (ColorUtility.TryParseHtmlString(colorCode, out Color parsedColor))
            {
                return parsedColor.Debuglog();
            }
            return Color.white; // デフォルトの色として白を返す
        }
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
    }
}