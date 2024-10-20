using Mamavon.Data;
using Mamavon.Funcs;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class SetInputAction : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] InputSystemNameActionData[] inputActionsDatas;
    [Header("InputAction")][SerializeField] InputActionReference[] inputActionArray;

    private void CompareActions()
    {
        if (playerInput == null) playerInput = GetComponent<PlayerInput>();
        inputActionArray = new InputActionReference[inputActionsDatas.Length];

        for (int i = 0; i < inputActionsDatas.Length; i++)
        {
            InputAction actionData = inputActionsDatas[i].actionReference.action;
            foreach (InputAction playerInputAction in playerInput.actions)
            {
                if (playerInputAction.name != actionData.name || playerInputAction.actionMap?.name != actionData.actionMap?.name)
                    continue;

                inputActionArray[i] = InputActionReference.Create(playerInput.actions[inputActionsDatas[i].actionReference.action.name]);
                break;
            }
        }

        //foreach (InputAction action in playerInput.actions)
        //{
        //    InputActionReference reference = InputActionReference.Create(action).Debuglog(playerInput.playerIndex.ToString(), TextColor.Blue);
        //    string inputActionStr = reference.ToString();
        //    for (int i = 0; i < inputActionsDatas.Length; i++)
        //    {
        //        string inputActionsDataStr = inputActionsDatas[i].actionReference.ToString().Debuglog(playerInput.playerIndex.ToString(), TextColor.Red);

        //        if (!String.Equals(inputActionStr, inputActionsDataStr))
        //            continue;

        //        inputActionArray[i] = reference;
        //    }
        //}
    }

    private void OnEnable()
    {
        CompareActions();
        "InputActionを有効化します".Debuglog(TextColor.Blue);
        for (int i = 0; i < inputActionArray.Length; i++)
        {
            inputActionsDatas[i].EnableAction(playerInput.playerIndex, inputActionArray[i]);
        }
    }
    private void OnDisable()
    {
        "InputActionを無効化します".Debuglog(TextColor.Red);
        for (int i = 0; i < inputActionArray.Length; i++)
        {
            inputActionsDatas[i].DisableAction(playerInput.playerIndex, inputActionArray[i]);
        }
    }
    private void OnDestroy()
    {
        "InputActionを破棄します".Debuglog(TextColor.Black);
        foreach (var data in inputActionsDatas)
        {
            InputWrapperManager.Instance.DestroyAction(playerInput.playerIndex, data.actionName);
        }
    }
}