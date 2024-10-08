using Mamavon.Data;
using Mamavon.Funcs;
using UniRx;
using UnityEngine;

namespace Mamavon.Code
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerJumpClass2D : MonoBehaviour
    {
        [SerializeField] InputSystemNameActionData m_jump;
        [SerializeField] Rigidbody2D _rig;
        [SerializeField] float m_jumpForce = 10f;
        [SerializeField] Player2DGroundData m_groundData;

        private Transform _myT;
        private RaycastHit2D _hit;

        private void Start()
        {
            _myT = transform;
            _rig = GetComponent<Rigidbody2D>();

            switch (m_groundData)
            {
                case null:
                    m_groundData.DebuglogError("nullです");
                    break;
                case CircleCastGroundData collider:
                    collider.Radius = _myT.localScale.x;
                    break;
                case BoxCastGroundData collider:
                    collider.Scale = _myT.localScale;
                    break;
            }

            m_jump.GetObservable<Unit>().
                   Where(_ => IsGrounded()).
                   Subscribe(_ =>
                   {
                       Jump(m_jumpForce);
                   }).AddTo(this);
        }

        public void Jump(float jumpForce)
        {
            _rig.velocity = new Vector2(_rig.velocity.x, jumpForce);
        }

        // 地面に接しているかどうかをチェックするメソッド
        public bool IsGrounded()
        {
            return m_groundData.CheckGround2D(_myT, out _hit) ? true : false;
        }
        private void OnDrawGizmos()
        {
            if (_myT != null && m_groundData != null)
            {
                bool isGrounded = IsGrounded();
                m_groundData.DrawGroundCheckGizmo2D(_myT, isGrounded);
            }
        }
    }
}