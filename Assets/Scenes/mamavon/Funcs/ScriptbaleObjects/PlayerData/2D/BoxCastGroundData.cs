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
        /// <param name="player">チェックするTransformオブジェクト</param>
        /// <param name="hit">地面チェックの結果としてのRaycastHit2Dオブジェクト</param>
        /// <returns>オブジェクトが地面に接している場合はtrue、それ以外の場合はfalse</returns>
        public override bool CheckGround2D(Transform player, out RaycastHit2D hit)
        {
            RaycastHit2D[] hits = Physics2D.BoxCastAll(
                player.position,                             // Objの位置
                scale,                                       // ボックスのサイズ
                player.rotation.eulerAngles.z,                  // ボックスの回転角度
                Vector2.down,                                // BoxCastの方向（下方向）
                base.length,                                 // BoxCastの最大距離
                base.groundLayer                             // 衝突を検出するレイヤーマスク
            );

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform == player)
                    continue;

                if (hits[i].collider.isTrigger) //トリガーだったらもう一度
                    continue;

                hit = hits[i]; //outの物に代入して

                //hit.transform.Debuglog();

                return true;
            }

            hit = default;
            return false;
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

        /// <summary>
        /// プレイヤーのオブジェクトの横に壁があるかどうかをチェックします。
        /// BoxCast2Dを使用しています
        /// </summary>
        /// <param name="obj">チェックするTransformオブジェクト</param>
        /// <param name="direction">チェックする方向（右または左）</param>
        /// <param name="hit">壁チェックの結果としてのRaycastHit2Dオブジェクト</param>
        /// <returns>オブジェクトの横に壁がある場合はtrue、それ以外の場合はfalse</returns>
        public override bool CheckSideWall2D(Transform obj, out RaycastHit2D hit, Vector2 direction)
        {
            // BoxCast2Dを使用して壁チェックを実行します。
            hit = Physics2D.BoxCast(
                obj.position,                                // Objの位置
                scale,                                       // ボックスのサイズ
                obj.rotation.eulerAngles.z,                  // ボックスの回転角度
                direction,                                   // BoxCastの方向（右または左）
                base.length,                           // BoxCastの最大距離
                base.groundLayer                             // 衝突を検出するレイヤーマスク
            );

            return hit.collider != null;
        }

        /// <summary>
        /// 横方向の壁チェックのギズモを描画します。
        /// </summary>
        /// <param name="obj">チェックするTransformオブジェクト</param>
        /// <param name="direction">チェックする方向（右または左）</param>
        /// <param name="isWallDetected">壁が検出されたかどうか</param>
        public override void DrawSideWallCheckGizmo2D(Transform obj, bool isWallDetected, Vector2 direction)
        {
            if (!base.isDraw)
                return;

            Color gizmoColor = isWallDetected ? Color.yellow : Color.cyan;
            gizmoColor.a = base.gizmoAlpha; // 透明度を設定

            Gizmos.color = gizmoColor;

            Vector2 endPosition = (Vector2)obj.position + direction * base.length;

            // 終了位置の矩形を描画
            Gizmos.DrawWireCube(endPosition, scale);
        }
    }
}