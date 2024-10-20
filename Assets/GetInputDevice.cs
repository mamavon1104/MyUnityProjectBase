using Mamavon.Funcs;
using UnityEngine;
using UnityEngine.InputSystem;

public class GetInputDevice : MonoBehaviour
{
    void Start()
    {
        DebugInputDevice();
    }
    [ContextMenu("デバッグする(InputDeviceを)")]
    void DebugInputDevice()
    {
        var playerInput = GetComponent<PlayerInput>();
        if (!playerInput.user.valid) //これで有効か判定。 
        {
            return;
        }

        foreach (var item in playerInput.devices)
        {
            item.Debuglog(playerInput.playerIndex.ToString(), TextColor.Black);
        }
    }
}
