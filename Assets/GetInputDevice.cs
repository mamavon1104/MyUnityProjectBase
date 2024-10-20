using Mamavon.Funcs;
using UnityEngine;
using UnityEngine.InputSystem;

public class GetInputDevice : MonoBehaviour
{
    void Start()
    {
        DebugInputDevice();
    }
    [ContextMenu("�f�o�b�O����(InputDevice��)")]
    void DebugInputDevice()
    {
        var playerInput = GetComponent<PlayerInput>();
        if (!playerInput.user.valid) //����ŗL��������B 
        {
            return;
        }

        foreach (var item in playerInput.devices)
        {
            item.Debuglog(playerInput.playerIndex.ToString(), TextColor.Black);
        }
    }
}
