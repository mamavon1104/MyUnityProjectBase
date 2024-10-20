#if UNITY_EDITOR
using Mamavon.Funcs;
using UnityEngine;
using UnityEngine.InputSystem;

public class GetInputDevice : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    void Start()
    {
        DebugInputDevice();
    }
    [ContextMenu("デバッグする(InputDeviceを)")]
    void DebugInputDevice()
    {
        //if (!playerInput.user.valid) //これで有効か判定。 
        //    return;

        string totalStr = $"{playerInput.playerIndex}のコントローラーはこちら : \n";
        foreach (UnityEngine.InputSystem.InputDevice item in playerInput.devices)
        {
            totalStr += $"{item}\n";
        }
        totalStr.Debuglog(playerInput.playerIndex.ToString(), TextColor.Black);
    }
}

#endif