#if UNITY_EDITOR
using Cysharp.Threading.Tasks;
using Mamavon.Funcs;
using UnityEngine;
using UnityEngine.InputSystem;

public class GetInputDevice : MonoBehaviour
{
    private void Start()
    {
        foreach (var device in InputSystem.devices)
        {
            Debug.Log($"検出されたデバイス: {device.name}, タイプ: {device.GetType()}, パス: {device.path}");
        }
    }
    [SerializeField] PlayerInput playerInput;
    private async void OnEnable()
    {
        await UniTask.WaitUntil(() => playerInput.devices.Count.Debuglog() > 0);
        DebugInputDevice();
    }
    [ContextMenu("デバッグする(InputDeviceを)")]
    void DebugInputDevice()
    {
        if (!playerInput.user.valid) //これで有効か判定。 
        {
            "userが死んでおりまする".Debuglog(playerInput.playerIndex.ToString());
            return;
        }

        string totalStr = $"{playerInput.playerIndex}のコントローラーはこちら : \n";
        foreach (var item in playerInput.devices)
        {
            totalStr += $"{item}\n";
        }
        totalStr.Debuglog(playerInput.playerIndex.ToString(), TextColor.Red);
    }
}

#endif