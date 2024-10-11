using Mamavon.Useful;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine.InputSystem;

//public class InputWrapperManager : SingletonMonoBehaviour<InputWrapperManager>
//{
//    private Dictionary<string, object> subjects = new Dictionary<string, object>();

//    public IObservable<T> GetObservable<T>(string actionName)
//    {
//        if (!subjects.TryGetValue(actionName, out var subject))
//        {
//            subject = new Subject<T>();
//            subjects[actionName] = subject;
//        }
//        return (subject as Subject<T>).AsObservable();
//    }

//    public void EnableAction<T>(string actionName, InputAction action, bool invokeOnRelease) where T : struct
//    {
//        // 既存のアクションがあれば、一旦無効化
//        DisableAction<T>(actionName, action, invokeOnRelease);

//        // 新しいアクションを登録
//        action.Enable();

//        action.performed += ctx => InvokeSubject<T>(actionName, ctx);
//        if (invokeOnRelease)
//            action.canceled += ctx => InvokeSubject<T>(actionName, ctx);
//    }

//    public void DisableAction<T>(string actionName, InputAction action, bool invokeOnRelease) where T : struct
//    {
//        if (subjects.TryGetValue(actionName, out var subject))
//        {
//            action.performed -= ctx => InvokeSubject<T>(actionName, ctx);
//            if (invokeOnRelease)
//                action.canceled -= ctx => InvokeSubject<T>(actionName, ctx);
//        }
//        action.Disable();
//    }

//    public void DestoryAction(string actionName)
//    {
//        if (subjects.TryGetValue(actionName, out var subject))
//            subjects.Remove(actionName);
//    }

//    private void InvokeSubject<T>(string actionName, InputAction.CallbackContext ctx) where T : struct
//    {
//        if (subjects.TryGetValue(actionName, out var subject))
//        {
//            if (subject is Subject<Unit> unitSubject)
//                unitSubject.OnNext(Unit.Default);
//            else if (subject is Subject<bool> boolSubject)
//                boolSubject.OnNext(ctx.ReadValueAsButton());
//            else
//                (subject as Subject<T>)?.OnNext(ctx.ReadValue<T>());
//        }
//    }
//}

public class InputWrapperManager : SingletonMonoBehaviour<InputWrapperManager>
{
    private Dictionary<int, Dictionary<string, object>> playerSubjects = new Dictionary<int, Dictionary<string, object>>();

    public IObservable<T> GetObservable<T>(int playerNumber, string actionName)
    {
        if (!playerSubjects.TryGetValue(playerNumber, out var subjects))
        {
            subjects = new Dictionary<string, object>();
            playerSubjects[playerNumber] = subjects;
        }

        if (!subjects.TryGetValue(actionName, out var subject))
        {
            subject = new Subject<T>();
            subjects[actionName] = subject;
        }
        return (subject as Subject<T>).AsObservable();
    }

    public void EnableAction<T>(int playerNumber, string actionName, InputAction action, bool invokeOnRelease) where T : struct
    {
        DisableAction<T>(playerNumber, actionName, action, invokeOnRelease);

        action.Enable();
        action.performed += ctx => InvokeSubject<T>(playerNumber, actionName, ctx);

        if (invokeOnRelease)
            action.canceled += ctx => InvokeSubject<T>(playerNumber, actionName, ctx);
    }

    public void DisableAction<T>(int playerNumber, string actionName, InputAction action, bool invokeOnRelease) where T : struct
    {
        if (playerSubjects.TryGetValue(playerNumber, out var subjects) && subjects.ContainsKey(actionName))
        {
            action.performed -= ctx => InvokeSubject<T>(playerNumber, actionName, ctx);
            if (invokeOnRelease)
                action.canceled -= ctx => InvokeSubject<T>(playerNumber, actionName, ctx);
        }
        action.Disable();
    }

    public void DestroyAction(int playerNumber, string actionName)
    {
        if (playerSubjects.TryGetValue(playerNumber, out var subjects))
        {
            subjects.Remove(actionName);
        }
    }

    private void InvokeSubject<T>(int playerNumber, string actionName, InputAction.CallbackContext ctx) where T : struct
    {
        if (playerSubjects.TryGetValue(playerNumber, out var subjects) && subjects.TryGetValue(actionName, out var subject))
        {
            if (subject is Subject<Unit> unitSubject)
                unitSubject.OnNext(Unit.Default);
            else if (subject is Subject<bool> boolSubject)
                boolSubject.OnNext(ctx.ReadValueAsButton());
            else
                (subject as Subject<T>)?.OnNext(ctx.ReadValue<T>());
        }
    }
}