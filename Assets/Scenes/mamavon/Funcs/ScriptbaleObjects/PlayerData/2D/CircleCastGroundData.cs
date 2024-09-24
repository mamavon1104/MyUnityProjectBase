using UnityEngine;

namespace Mamavon.Data
{
    [CreateAssetMenu(fileName = "CircleCollider2DScriptsObjs", menuName = "Mamavon Packs/ScriptableObject/Object Scripts/Circle Collider 2D ScriptObjs")]
    public class CircleCastGroundData : Player2DGroundData
    {
        [Header("CircleCollider2Dの半径")]
        [SerializeField] private float r = 0.5f;
        public float Radius
        {
            set { r = value - size_margine; }
        }

        public override bool CheckGround2D(Transform obj, out RaycastHit2D hit)
        {
            // CircleCast2Dを使用して地面チェックを実行します。
            hit = Physics2D.CircleCast(
                obj.position,                                           // Objの位置
                r,                                  // CircleCastの半径 - ほんのちょっとだけ
                Vector2.down,                                           // CircleCastの方向（下方向）
                base.length,                                            // CircleCastの最大距離
                base.groundLayer                                        // 衝突を検出するレイヤーマスク
            );

            return hit.collider != null;
        }

        public override bool CheckSideWall2D(Transform obj, out RaycastHit2D hit, Vector2 direction)
        {
            throw new System.NotImplementedException();
        }

        public override void DrawGroundCheckGizmo2D(Transform obj, bool isGrounded)
        {
            if (!base.isDraw)
                return;

            Color gizmoColor = isGrounded ? Color.green : Color.red;
            gizmoColor.a = base.gizmoAlpha; // 透明度を設定

            Gizmos.color = gizmoColor;

            Vector2 endPosition = (Vector2)obj.position + Vector2.down * base.length;

            // 2D円を描画するために、XY平面上に円を描画
            Vector3 center = new Vector3(endPosition.x, endPosition.y, 0);
            Vector3 size = new Vector3(r * 2, r * 2, 0);
            Gizmos.DrawWireSphere(center, r);
        }

        public override void DrawSideWallCheckGizmo2D(Transform obj, bool isWallDetected, Vector2 direction)
        {
            throw new System.NotImplementedException();
        }
    }
}