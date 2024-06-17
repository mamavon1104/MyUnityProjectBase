using Mamavon.Funcs;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerTest : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] private Transform cameraTrans;
    private Rigidbody rig;
    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        var move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        rig.AddForce(cameraTrans.CalculateMovementDirection(move) * _speed);
    }
}
