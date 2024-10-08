using UnityEngine;
namespace Mamavon.Data
{
    [System.Serializable]
    public class SaveVelocityCS
    {
        [SerializeField] Vector3 _angularVelocity;
        [SerializeField] Vector3 _velocity;

        public Vector3 AngularVelocity
        {
            get { return _angularVelocity; }
        }
        public Vector3 Velocity
        {
            get { return _velocity; }
        }

        /// <summary>
        /// Rigidbody‚ğˆø”‚É‚µ‚Ä‘¬“x‚ğİ’è‚·‚é
        /// </summary>
        public void Set(Rigidbody rigidbody)
        {
            _angularVelocity = rigidbody.angularVelocity;
            _velocity = rigidbody.velocity;
        }
    }
}