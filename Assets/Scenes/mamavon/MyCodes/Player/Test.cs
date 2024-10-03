using Mamavon.Funcs;
using Mamavon.Useful;
using UniRx;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InputObservableUtility.OnKeyDown(KeyCode.Q).Subscribe(_ =>
        {
            PauseGameManager.Instance.Pause();
        }).AddTo(this);
        InputObservableUtility.OnKeyDown(KeyCode.E).Subscribe(_ =>
        {
            PauseGameManager.Instance.Resume();
        }).AddTo(this);
    }
}
