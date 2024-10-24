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
            Debug.Log($"���o���ꂽ�f�o�C�X: {device.name}, �^�C�v: {device.GetType()}, �p�X: {device.path}");
        }
    }
    [SerializeField] PlayerInput playerInput;
    private async void OnEnable()
    {
        await UniTask.WaitUntil(() => playerInput.devices.Count.Debuglog() > 0);
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