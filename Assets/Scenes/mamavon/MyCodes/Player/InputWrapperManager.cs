using Mamavon.Useful;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
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

public class SetInputAction : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;

    private void OnEnable()
    {
        var manager = InputWrapperManager.Instance;
        manager.RegisterAction<Vector2>("move", inputActions.FindAction("move"));
        manager.RegisterAction<float>("jump", inputActions.FindAction("jump"));
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}

public class Player : MonoBehaviour
{
    private void Start()
    {
        var manager = InputWrapperManager.Instance;
        manager.GetObservable<Vector2>("move").Subscribe(Move);
        manager.GetObservable<float>("jump").Subscribe(_ => Jump());
    }

    private void Move(Vector2 v) { /*処理*/ }
    private void Jump() { /*処理*/ }
}