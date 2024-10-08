using Mamavon.Data;
using Mamavon.Funcs;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetInputAction : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] InputSystemNameActionData[] inputActionsDatas;
    [Header("InputAction")][SerializeField] List<InputActionReference> inputActionList = new List<InputActionReference>();

    private void Start()
    {
        if (playerInput == null)
        {
            playerInput = GetComponent<PlayerInput>();
        }
        CompareActions();
    }

    private void CompareActions()
    {
        foreach (InputAction action in playerInput.actions)
        {
            InputActionReference reference = InputActionReference.Create(action);
            string inputActionStr = reference.ToString().Debuglog("PlayerInputのAction");
            for (int i = 0; i < inputActionsDatas.Length; i++)
            {
                string inputActionsDataStr = inputActionsDatas[i].actionReference.ToString().Debuglog("設定されたInputAction");

                if (!String.Equals(inputActionStr, inputActionsDataStr).Debuglog())
                    continue;

                inputActionList.Add(reference);
            }
        }
    }

    private void OnEnable()
    {
        "InputActionを有効化します".Debuglog(TextColor.Blue);
        for (int i = 0; i < inputActionList.Count; i++)
        {
            inputActionsDatas[i].EnableAction(inputActionList[i]);
        }
    }
    private void OnDisable()
    {
        "InputActionを無効化します".Debuglog(TextColor.Red);
        for (int i = 0; i < inputActionList.Count; i++)
        {
            inputActionsDatas[i].DisableAction(inputActionList[i]);
        }
    }
    private void OnDestroy()
    {
        "InputActionを破棄します".Debuglog(TextColor.Black);
        foreach (var data in inputActionsDatas)
        {
            InputWrapperManager.Instance.DestoryAction(data.actionName);
        }
    }
}

//using Mamavon.Funcs;
//using UnityEngine;
//using UnityEngine.InputSystem;
//public class SetInputAction : MonoBehaviour
//{
//    [SerializeField] PlayerInput playerInput;
//    [SerializeField] InputActionReference[] inputActionsDatas;
//    [Header("InputAction")][SerializeField] InputAction[] actionReference;

//    private void Start()
//    {
//        actionReference = new InputAction[inputActionsDatas.Length];
//        for (int i = 0; i < inputActionsDatas.Length; i++)
//        {
//            actionReference[i] = playerInput.actions[inputActionsDatas[i].name];
//        }
//    }

//    private void OnEnable()
//    {
//        "InputActionを有効化します".Debuglog(TextColor.Blue);
//        foreach (var data in inputActionsDatas)
//        {
//            data.EnableAction();
//        }
//    }
//    private void OnDisable()
//    {
//        "InputActionを無効化します".Debuglog(TextColor.Red);
//        foreach (var data in inputActionsDatas)
//        {
//            data.DisableAction();
//        }
//    }
//    private void OnDestroy()
//    {
//        "InputActionを破棄します".Debuglog(TextColor.Black);
//        foreach (var data in inputActionsDatas)
//        {
//            InputWrapperManager.Instance.DestoryAction(data.actionName);
//        }
//    }
//}