using UnityEngine;

namespace Mamavon.Data
{
    [CreateAssetMenu(fileName = "BoxCast2DGroundData", menuName = "Mamavon Packs/ScriptableObject/Object Scripts/BoxCast2DGroundData")]
    public class BoxCastGroundData : Player2DGroundData
    {
        [SerializeField] private Vector2 scale;
        public Vector2 Scale
        {
            set
            {
                scale = new Vector2(value.x - size_margine,
                                   value.y - size_margine);
            }
        }

        /// <summary>
        /// プレイヤーのオブジェクトが地面に接しているかどうかをチェックします。
        /// BoxCast2Dを使用しています
        /// </summary>
        /// <param name="obj">チェックするTransformオブジェクト</param>
        /// <param name="hit">地面チェックの結果としてのRaycastHit2Dオブジェクト</param>
        /// <returns>オブジェクトが地面に接している場合はtrue、それ以外の場合はfalse</returns>
        public override bool CheckGround2D(Transform obj, out RaycastHit2D hit)
        {
            // BoxCast2Dを使用して地面チェックを実行します。
            hit = Physics2D.BoxCast(
                obj.position,                                // Objの位置
                scale,                                       // ボックスのサイズ
                obj.rotation.eulerAngles.z,                  // ボックスの回転角度
                Vector2.down,                                // BoxCastの方向（下方向）
                base.length,                                 // BoxCastの最大距離
                base.groundLayer                             // 衝突を検出するレイヤーマスク
            );

            return hit.collider != null;
        }

        public override void DrawGroundCheckGizmo2D(Transform obj, bool isGrounded)
        {
            if (!base.isDraw)
                return;

            Color gizmoColor = isGrounded ? Color.green : Color.red;
            gizmoColor.a = base.gizmoAlpha; // 透明度を設定

            Gizmos.color = gizmoColor;

            Vector2 endPosition = (Vector2)obj.position + Vector2.down * base.length;

            // 終了位置の矩形を描画
            Gizmos.DrawWireCube(endPosition, scale);
        }
    }
}