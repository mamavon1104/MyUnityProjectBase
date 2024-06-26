#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace Mamavon.DownLoad
{
    /// <summary>
    /// パッケージ選択用の ScriptableObjectだよ。
    /// </summary>

    public class DownLoadsMyPackEditor : EditorWindow
    {
        [MenuItem("Mamavon/MyPacksDownload")]
        public static void ShowWindow()
        {
            GetWindow(typeof(DownLoadsMyPackEditor));
        }
        private void OnEnable()
        {

        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("以下、選択されているパッケージ:");

            if (GUILayout.Button("選んだパッケージをインストールしちゃいますよ。"))
            {
                DownLoadMyPacks();
            }
        }
        private void DownLoadMyPacks()
        {
            const string MY_PACKS_NAME = "https://github.com/mamavon1104/MyUnityProjectBase.git";

            AddRequest addRequest = Client.Add(MY_PACKS_NAME);

            addRequest = Client.Add(MY_PACKS_NAME);

            Debug.Log($"{MY_PACKS_NAME}をインストールしま～す");
            if (addRequest != null && addRequest.IsCompleted)
            {
                if (addRequest.Status == StatusCode.Success)
                    Debug.Log("インストールが完了しました: " + addRequest.Result.packageId);
                else if (addRequest.Status >= StatusCode.Failure)
                    Debug.LogError("インストールに失敗しました: " + addRequest.Error.message);
            }
            AssetDatabase.Refresh();
        }
    }
}
#endif