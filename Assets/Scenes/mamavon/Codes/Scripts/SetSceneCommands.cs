using Cysharp.Threading.Tasks;
using Mamavon.Funcs;
using Mamavon.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


[Serializable]
public class CommandScenePair
{
    public string command;
    public SceneObject sceneObj;
    public CommandScenePair(string command, SceneObject sceneObj)
    {
        this.command = command;
        this.sceneObj = sceneObj;
    }
}
public class SetSceneCommands : MonoBehaviour
{

    [Header("Command,SceneObj"), SerializeField]
    [FormerlySerializedAs("m_commandVsLevel")]
    private List<CommandScenePair> m_commandVsLevel;

    private Dictionary<string, SceneObject> commandVsLevelDict;

    private bool movingScene = false;
    private void Start()
    {
        m_commandVsLevel.Add(new SetReloadNowSceneCommand().GetReloadNowSceneCommand());

        commandVsLevelDict = m_commandVsLevel.ToDictionary(pair => pair.command.ToLower(), p => p.sceneObj);

        SecretCommandsManager.KeyCodeCommandEvent += OnCommandOfScene;
    }
    private void OnDisable()
    {
        SecretCommandsManager.KeyCodeCommandEvent -= OnCommandOfScene;
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

public class SetReloadNowSceneCommand
{
    private readonly static string reloadNowSceneCommand = "rns";

    public CommandScenePair GetReloadNowSceneCommand()
    {
        return new CommandScenePair(reloadNowSceneCommand, SceneManager.GetActiveScene().name);
    }
}