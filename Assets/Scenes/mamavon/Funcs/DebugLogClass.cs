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
            return color == TextColor.White ? "#ffffff" : ConvertEnumToColorCode(color);
        }

        /// <summary>
        /// Debug.Log���s���܂��B
        /// �����Ȃ��̏ꍇ�̓N���X����\�����܂���B
        /// </summary>
        public static T Debuglog<T>(this T value, TextColor color = TextColor.White)
        {
            Debug.Log($"Debug : <color={GetColorString(color)}> {typeof(T).Name}: {value} </color>");
            return value;
        }

        /// <summary>
        /// Debug.Log���s���܂��B
        /// ��������̏ꍇ�̓N���X����\�����܂��B
        /// </summary>
        public static T Debuglog<T, O>(this T value, O orderClass, TextColor color = TextColor.White) where O : class
        {
            Debug.Log($"Debug : <color={GetColorString(color)}> {orderClass} �� {typeof(T).Name}: {value} </color>");
            return value;
        }
    }
    internal static class VectorClass
    {
        /// <summary>
        /// Vector3�~Vector3���s���܂��B 
        /// VectorA��x,y,z���ꂼ��̒l��VectorB�̂��ꂼ��l��{�ɂ��܂��B
        /// x1 * x2; y1 * y2; z1 * z2;
        /// </summary>
        public static Vector3 MultiVecs(this Vector3 vectorA, Vector3 vectorB)
        {
            Vector3 vecResult = new Vector3();
            vecResult.x = vectorA.x * vectorB.x;
            vecResult.y = vectorA.y * vectorB.y;
            vecResult.z = vectorA.z * vectorB.z;
            return vecResult;
        }

        /// <summary>
        /// Vector2�~Vector2���s���܂��B 
        /// �P����x,y���ꂼ��̒l�������̂��ꂼ��l�{�ɂ��܂��B
        /// </summary>
        public static Vector2 MultiVecs(this Vector2 vectorA, Vector2 vectorB)
        {
            Vector2 vecResult = new Vector2();
            vecResult.x = vectorA.x * vectorB.x;
            vecResult.y = vectorA.y * vectorB.y;
            return vecResult;
        }
    }
    internal static class CameraClass
    {
        /// <summary>
        /// �J������Transform���牽���ɉ�]���Ă��邩���āA�����input���|���鎖�ňړ�������vector���Q�b�g����
        /// </summary>
        /// <param name="cameraTransform">Camera��Transform</param>
        /// <param name="inputVector"> player�̓���{vector2}</param>
        /// <returns> �����̒l��speed���|���Ă����Έړ��ł���͂����B </returns>
        public static Vector3 CalculateMovementDirection(this Transform cameraTransform, Vector2 inputVector)
        {
            return (Quaternion.AngleAxis(cameraTransform.eulerAngles.y, Vector3.up) //�J�����̉�]
                    * new Vector3(inputVector.x, 0, inputVector.y))               �@//player��vector
                    .normalized;                                                    //���K������
        }
    }
}