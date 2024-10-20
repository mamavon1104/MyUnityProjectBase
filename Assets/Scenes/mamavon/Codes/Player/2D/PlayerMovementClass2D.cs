using Mamavon.Data;
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

    private float _moveFloat;
    private void Start()
    {
        _rig = GetComponent<Rigidbody2D>();
        var playerInput = GetComponent<PlayerInput>();

        m_move.GetObservable<Vector2>(playerInput).Subscribe(v =>
        {
            _moveFloat = v.x; //UniRX * PlayerInput
        }).AddTo(this);

        #region InputManager‚ð—˜—p‚µ‚½
        //InputObservableUtility.OnAxisInput("Horizontal").Subscribe(v =>
        //{
        //    _moveFloat = v; 
        //}).AddTo(this);
        #endregion
    }
    //private void Update()
    //{
    //_moveFloat = Input.GetAxisRaw("Horizontal");
    //}
    private void FixedUpdate()
    {
        if (_rig.velocity.magnitude < m_maxSpeed)
            _rig.AddForce(new Vector2(_moveFloat * m_moveSpeed, 0));
    }
}