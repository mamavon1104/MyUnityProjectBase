using Mamavon.Data;
using Mamavon.Funcs;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementClass2D : MonoBehaviour
{
    [SerializeField] private InputSystemNameActionData m_move;
    [SerializeField] private float m_moveSpeed = 10f;
    [SerializeField] private float m_maxSpeed = 10f;
    [SerializeField] private Rigidbody2D _rig;

    private Vector2 _moveVec;
    private void Start()
    {
        _rig = GetComponent<Rigidbody2D>();
        var playerIndex = GetComponent<PlayerInput>().playerIndex;

        // Debug.logを後付けしながらテキストカラーも設定できる拡張
        // InputSystemをUniRx形式で待つ拡張機能 (hoge.GetObservable<型>(playerIndex))
        m_move.GetObservable<Vector2>(playerIndex.Debuglog(TextColor.Green)).Subscribe(v =>
        {
            _moveVec = v; //UniRX * PlayerInput
        }).AddTo(this);

        #region InputManagerのUniRx
        //InputObservableUtility.OnAxisInput("Horizontal").Subscribe(v =>
        //{
        //    _moveVec = v; 
        //}).AddTo(this);
        #endregion
    }
    private void FixedUpdate()
    {
        _rig.velocity = _moveVec * m_moveSpeed;

        //if (_rig.velocity.sqrMagnitude < m_maxSpeed) 
        //_rig.AddForce(_moveVec * m_moveSpeed, ForceMode2D.Impulse);
    }
}