using Cysharp.Threading.Tasks;
using Mamavon.Funcs;
using Mamavon.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetSceneCommands : MonoBehaviour
{
    [Serializable]
    public struct CommandScenePair
    {
        public string command;
        public SceneObject scene;
    }

    [Header("Command,SceneObj"), SerializeField] private CommandScenePair[] m_commandVsLevel;

    private Dictionary<string, SceneObject> commandVsLevelDict;

    private bool movingScene = false;

    private void Start()
    {
        commandVsLevelDict = m_commandVsLevel.ToDictionary(p => p.command.ToLower(), p => p.scene);

        SecretCommands.KeyCodeCommandEvent += OnCommandOfScene;
    }
    private void OnDisable()
    {
        SecretCommands.KeyCodeCommandEvent -= OnCommandOfScene;
    }
    private void OnCommandOfScene(string command)
    {
        if (commandVsLevelDict.TryGetValue(command.ToLower(), out var scene))
        {
            if (movingScene == false)
                DoLoadScene(scene).Forget();
        }
    }
    private async UniTaskVoid DoLoadScene(SceneObject scene)
    {
        scene.Debuglog("Ç±ÇÃsceneÉçÅ[ÉhÇ≥ÇÍÇ‹Ç∑ :");
        movingScene = true;
        await LoadSceneMamavon.Instance.LoadScene(scene);
        movingScene = false;
    }
}