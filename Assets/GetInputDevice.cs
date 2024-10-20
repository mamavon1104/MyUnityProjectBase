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
    [ContextMenu("�f�o�b�O����(InputDevice��)")]
    void DebugInputDevice()
    {
        //if (!playerInput.user.valid) //����ŗL��������B 
        //    return;

        string totalStr = $"{playerInput.playerIndex}�̃R���g���[���[�͂����� : \n";
        foreach (UnityEngine.InputSystem.InputDevice item in playerInput.devices)
        {
            totalStr += $"{item}\n";
        }
        totalStr.Debuglog(playerInput.playerIndex.ToString(), TextColor.Black);
    }
}

#endif