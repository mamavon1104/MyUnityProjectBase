using Mamavon.Data;
using Mamavon.Funcs;
using UniRx;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] InputSystemNameActionData move, jump;
    private void Start()
    {
        var manager = InputWrapperManager.Instance;
        move.GetObservable<Vector2>(0).Subscribe(v => Move(v));
        jump.GetObservable<bool>(0).Subscribe(_ => Jump());
    }

    private void Move(Vector2 v) { $"{v}".Debuglog("Move"); }
    private void Jump() { $"Jump".Debuglog(""); }
}