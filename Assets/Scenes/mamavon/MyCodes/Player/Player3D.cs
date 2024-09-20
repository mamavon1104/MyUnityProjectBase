using Mamavon.Data;
using Mamavon.Funcs;
using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerTest : MonoBehaviour
{
    [SerializeField] float m_playerMoveSpeed = 10f;
    [SerializeField] float m_playerRotateSpeed = 10f;
    [SerializeField] private float m_jumpForce = 10f; // ジャンプの力
    [SerializeField] private Transform m_cameraTrans = default;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private PlayerCastGroundBase m_collider;

    private Transform _myT;
    private Rigidbody _rig;
    private Vector2 _move;
    private Vector3 _myMoveVec;
    private RaycastHit _hit;
    private Quaternion _targetRotation;

    private System.Func<PlayerCastGroundBase, RaycastHit, bool> _jumpCheckGround;
    private void Start()
    {
        _myT = transform;
        _rig = GetComponent<Rigidbody>();
        _targetRotation = _myT.rotation;

        _jumpCheckGround = switch m_collider
        {
            case SphereCastGroundData => CheckGroundSphere
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
        _myMoveVec = m_cameraTrans.CalculateMovementDirection(_move) * m_playerMoveSpeed;
        _rig.velocity = new Vector3(_myMoveVec.x, _rig.velocity.y, _myMoveVec.z);
    }
    private void Update()
    {
        _move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (_myMoveVec.sqrMagnitude > 0)
            _targetRotation = Quaternion.LookRotation(_myMoveVec);

        _myT.rotation = Quaternion.RotateTowards(_myT.rotation, _targetRotation, m_playerRotateSpeed * Time.deltaTime);
    }

    // 地面に接しているかどうかをチェックするメソッド
    private bool IsGrounded()
    {
        if (!_myT.CheckGroundSphere((SphereCastGroundData)m_collider, out _hit).Debuglog(TextColor.Green))
            return false;

        return true;
    }
    //private void OnDrawGizmos()
    //{
    //    if (_myT != null && m_collider != null)
    //    {
    //        bool isGrounded = IsGrounded();
    //        PlayerCheckGroundClass.DrawGroundCheckGizmo(_myT, m_collider, isGrounded);
    //    }
    //}
}