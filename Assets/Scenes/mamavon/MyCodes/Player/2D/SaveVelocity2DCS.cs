using UnityEngine;
namespace Mamavon.Data
{
    [System.Serializable]
    public class SaveVelocity2DCS
    {
        [SerializeField] Vector2 _velocity;

        public Vector2 Velocity
        {
            get { return _velocity; }
        }

        /// <summary>
        /// Rigidbody������ɂ��đ��x��ݒ肷��
        /// </summary>
        public void Set(Rigidbody2D rigidbody)
        {
            _velocity = rigidbody.velocity;
        }
    }
}