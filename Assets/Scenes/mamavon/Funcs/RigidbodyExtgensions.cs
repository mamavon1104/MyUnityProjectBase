using Mamavon.Data;
using UnityEngine;

namespace Mamavon.Funcs
{
    /// <summary>
    /// Rigidbody �^�̊g�����\�b�h���Ǘ�����N���X
    /// </summary>
    public static class RigidbodyExtension
    {
        /// <summary>
        /// �ꎞ��~
        /// </summary>
        public static void Pause(this Rigidbody rigidbody, SaveVelocityCS velocityTmp)
        {
            velocityTmp.Set(rigidbody);
            rigidbody.isKinematic = true;
        }

        /// <summary>
        /// �ĊJ
        /// </summary>
        public static void Resume(this Rigidbody rigidbody, SaveVelocityCS velocityTmp)
        {
            rigidbody.isKinematic = false;
            rigidbody.velocity = velocityTmp.Velocity;
            rigidbody.angularVelocity = velocityTmp.AngularVelocity;
        }
    }
    /// <summary>
    /// Rigidbody2D �^�̊g�����\�b�h���Ǘ�����N���X
    /// </summary>
    public static class Rigidbody2DExtension
    {
        /// <summary>
        /// �ꎞ��~
        /// </summary>
        public static void Pause(this Rigidbody2D rigidbody, SaveVelocity2DCS velocityTmp)
        {
            velocityTmp.Set(rigidbody);
            rigidbody.simulated = false;
        }

        /// <summary>
        /// �ĊJ
        /// </summary>
        public static void Resume(this Rigidbody2D rigidbody, SaveVelocity2DCS velocityTmp)
        {
            rigidbody.simulated = true;
            rigidbody.velocity = velocityTmp.Velocity;
        }
    }
}