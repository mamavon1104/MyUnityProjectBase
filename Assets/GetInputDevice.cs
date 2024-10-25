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
    [ContextMenu("�f�o�b�O����(InputDevice��)")]
    void DebugInputDevice()
    {
        if (!playerInput.user.valid) //����ŗL��������B 
        {
            "user������ł���܂���".Debuglog(playerInput.playerIndex.ToString());
            return;
        }

        string totalStr = $"{playerInput.playerIndex}�̃R���g���[���[�͂����� : \n";
        foreach (var item in playerInput.devices)
        {
            totalStr += $"{item}\n";
        }
        totalStr.Debuglog(playerInput.playerIndex.ToString(), TextColor.Red);
    }
}

#endif