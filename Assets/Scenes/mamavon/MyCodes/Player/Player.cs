using Mamavon.Funcs;
using UniRx;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void Start()
    {
        var manager = InputWrapperManager.Instance;
        manager.GetObservable<Vector2>("move").Subscribe(v => Move(v));
        manager.GetObservable<float>("jump").Subscribe(_ => Jump());
    }

    private void Move(Vector2 v) { $"{v}".Debuglog("Move"); }
    private void Jump() { $"Jump".Debuglog(""); }
}