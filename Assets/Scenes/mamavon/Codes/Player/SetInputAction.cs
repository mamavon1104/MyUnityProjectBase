using Mamavon.Data;
using Mamavon.Funcs;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetInputAction : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] InputSystemNameActionData[] inputActionsDatas;
    [Header("InputAction")][SerializeField] InputActionReference[] inputActionArray;

    private void Awake()
    {
        if (playerInput == null)
        {
            playerInput = GetComponent<PlayerInput>();
        }
        CompareActions();
    }

    private void CompareActions()
    {
        inputActionArray = new InputActionReference[inputActionsDatas.Length];

        foreach (InputAction action in playerInput.actions)
        {
            InputActionReference reference = InputActionReference.Create(action);
            string inputActionStr = reference.ToString();
            for (int i = 0; i < inputActionsDatas.Length; i++)
            {
                string inputActionsDataStr = inputActionsDatas[i].actionReference.ToString();

                if (!String.Equals(inputActionStr, inputActionsDataStr))
                    continue;

                inputActionArray[i] = reference;
            }
        }
    }

    private void OnEnable()
    {
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