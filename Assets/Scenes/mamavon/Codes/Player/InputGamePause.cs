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
        var playerIndex = GetComponent<PlayerInput>().playerIndex;
        pause.GetObservable<Unit>(playerIndex).Subscribe(_ =>
        {
            PauseGameManager.Instance.ChangePauseState();
        }).AddTo(this);
    }
}
