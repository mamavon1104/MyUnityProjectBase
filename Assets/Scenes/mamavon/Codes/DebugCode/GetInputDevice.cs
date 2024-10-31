#if UNITY_EDITOR
using Mamavon.Funcs;
using UnityEngine;
using UnityEngine.InputSystem;

public class GetInputDevice : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    private void OnEnable()
    {
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