using Mamavon.Data;
using UniRx;
using UnityEngine;

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

        m_move.GetObservable<Vector2>().Subscribe(v =>
        {
            _moveFloat = v.x; //UniRX * PlayerInput
        }).AddTo(this);

        //InputObservableUtility.OnAxisInput("Horizontal").Subscribe(v =>
        //{
        //    _moveFloat = v; 
        //}).AddTo(this);
    }
    private void Update()
    {
        //_moveFloat = Input.GetAxisRaw("Horizontal");
    }
    private void FixedUpdate()
    {
        if (_rig.velocity.magnitude < m_maxSpeed)
            _rig.AddForce(new Vector2(_moveFloat * m_moveSpeed, 0));
    }
}