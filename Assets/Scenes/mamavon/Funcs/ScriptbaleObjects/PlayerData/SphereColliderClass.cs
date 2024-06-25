using UnityEngine;

namespace Mamavon.Funcs
{

    [CreateAssetMenu(fileName = "SphereColliderScriptsObjs", menuName = "Mamavon Packs/Object Scripts/Sphere Collider ScriptObjs")]
    public class SphereColliderClass : ScriptableObject
    {
        [Header("SphereCollider‚Ì”¼Œa")] public float radius = 0.5f;
        [Header("‰º‚ÉL‚Î‚·‚½‚ß‚Ì‹——£")] public float length = 0.1f;
        [Header("’n–Ê‚ÌƒŒƒCƒ„[‚ğİ’è")] public LayerMask groundLayer = 1 << 0;
        [Header("•`‰æ‚·‚éŠÔ‚ğ‚±‚±‚É")] public byte drawSeconds = 2;
        [Header("•`‰æ‚·‚é‚Í‚±‚ê‚ğtrue‚É")] public bool isDraw = false;

        /// <summary>
        /// Radius‚ª‚Ò‚Á‚½‚è(transform.size / 2)‚¾‚Æ‰½ŒÌ‚©ãè‚És‚©‚È‚¢‚Ì‚Å‚±‚ê‚Åˆø‚¢‚Ä‚ ‚°‚È‚¢‚Æ‚¢‚¯‚È‚¢B
        /// </summary>
        public const float RADIUS_TOLERANCE = 0.0001f;
    }
}
