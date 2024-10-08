using Mamavon.Data;
using Mamavon.Useful;
using UniRx;
using UnityEngine;

public class InputGamePause : MonoBehaviour
{
    [SerializeField] InputSystemNameActionData pause;
    void Start()
    {
        pause.GetObservable<Unit>().Subscribe(_ =>
        {
            PauseGameManager.Instance.ChangePauseState();
        }).AddTo(this);
    }
}
