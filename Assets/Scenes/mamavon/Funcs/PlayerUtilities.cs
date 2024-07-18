using UnityEngine;
namespace Mamavon.Funcs
{
    //Player Utilities　プレイヤーが簡単に動けるように基礎的な動作を纏め上げますよー

    public static class PlayerCheckGroundClass
    {
        /// <summary>
        /// プレイヤーのオブジェクトが地面に接しているかどうかをチェックします。
        /// Sphere Castを使用しています
        /// </summary>
        /// <param name="obj">チェックするTransformオブジェクト</param>
        /// <param name="groundCheckOffsetY">地面チェックのY方向オフセット</param>
        /// <param name="groundCheckRadius">地面チェックの球の半径</param>
        /// <param name="groundCheckDistance">地面チェックの最大距離</param>
        /// <param name="groundLayers">地面として扱うレイヤーのマスク</param>
        /// <param name="hit">地面チェックの結果としてのRaycastHitオブジェクト</param>
        /// <param name="isDraw">デバッグ表示を行うかどうか</param>
        /// <returns>オブジェクトが地面に接している場合はtrue、それ以外の場合はfalse</returns>
        public static bool CheckGroundSphere(this Transform obj, SphereColliderClass scripObj, out RaycastHit hit, bool isDraw = false)
        {
            // SphereCastを使用して地面チェックを実行します。
            bool isGrounded = Physics.SphereCast(
                obj.position,                                           // Objの方向
                scripObj.radius - SphereColliderClass.RADIUS_TOLERANCE, // SphereCastの半径 - ほんのちょっとだけ
                Vector3.down,                                           // SphereCastの方向（下方向）
                out hit,                                                // 衝突情報を受け取るためのRaycastHit
                scripObj.length,                                        // SphereCastの最大距離
                scripObj.groundLayer,                                   // 衝突を検出するレイヤーマスク
                QueryTriggerInteraction.Ignore                          // トリガーを無視
            );

            if (isDraw)
                Debug.DrawRay(obj.position, Vector3.down * scripObj.length, isGrounded ? Color.green : Color.red);

            return isGrounded;
        }
    }
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
    internal static class TransformExtention
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