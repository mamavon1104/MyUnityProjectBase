using Mamavon.Data;
using Mamavon.Funcs;
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
        [SerializeField] float m_rotateSpeed = 10f;
        [SerializeField] private float m_jumpForce = 10f;
        [SerializeField] private Player2DGroundData m_groundData;

        private Transform _myT;
        private Rigidbody2D _rig;
        private float _moveFloat;
        private RaycastHit2D _hit;
        private Quaternion _targetRotation;

        private void Start()
        {
            _myT = transform;
            _rig = GetComponent<Rigidbody2D>();
            _targetRotation = _myT.rotation;

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
                .Where(_ => Input.GetKeyDown(KeyCode.Space) && IsGrounded())
                .ThrottleFirst(TimeSpan.FromMilliseconds(10))
                .Subscribe(_ =>
                {
                    _rig.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Impulse);
                }).AddTo(this);
        }

        private void FixedUpdate()
        {
            _rig.velocity = new Vector3(_moveFloat * m_moveSpeed, _rig.velocity.y);
        }
        private void Update()
        {
            _moveFloat = Input.GetAxisRaw("Horizontal");

            //if (_myMoveVec.sqrMagnitude > 0)
            //    _targetRotation = Quaternion.LookRotation(_myMoveVec);
            //_myT.rotation = Quaternion.RotateTowards(_myT.rotation, _targetRotation, m_rotateSpeed * Time.deltaTime);

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