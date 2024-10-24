using Mamavon.Data;
using Mamavon.Funcs;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class SetInputAction : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] InputSystemNameActionData[] inputActionsDatas;
    [Header("InputAction")][SerializeField] InputAction[] inputActionArray;

    private void Awake()
    {
        CompareActions();
        "InputActionをAwakeで有効化します".Debuglog(TextColor.Blue);
        for (int i = 0; i < inputActionArray.Length; i++)
        {
            inputActionsDatas[i].EnableAction(playerInput, inputActionArray[i]);
        }
    }
    private void CompareActions()
    {
        if (playerInput == null) playerInput = GetComponent<PlayerInput>();
        inputActionArray = new InputAction[inputActionsDatas.Length];


        for (int i = 0; i < inputActionsDatas.Length; i++)
        {
            InputAction action = inputActionsDatas[i].actionReference.action;
            foreach (InputAction playerInputAction in playerInput.actions)
            {
                if (playerInputAction.name != action.name || playerInputAction.actionMap?.name != action.actionMap?.name)
                    continue;

                inputActionArray[i] = InputActionReference.Create(playerInput.actions[action.name]);
                break;
            }
        }
    }

    private void OnEnable()
    {
        CompareActions();
        "InputActionをEnabledで再有効化します".Debuglog(TextColor.Blue);
        for (int i = 0; i < inputActionArray.Length; i++)
        {
            inputActionsDatas[i].EnableAction(playerInput, inputActionArray[i]);
        }
    }
    private void OnDisable()
    {
        "InputActionを無効化します".Debuglog(TextColor.Red);
        for (int i = 0; i < inputActionArray.Length; i++)
        {
            inputActionsDatas[i].DisableAction(playerInput, inputActionArray[i]);
        }
    }
    private void OnDestroy()
    {
        "InputActionを破棄します".Debuglog(TextColor.Black);
        foreach (var act in inputActionArray)
        {
            InputWrapperManager.Instance.DestroyAction(playerInput.playerIndex, act.name);
        }
    }
}