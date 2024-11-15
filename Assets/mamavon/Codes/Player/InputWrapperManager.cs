using Cysharp.Threading.Tasks;
using Mamavon.Data;
using Mamavon.Funcs;
using Mamavon.Useful;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine.InputSystem;

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
            //(playerNumber, actionName, subject).Debuglog(TextColor.GreenYellow);
        }

        return (subject as Subject<T>).
            AsObservable();
    }

    public void EnableAction<T>(PlayerInput playerIndex, InputAction action, InputSystemNameActionData data) where T : struct
    {
        DisableAction<T>(playerIndex, action, data);

        action.Enable();
        action.performed += ctx => InvokeSubject<T>(playerIndex, data.actionReference.name, ctx, false);

        if (data.triggerOnRelease)
            action.canceled += ctx => InvokeSubject<T>(playerIndex, data.actionReference.name, ctx, true);
    }

    public void DisableAction<T>(PlayerInput playerInput, InputAction action, InputSystemNameActionData data) where T : struct
    {
        if (playerSubjects.TryGetValue(playerInput.playerIndex, out var subjects) && subjects.ContainsKey(data.actionReference.name))
        {
            action.performed -= ctx => InvokeSubject<T>(playerInput, data.actionReference.name, ctx, false);
            if (data.triggerOnRelease)
                action.canceled -= ctx => InvokeSubject<T>(playerInput, data.actionReference.name, ctx, true);
        }
        action.Disable();
    }

    public void DestroyAction(int playerNumber, string actionName)
    {
        if (playerSubjects.TryGetValue(playerNumber, out var subjects))
        {
            if (subjects.TryGetValue(actionName, out var subject))
            {
                // アクション名に関連するSubjectを破棄
                (subject as IDisposable)?.Dispose();
                subjects.Remove(actionName);
            }

            // プレイヤーの全アクションが削除された場合、プレイヤーエントリも削除
            if (subjects.Count == 0)
            {
                playerSubjects.Remove(playerNumber);
            }
        }
    }

    private async void InvokeSubject<T>(PlayerInput playerInput, string actionName,
                                  InputAction.CallbackContext ctx, bool isReleased) where T : struct
    {
        if (isReleased)
            await UniTask.WaitForSeconds(0.05f); //キャンセルのタイミングの場合0.05秒程送らせることで確実に0初期化

        if (playerSubjects.TryGetValue(playerInput.playerIndex, out var subjects) && subjects.TryGetValue(actionName, out var subject))
        {
            if (subject is Subject<Unit> unitSubject)
                unitSubject.OnNext(Unit.Default);
            else if (subject is Subject<bool> boolSubject)
                boolSubject.OnNext(isReleased ? false : ctx.ReadValueAsButton());
            else if (subject is Subject<T> valueSubject)
                valueSubject.OnNext(isReleased ? default(T).Debuglog(TextColor.Aqua) : ctx.ReadValue<T>());
        }
    }

}