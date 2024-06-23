using UnityEngine;

#if UNITY_EDITOR
using Mamavon.Funcs;
using System.IO;
using UnityEditor;
#endif

namespace Mamavon.DownLoad
{
    [CreateAssetMenu(menuName = "Mamavon Packs/DownLoad My Packs", fileName = "DownLoadMy.asset")]
    public class DownLoadMyPacksMamavon : ScriptableObject
    {
#if UNITY_EDITOR
        /// <summary>
        /// アセットのパス
        /// </summary>
        public string selectAssetPath;

        // Method to open a folder panel and set the ASSET_PATH
        public void SetAssetPath()
        {
            string path = EditorUtility.OpenFolderPanel("Select Folder", "", "");
            if (!string.IsNullOrEmpty(path))
            {
                selectAssetPath = path;
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();
            }
        }

        public void SetDefaultPath()
        {
            selectAssetPath = "C:\\Users\\vanntann\\Desktop\\ProjectBase\\Assets\\Scenes\\mamavon";
        }

        public void DownloadAllFiles(string parentDirectoryPath)
        {
            if (string.IsNullOrEmpty(selectAssetPath))
            {
                Debug.LogError("アセットパスが空です。ダウンロードできません。");
                return;
            }

            // アセットパスからファイルを取得
            string[] files = Directory.GetDirectories(selectAssetPath);

            string myFilesStr = "";
            // ファイルをコピーして保存
            foreach (string file in files)
            {
                // ファイル名のみ取得
                string fileName = Path.GetFileName(file);

                // ファイルの保存先パス
                string destinationPath = Path.Combine(parentDirectoryPath, fileName);

                myFilesStr = $"\n{destinationPath}\n{file}\n";

                CopyFolder(file, destinationPath);
            }

            AssetDatabase.Refresh();
            myFilesStr.Debuglog();
            "ダウンロードが完了しました".Debuglog(TextColor.Yellow);
        }
        private void CopyFolder(string sourceFolder, string destinationFolder)
        {
            // コピー先のフォルダが存在しない場合は作成する
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            // ファイルをコピーする
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(destinationFolder, fileName);
                File.Copy(file, destFile, true);
            }

            // サブフォルダを再帰的にコピーする
            string[] subfolders = Directory.GetDirectories(sourceFolder);
            foreach (string subfolder in subfolders)
            {
                string folderName = Path.GetFileName(subfolder);
                string destFolder = Path.Combine(destinationFolder, folderName);
                CopyFolder(subfolder, destFolder);
            }
        }
#endif
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(DownLoadMyPacksMamavon))]
    public class SamplePathScriptableObjectInspector : Editor
    {
        private DownLoadMyPacksMamavon myScript;
        private string parentDirectory;
        private string parentDirectoryPath;

        private void OnEnable()
        {
            myScript = (DownLoadMyPacksMamavon)target;
            UpdatePaths();
        }

        /// <summary>
        /// pathを取得する処理。
        /// </summary>
        private void UpdatePaths()
        {
            string thisObjAssetPath = AssetDatabase.GetAssetPath(myScript); // Assets/Scenes/mamavon/MyScriptableObjs/DownLoadMy.asset
            string directory = Path.GetDirectoryName(thisObjAssetPath);     // Assets/Scenes/mamavon/MyScriptableObjs
            parentDirectory = Directory.GetParent(directory).Name;          // MyscriptableObjsの親のmamavon
            parentDirectoryPath = Path.Combine(directory, "..");            // Assets/Scenes/mamavon/-MyScriptableObjs-/.. 
        }

        public override void OnInspectorGUI()
        {
            // ベースのInspector GUIを描画
            base.OnInspectorGUI();

            // ヘルプボックスを表示
            if (parentDirectory == "mamavon")
            {
                EditorGUILayout.HelpBox("親フォルダの名前は「mamavon」です。", MessageType.Info);
            }
            else
            {
                EditorGUILayout.HelpBox($"親フォルダの名前が「{parentDirectory}」です。\n「mamavon」に設定しなおしてください。", MessageType.Error);
            }

            // パスの設定ボタン
            if (GUILayout.Button("パスの設定をする"))
            {
                myScript.SetAssetPath();
                UpdatePaths(); // パスを更新
            }

            // デフォルトパス設定ボタン
            if (GUILayout.Button("デフォルトパスに設定する"))
            {
                myScript.SetDefaultPath();
                UpdatePaths(); // パスを更新
            }

            // 親ディレクトリが「mamavon」でない場合は早期リターン
            if (parentDirectory != "mamavon")
                return;

            // ダウンロードボタン
            if (GUILayout.Button("mamavonベースから最新版をダウンロード"))
            {
                myScript.DownloadAllFiles(parentDirectoryPath);
            }
        }
    }
#endif
}