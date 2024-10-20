using Mamavon.Data;
using Mamavon.Useful;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputGamePause : MonoBehaviour
{
    [SerializeField] InputSystemNameActionData pause;
    void Start()
    {
        var playerInput = GetComponent<PlayerInput>();
        pause.GetObservable<Unit>(playerInput).Subscribe(_ =>
        {
            PauseGameManager.Instance.ChangePauseState();
        }).AddTo(this);
    }
}
