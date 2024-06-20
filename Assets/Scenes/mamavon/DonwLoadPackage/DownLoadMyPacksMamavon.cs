using UnityEngine;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

namespace Mamavon.DownLoad
{
    [CreateAssetMenu(menuName = "Mamavon Packs/DownLoad My Packs", fileName = "DownLoadMy.asset")]
    public class DownLoadMyPacksMamavon : ScriptableObject
    {
        /// <summary>
        /// アセットのパス
        /// </summary>
        public string asset_path;

#if UNITY_EDITOR
        // Method to open a folder panel and set the ASSET_PATH
        public void SetAssetPath()
        {
            string path = EditorUtility.OpenFolderPanel("Select Folder", "", "");
            if (!string.IsNullOrEmpty(path))
            {
                asset_path = path;
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();
            }
        }

        public void DownloadAllFiles(string parentDirectoryPath)
        {
            if (string.IsNullOrEmpty(asset_path))
            {
                Debug.LogError("アセットパスが空です。ダウンロードできません。");
                return;
            }

            // アセットパスからファイルを取得
            string[] files = Directory.GetFiles(asset_path);

            // ファイルをコピーして保存
            foreach (string file in files)
            {
                // ファイル名のみ取得
                string fileName = Path.GetFileName(file);

                // ファイルの保存先パス
                string destinationPath = Path.Combine(parentDirectoryPath, fileName);

                // ファイルをコピー
                File.Copy(file, destinationPath, true);
            }

            Debug.Log("すべてのファイルをダウンロードしました。");
        }
#endif
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(DownLoadMyPacksMamavon))]
    public class SamplePathScriptableObjectInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DownLoadMyPacksMamavon myScript = (DownLoadMyPacksMamavon)target;

            string assetPath = AssetDatabase.GetAssetPath(myScript);        // Assets/Scenes/mamavon/MyScriptableObjs/DownLoadMy.asset
            string directory = Path.GetDirectoryName(assetPath);            // Assets/Scenes/mamavon/MyScriptableObjs
            string parentDirectory = Directory.GetParent(directory).Name;   // MyscriptableObjsの親のmamavon.name
            string parentDirectoryPath = Path.Combine(directory, "..");     // Assets/Scenes/mamavon/-MyScriptableObjs-/.. 
                                                                            // ..が表すのは親のためmamavonまでのパスを取得。

            base.OnInspectorGUI();

            if (parentDirectory == "mamavon")
            {
                EditorGUILayout.HelpBox("親フォルダの名前は「mamavon」です。", MessageType.Info);
            }
            else
            {
                EditorGUILayout.HelpBox($"親フォルダの名前が「{parentDirectory}」です。\n 「mamavon」に設定しなおしてください。", MessageType.Error);
            }

            //このボタン押したら超絶素晴らしいパス選択画面へ移行
            if (GUILayout.Button("パスの設定をする"))
            {
                myScript.SetAssetPath();
            }

            if (parentDirectory != "mamavon")
                return;

            if (GUILayout.Button("mamavonベースから最新版をダウンロード"))
            {
                myScript.DownloadAllFiles(parentDirectoryPath);
            }
        }
    }
#endif
}