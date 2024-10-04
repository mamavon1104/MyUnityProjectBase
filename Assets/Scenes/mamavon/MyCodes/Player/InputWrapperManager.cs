using Mamavon.Useful;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine.InputSystem;

public class InputWrapperManager : SingletonMonoBehaviour<InputWrapperManager>
{
    private Dictionary<string, object> subjects = new Dictionary<string, object>();

    public IObservable<T> GetObservable<T>(string actionName)
    {
        if (!subjects.TryGetValue(actionName, out var subject))
        {
            subject = new Subject<T>();
            subjects[actionName] = subject;
        }
        return (subject as Subject<T>).AsObservable();
    }

    public void RegisterAction<T>(string actionName, InputAction action) where T : struct
    {
        action.Enable();
        action.performed += ctx =>
        {
            if (subjects.TryGetValue(actionName, out var subject))
            {
                (subject as Subject<T>).OnNext(ctx.ReadValue<T>());
            }
        };
    }
}

