using Mamavon.Data;
using UnityEngine;

namespace Mamavon.Funcs
{
    /// <summary>
    /// Rigidbody 型の拡張メソッドを管理するクラス
    /// </summary>
    public static class RigidbodyExtension
    {
        /// <summary>
        /// 一時停止
        /// </summary>
        public static void Pause(this Rigidbody rigidbody, SaveVelocityCS velocityTmp)
        {
            velocityTmp.Set(rigidbody);
            rigidbody.isKinematic = true;
        }

        /// <summary>
        /// 再開
        /// </summary>
        public static void Resume(this Rigidbody rigidbody, SaveVelocityCS velocityTmp)
        {
            rigidbody.isKinematic = false;
            rigidbody.velocity = velocityTmp.Velocity;
            rigidbody.angularVelocity = velocityTmp.AngularVelocity;
        }
    }
    /// <summary>
    /// Rigidbody2D 型の拡張メソッドを管理するクラス
    /// </summary>
    public static class Rigidbody2DExtension
    {
        /// <summary>
        /// 一時停止
        /// </summary>
        public static void Pause(this Rigidbody2D rigidbody, SaveVelocity2DCS velocityTmp)
        {
            velocityTmp.Set(rigidbody);
            rigidbody.simulated = false;
        }

        /// <summary>
        /// 再開
        /// </summary>
        public static void Resume(this Rigidbody2D rigidbody, SaveVelocity2DCS velocityTmp)
        {
            rigidbody.simulated = true;
            rigidbody.velocity = velocityTmp.Velocity;
        }
    }
}