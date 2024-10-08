using Mamavon.Funcs;
using Mamavon.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SetEventCommands : MonoBehaviour
{
    [Serializable]
    public struct CommandEventPair
    {
        public string command;
        public UnityEvent myEvent;
    }

    [Header("Command,UnityEvent"), SerializeField] private CommandEventPair[] m_commandVsEvent;

    private Dictionary<string, UnityEvent> commandVsEventDict;

    private void Start()
    {
        commandVsEventDict = m_commandVsEvent.ToDictionary(p => p.command.ToLower(), p => p.myEvent);

        SecretCommandsManager.KeyCodeCommandEvent += OnCommandOfEvent;
    }
    private void OnDisable()
    {
        SecretCommandsManager.KeyCodeCommandEvent -= OnCommandOfEvent;
    }
    private void OnCommandOfEvent(string command)
    {
        if (commandVsEventDict.TryGetValue(command.ToLower(), out var unityEvent))
        {
            unityEvent?.Debuglog("UnityEvent‚ª”­‰Î‚³‚ê‚Ü‚µ‚½ : ").Invoke();
        }
    }
}
