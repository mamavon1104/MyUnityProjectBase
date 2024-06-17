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
            return color == TextColor.White ? "#ffffff" : ConvertEnumToColorCode(color);
        }

        /// <summary>
        /// Debug.Logを行います。
        /// 引数なしの場合はクラス名を表示しません。
        /// </summary>
        public static T Debuglog<T>(this T value, TextColor color = TextColor.White)
        {
            Debug.Log($"Debug : <color={GetColorString(color)}> {typeof(T).Name}: {value} </color>");
            return value;
        }

        /// <summary>
        /// Debug.Logを行います。
        /// 引数ありの場合はクラス名を表示します。
        /// </summary>
        public static T Debuglog<T, O>(this T value, O orderClass, TextColor color = TextColor.White) where O : class
        {
            Debug.Log($"Debug : <color={GetColorString(color)}> {orderClass} の {typeof(T).Name}: {value} </color>");
            return value;
        }
    }
    internal static class VectorClass
    {
        /// <summary>
        /// Vector3×Vector3を行います。 
        /// VectorAのx,y,zそれぞれの値をVectorBのそれぞれ値を倍にします。
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
        /// Vector2×Vector2を行います。 
        /// 単純にx,yそれぞれの値を引数のそれぞれ値倍にします。
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
        /// カメラのTransformから何方に回転しているか見て、それとinputを掛ける事で移動方向のvectorをゲットする
        /// </summary>
        /// <param name="cameraTransform">CameraのTransform</param>
        /// <param name="inputVector"> playerの動き{vector2}</param>
        /// <returns> こいつの値にspeedを掛けてくれれば移動できるはずさ。 </returns>
        public static Vector3 CalculateMovementDirection(this Transform cameraTransform, Vector2 inputVector)
        {
            return (Quaternion.AngleAxis(cameraTransform.eulerAngles.y, Vector3.up) //カメラの回転
                    * new Vector3(inputVector.x, 0, inputVector.y))               　//playerのvector
                    .normalized;                                                    //正規化する
        }
    }
}