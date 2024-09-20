using UnityEngine;

namespace Mamavon.Data
{

    [CreateAssetMenu(fileName = "SphereColliderScriptsObjs", menuName = "Mamavon Packs/ScriptableObject/Object Scripts/Sphere Collider ScriptObjs")]
    public class SphereCastGroundData : PlayerCastGroundBase
    {
        [Header("SphereCollider�̔��a")] public float radius = 0.5f;
        /// <summary>
        /// Radius���҂�����(transform.size / 2)���Ɖ��̂����ɍs���Ȃ��̂ł���ň����Ă����Ȃ��Ƃ����Ȃ��B
        /// </summary>
        public const float RADIUS_TOLERANCE = 0.0001f;
    }
}
