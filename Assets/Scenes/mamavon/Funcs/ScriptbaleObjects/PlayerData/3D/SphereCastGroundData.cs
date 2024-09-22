using UnityEngine;

namespace Mamavon.Data
{

    [CreateAssetMenu(fileName = "SphereColliderScriptsObjs", menuName = "Mamavon Packs/ScriptableObject/Object Scripts/Sphere Collider ScriptObjs")]
    public class SphereCastGroundData : Player3DGroundData
    {
        [Header("SphereColliderの半径")] private float radius = 0.5f;

        public override bool CheckGround(Transform obj, out RaycastHit hit)
        {
            // SphereCastを使用して地面チェックを実行します。
            bool isGrounded = Physics.SphereCast(
                obj.position,                                           // Objの方向
                radius - size_margine, // SphereCastの半径 - ほんのちょっとだけ
                Vector3.down,                                           // SphereCastの方向（下方向）
                out hit,                                                // 衝突情報を受け取るためのRaycastHit
                base.length,                                        // SphereCastの最大距離
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

            float r = radius - size_margine;

            Vector3 endPosition = obj.position + Vector3.down * base.length;
            Gizmos.DrawSphere(endPosition, r);
        }
    }
}
