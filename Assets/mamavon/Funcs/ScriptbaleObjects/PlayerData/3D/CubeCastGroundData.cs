using UnityEngine;

namespace Mamavon.Data
{
    [CreateAssetMenu(fileName = "BoxCastGroundData", menuName = "Mamavon Packs/ScriptableObject/Object Scripts/BoxCastGroundData")]
    public class CubeCastGroundData : Player3DGroundData
    {
        [SerializeField] private Vector3 halfScale;
        public Vector3 Scale
        {
            set
            {
                halfScale = new Vector3(value.x / 2 - size_margine,
                                        value.y / 2 - size_margine,
                                        value.z / 2 - size_margine);
            }
        }

        /// <summary>
        /// プレイヤーのオブジェクトが地面に接しているかどうかをチェックします。
        /// Box Castを使用しています
        /// </summary>
        /// <param name="obj">チェックするTransformオブジェクト</param>
        /// <param name="scripObj">BoxCastGroundDataオブジェクト</param>
        /// <param name="hit">地面チェックの結果としてのRaycastHitオブジェクト</param>
        /// <returns>オブジェクトが地面に接している場合はtrue、それ以外の場合はfalse</returns>
        public override bool CheckGround(Transform obj, out RaycastHit hit)
        {
            // BoxCastを使用して地面チェックを実行します。
            bool isGrounded = Physics.BoxCast(
                obj.position,                                           // Objの位置
                halfScale,                                            // ボックスの半分のサイズ
                Vector3.down,                                           // BoxCastの方向（下方向）
                out hit,                                                // 衝突情報を受け取るためのRaycastHit
                obj.rotation,                                           // ボックスの回転
                base.length,                                        // BoxCastの最大距離
                base.groundLayer,                                   // 衝突を検出するレイヤーマスク
                QueryTriggerInteraction.Ignore                          // トリガーを無視
            );

            return isGrounded;
        }

        public override void DrawGroundCheckGizmo(Transform obj, bool isGrounded)
        {
            if (!base.isDraw)
                return;

            Color gizmoColor = isGrounded ? Color.green : Color.red;
            gizmoColor.a = base.gizmoAlpha; // 透明度を設定

            Gizmos.color = gizmoColor;

            Vector3 endPosition = obj.position + Vector3.down * base.length;

            // 終了位置のキューブを描画
            Gizmos.DrawCube(endPosition, halfScale * 2);
        }
    }
}