using Mamavon.Data;
using Mamavon.Funcs;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mamavon.Code
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerJumpClass2D : MonoBehaviour
    {
        [SerializeField] InputSystemNameActionData m_jump;
        [SerializeField] Rigidbody2D _rig;
        [SerializeField] float m_jumpForce = 10f;

        private Transform _myT;
        private RaycastHit2D _hit;

        private void Start()
        {
            _myT = transform;
            _rig = GetComponent<Rigidbody2D>();
            var playerIndex = GetComponent<PlayerInput>().playerIndex.Debuglog($"{gameObject}ÇÃIndex : ");


            m_jump.GetObservable<Unit>(playerIndex).
                   Where(_ => true).
                   Subscribe(_ =>
                   {
                       //Jump(m_jumpForce);
                   }).AddTo(this);
        }

        public void Jump(float jumpForce)
        {
            "ÉWÉÉÉìÉvé¿çs".Debuglog();
            _rig.velocity = new Vector2(_rig.velocity.x, jumpForce);
        }
    }
}