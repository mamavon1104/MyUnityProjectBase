using UnityEngine;

namespace Mamavon.Data
{

    [CreateAssetMenu(fileName = "SphereColliderScriptsObjs", menuName = "Mamavon Packs/ScriptableObject/Object Scripts/Sphere Collider ScriptObjs")]
    public class SphereCastGroundData : PlayerCastGroundBase
    {
        [Header("SphereCollider‚Ì”¼Œa")] public float radius = 0.5f;
        /// <summary>
        /// Radius‚ª‚Ò‚Á‚½‚è(transform.size / 2)‚¾‚Æ‰½ŒÌ‚©ãè‚És‚©‚È‚¢‚Ì‚Å‚±‚ê‚Åˆø‚¢‚Ä‚ ‚°‚È‚¢‚Æ‚¢‚¯‚È‚¢B
        /// </summary>
        public const float RADIUS_TOLERANCE = 0.0001f;
    }
}
