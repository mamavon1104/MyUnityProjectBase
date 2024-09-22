using UnityEngine;

namespace Mamavon.Data
{
    public class PlayerCastGroundBase : ScriptableObject
    {
        [Header("下に伸ばすための距離")] public float length = 0.1f;
        [Header("地面のレイヤーを設定")] public LayerMask groundLayer = 1 << 0;
        [Header("描画する時はこれをtrueに")] public bool isDraw = false;
        [Range(0.5f, 1), Header("描画するときの透明度")] public float gizmoAlpha = 0.75f;

        /// <summary>
        /// Radiusやサイズがぴったりtransform.sizeだと何故か上手に行かないのでこれで引いてあげないといけない。
        /// </summary>
        protected readonly float size_margine = 0.0001f;
    }
}