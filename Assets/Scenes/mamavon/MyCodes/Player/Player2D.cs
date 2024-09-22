using Mamavon.Data;
using Mamavon.Funcs;
using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Mamavon.Code
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player2D : MonoBehaviour
    {
        [SerializeField] float m_moveSpeed = 10f;
        [SerializeField] float m_rotateSpeed = 10f;
        [SerializeField] private float m_jumpForce = 10f;
        [SerializeField] private Transform m_cameraTrans = default;
        [SerializeField] private Player2DGroundData m_groundData;

        private Transform _myT;
        private Rigidbody _rig;
        private Vector2 _move;
        private Vector3 _myMoveVec;
        private RaycastHit2D _hit;
        private Quaternion _targetRotation;

        private void Start()
        {
            _myT = transform;
            _rig = GetComponent<Rigidbody>();
            _targetRotation = _myT.rotation;

            switch (m_groundData)
            {
                case SphereCastGroundData:
                    break;
                case CubeCastGroundData collider:
                    collider.Scale = _myT.localScale;
                    break;
            }

            this.UpdateAsObservable()
                .TakeUntilDestroy(this)
                .Where(_ => Input.GetKeyDown(KeyCode.Space) && IsGrounded())
                .ThrottleFirst(TimeSpan.FromMilliseconds(10))
                .Subscribe(_ =>
                {
                    _rig.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
                }).AddTo(this);
        }

        private void FixedUpdate()
        {
            _myMoveVec = m_cameraTrans.CalculateMovementDirection(_move) * m_moveSpeed;
            _rig.velocity = new Vector3(_myMoveVec.x, _rig.velocity.y, _myMoveVec.z);
        }
        private void Update()
        {
            _move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (_myMoveVec.sqrMagnitude > 0)
                _targetRotation = Quaternion.LookRotation(_myMoveVec);

            _myT.rotation = Quaternion.RotateTowards(_myT.rotation, _targetRotation, m_rotateSpeed * Time.deltaTime);
        }

        // 地面に接しているかどうかをチェックするメソッド
        private bool IsGrounded()
        {
            if (!m_groundData.CheckGround2D(_myT, out _hit).Debuglog(TextColor.Green))
                return false;

            return true;
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