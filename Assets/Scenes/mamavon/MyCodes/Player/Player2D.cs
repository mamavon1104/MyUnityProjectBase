using Mamavon.Data;
using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Mamavon.Code
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player2D : MonoBehaviour
    {
        [SerializeField] float m_moveSpeed = 10f;
        [SerializeField] float m_maxSpeed = 10f;
        [SerializeField] private float m_jumpForce = 10f;
        [SerializeField] private Player2DGroundData m_groundData;

        private Transform _myT;
        private Rigidbody2D _rig;
        private float _moveFloat;
        private RaycastHit2D _hit;

        [Range(0, 1f), SerializeField] private float fallSpeed;
        private void Start()
        {
            _myT = transform;
            _rig = GetComponent<Rigidbody2D>();

            switch (m_groundData)
            {
                case CircleCastGroundData collider:
                    collider.Radius = _myT.localScale.x;
                    break;
                case BoxCastGroundData collider:
                    collider.Scale = _myT.localScale;
                    break;
            }

            this.UpdateAsObservable()
                .TakeUntilDestroy(this)
                .Where(_ => Input.GetKeyDown(KeyCode.Space))
                .Where(_ => IsGrounded() || IsSideWall())
                .ThrottleFirst(TimeSpan.FromMilliseconds(10))
                .Subscribe(_ =>
                {
                    _rig.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Impulse);
                }).AddTo(this);
        }

        private void FixedUpdate()
        {
            if (_rig.velocity.magnitude < m_maxSpeed)
                _rig.AddForce(new Vector2(_moveFloat * m_moveSpeed, 0));
        }
        private void Update()
        {
            _moveFloat = Input.GetAxisRaw("Horizontal");
        }

        // 地面に接しているかどうかをチェックするメソッド
        private bool IsGrounded()
        {
            return m_groundData.CheckGround2D(_myT, out _hit) ? true : false;
        }
        private bool IsSideWall()
        {
            return m_groundData.CheckSideWall2D(_myT, out _hit, _myT.right) ? true : false;
        }
        private void OnDrawGizmos()
        {
            if (_myT != null && m_groundData != null)
            {
                bool isGrounded = IsGrounded();
                bool isSideWall = IsSideWall();
                m_groundData.DrawGroundCheckGizmo2D(_myT, isGrounded);
                m_groundData.DrawSideWallCheckGizmo2D(_myT, isSideWall, _myT.right);
            }
        }
    }
}