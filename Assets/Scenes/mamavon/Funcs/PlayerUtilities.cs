using UnityEngine;

namespace Mamavon.Funcs
{
    //Player Utilities　プレイヤーが簡単に動けるように基礎的な動作を纏め上げますよー

    internal static class CameraClass
    {
        /// <summary>
        /// カメラのTransformから何方に回転しているか見て、それとinputを掛ける事で移動方向のvectorをゲットする
        /// </summary>
        /// <param name="cameraTransform">CameraのTransform</param>
        /// <param name="inputVector"> playerの動き{vector2}</param>
        /// <returns> Vector3で返す こいつの値にspeedを掛けてくれれば移動できるはずさ。 </returns>
        public static Vector3 CalculateMovementDirection(this Transform cameraTransform, Vector2 inputVector)
        {
            return (Quaternion.AngleAxis(cameraTransform.eulerAngles.y, Vector3.up) //カメラの回転
                    * new Vector3(inputVector.x, 0, inputVector.y))                //playerのvector
                    .normalized;                                                    //正規化する
        }
    }
    public static class TransformExtention
    {
        public static void Reset(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
        public static void SetPosX(this Transform transform, float x)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        public static void SetPosY(this Transform transform, float y)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        public static void SetPosZ(this Transform transform, float z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }
    }
}