#if UNITY_EDITOR
using Mamavon.Funcs;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace Mamavon.MyEditor
{
    /// <summary>
    /// ユニティーパッケージに対応する列挙型、この世の核、世界を作り出すための動力、命かけても守れ。
    /// 追加する場合はDicテーブルにもリンクの追加を忘れずにね。
    /// </summary>
    [Flags]
    public enum UnityPackages
    {
        None = 0,
        UniRX = 1 << 0,
        UniTask = 1 << 1,
        VContainer = 1 << 2,

        Recorder = 1 << 3,
        Profiler = 1 << 4,
        ProBuilder = 1 << 5,
        Cinemachine = 1 << 6,
        InputSystem = 1 << 7,
        Navigation = 1 << 8,

        PostProcessing = 1 << 9,
        VisualEffectGraph = 1 << 10,
        UniversalRP = 1 << 11,
        ShaderGraph = 1 << 12,

        ECSGraphics = 1 << 13,
        EntityComponentSystem = 1 << 14,
        ECSPhysics = 1 << 15,

        CodePacks = UniRX | UniTask | VContainer,
        UnityToolPacks = Recorder | ProBuilder | Cinemachine | InputSystem | Profiler | Navigation,
        UnityVisualPacks = ShaderGraph | PostProcessing | VisualEffectGraph | UniversalRP,
        Others = EntityComponentSystem | ECSGraphics | ECSPhysics,

        Everything = CodePacks | UnityToolPacks | UnityVisualPacks | Others, //すべてのフラグを包含

        //Dotweenはgithubから無理で、AssetsStore
        //あと注意点として、○○ = A;だけだとビットの＆演算で駄目みたい。
        //Others = EntityComponentSystem;
    }

    /// <summary>
    /// インストールかアンインストールか、これを引数にすることで
    /// どちらかの処理が行われる。
    /// </summary>
    public enum PackageManagerAction
    {
        Install,
        Uninstall
    }

    public static class PackageDownloadClass
    {
        #region UnityPackagesに対応するテーブルを記載

        /// <summary>
        /// テーブルっす、Dictionaryとかでinstallに使う文字、unInstallに使う文字が格納されている。
        /// 呼び出すときはそっちで{ PackageManagerAction }を確認して、何方かの文字を取得してね。
        /// </summary>
        public static readonly Dictionary<UnityPackages, (string installStr, string uninstallStr)> packageDic = new Dictionary<UnityPackages, (string, string)>
        {
            // Unityのコード達
            { UnityPackages.UniRX, ("https://github.com/neuecc/UniRx.git?path=Assets/Plugins/UniRx/Scripts", "com.neuecc.unirx") },
            { UnityPackages.UniTask, ("https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask", "com.cysharp.unitask") },
            { UnityPackages.VContainer, ("https://github.com/hadashiA/VContainer.git?path=VContainer/Assets/VContainer#1.15.4", "jp.hadashikick.vcontainer") },

            // Unityのツール達
            { UnityPackages.Recorder, ("com.unity.recorder", "com.unity.recorder") },
            { UnityPackages.ProBuilder, ("com.unity.probuilder", "com.unity.probuilder") },
            { UnityPackages.Cinemachine, ("com.unity.cinemachine", "com.unity.cinemachine") },
            { UnityPackages.InputSystem, ("com.unity.inputsystem", "com.unity.inputsystem") },
            { UnityPackages.Profiler, ("com.unity.memoryprofiler", "com.unity.memoryprofiler") },
            { UnityPackages.Navigation, ("com.unity.ai.navigation", ".com.unity.ai.navigation") },
            
            // Unityのグラフィック達
            { UnityPackages.ShaderGraph, ("com.unity.shadergraph", "com.unity.shadergraph") },
            { UnityPackages.PostProcessing, ("com.unity.postprocessing", "com.unity.postprocessing") },
            { UnityPackages.VisualEffectGraph, ("com.unity.visualeffectgraph", "com.unity.visualeffectgraph") },
            { UnityPackages.UniversalRP, ("com.unity.render-pipelines.universal", "com.unity.render-pipelines.universal") },
            
            // Unityその他たち(ECSに変更する可能性あり。)
            { UnityPackages.ECSPhysics, ("com.unity.physics", "com.unity.physics") },
            { UnityPackages.EntityComponentSystem, ("com.unity.entities", "com.unity.entities") },
            { UnityPackages.ECSGraphics, ("com.unity.entities.graphics", "com.unity.entities.graphics") },
        };

        #endregion

        static Queue<string> packageQueue = new Queue<string>();
        static AddRequest addRequest;
        static RemoveRequest removeRequest;
        public static void DownloadPackage(PackageSelectionEditorWindow window, PackageManagerAction act)
        {
            if (window.SelectedPacks == UnityPackages.None)
            {
                "パッケージ選択されていません.".Debuglog(TextColor.Red);
                return;
            }

            string totalstr = "\n";
            foreach (UnityPackages packs in Enum.GetValues(typeof(UnityPackages)))
            {
                switch (packs)
                {
                    case UnityPackages.None:
                    case UnityPackages.CodePacks:
                    case UnityPackages.UnityToolPacks:
                    case UnityPackages.UnityVisualPacks:
                    case UnityPackages.Everything:
                    case UnityPackages.Others:  //ここにも新規追加しなくちゃいけないけど、まあ仕方なしという事にして下さいなｘ
                        continue;
                }

                if ((window.SelectedPacks & packs) == 0) //ビット演算で含まれているかどうか確認。
                    continue;

                if (packageDic.TryGetValue(packs, out (string insStr, string uninsStr) packageId))
                {
                    if (act == PackageManagerAction.Install)
                    {
                        totalstr += $"{packs} : {packageId.insStr} \n";
                        packageQueue.Enqueue(packageId.insStr);
                    }
                    else if (act == PackageManagerAction.Uninstall)
                    {
                        totalstr += $"{packs} : {packageId.uninsStr} \n";
                        packageQueue.Enqueue(packageId.uninsStr);
                    }
                }
                else
                {
                    Debug.LogError($"{packs}:そんな情報！？僕のデータにないぞ！\n" +
                       "選択されたpackageがテーブルに入ってないからアップデートしてください。");
                }
            }
            totalstr.Debuglog();

            ProcessPackageAction(act); //呼び出しを行い

            window.ResetPackages();
            AssetDatabase.Refresh();
        }

        private static void ProcessPackageAction(PackageManagerAction act)
        {
            if (packageQueue.Count == 0)
            {
                "すべてのパッケージが処理されました。".Debuglog();
                return;
            }

            string packageName = packageQueue.Dequeue();
            if (act == PackageManagerAction.Install)
            {
                $"{packageName}をインストールしま〜す".Debuglog();
                addRequest = Client.Add(packageName);
                EditorApplication.update += InstallProgress;
            }
            else if (act == PackageManagerAction.Uninstall)
            {
                $"{packageName}をアンインストールしま〜す".Debuglog();
                removeRequest = Client.Remove(packageName);
                EditorApplication.update += UnInstallProgress;
            }
        }

        public static void InstallProgress()
        {
            if (addRequest != null && addRequest.IsCompleted)
            {
                if (addRequest.Status == StatusCode.Success)
                    Debug.Log("インストールが完了しました: " + addRequest.Result.packageId);
                else if (addRequest.Status >= StatusCode.Failure)
                    Debug.LogError("インストールに失敗しました: " + addRequest.Error.message);

                EditorApplication.update -= InstallProgress;
                addRequest = null;

                ProcessPackageAction(PackageManagerAction.Install);
            }
        }

        public static void UnInstallProgress()
        {
            if (removeRequest != null && removeRequest.IsCompleted)
            {
                if (removeRequest.Status == StatusCode.Success)
                    Debug.Log("アンインストールが完了しました。");
                else if (removeRequest.Status >= StatusCode.Failure)
                    Debug.LogError("アンインストールに失敗しました: " + removeRequest.Error.message);

                EditorApplication.update -= UnInstallProgress;
                removeRequest = null;

                ProcessPackageAction(PackageManagerAction.Uninstall);
            }
        }
    }
}
#endif