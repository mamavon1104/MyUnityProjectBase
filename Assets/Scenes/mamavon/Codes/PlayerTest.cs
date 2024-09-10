using Mamavon.Funcs;
using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerTest : MonoBehaviour
{
    [SerializeField] float m_speed = 10f;
    [SerializeField] private float m_jumpForce = 10f; // ジャンプの力
    [SerializeField] private Transform m_cameraTrans = default;
    [SerializeField] private SphereColliderClass m_collider;

    private Transform _myT;
    private Rigidbody _rig;
    private Vector2 _move;
    private Vector3 _myMoveVec;
    private RaycastHit _hit;

    private void Start()
    {
        _myT = transform;
        _rig = GetComponent<Rigidbody>();
        // ジャンプ処理
        this.UpdateAsObservable()
            .TakeUntilDestroy(this)
            .Where(_ => Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            .ThrottleFirst(TimeSpan.FromMilliseconds(10))
            .Subscribe(_ =>
            {
                _rig.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            });
    }

    private void FixedUpdate()
    {
        _move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _myMoveVec = m_cameraTrans.CalculateMovementDirection(_move) * m_speed;
    }
    private void Update()
    {
        _rig.velocity = new Vector3(_myMoveVec.x, _rig.velocity.y, _myMoveVec.z);
    }

    // 地面に接しているかどうかをチェックするメソッド
    private bool IsGrounded()
    {
        return _myT.CheckGroundSphere(m_collider, out _hit, true).Debuglog(TextColor.Green);
    }
}