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
        /// Enum�̖��O���������ɕϊ����ĐF�Ƃ��Ďg�p
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
        /// Debug.Log���s���܂��B
        /// �����Ȃ��̏ꍇ�̓N���X����\�����܂���B
        /// </summary>
        public static T Debuglog<T>(this T value, TextColor color = TextColor.White)
        {
            Debug.Log($"{GetColorString(color)} {typeof(T).Name}: {value} </color>");
            return value;
        }

        /// <summary>
        /// Debug.Log���s���܂��B
        /// ��������̏ꍇ�̓N���X����\�����܂��B
        /// </summary>
        public static T Debuglog<T, O>(this T value, O orderClass, TextColor color = TextColor.White) where O : class
        {
            Debug.Log($"{GetColorString(color)} {orderClass} �� {typeof(T).Name}: {value} </color>");
            return value;
        }
    }
}