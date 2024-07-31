using Mamavon.Funcs;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Mamavon.DownLoad
{
    public class DownLoadMamavonPacksWindow : EditorWindow
    {
        /// <summary>
        /// コピー元のパスもうここで固定、
        /// キャンプ地はここにするって訳
        /// </summary>
        private readonly static string
        selectAssetPath = "C:\\Users\\vanntann\\Desktop\\ProjectBase\\Assets\\Scenes\\mamavon";

        /// <summary>
        /// Assetsから始まる自分のmamavonPathです。
        /// </summary>
        private string myMamavonPath;

        private void OnEnable()
        {
            SetMyPath();
        }

        private void SetMyPath()
        {
            string projectPath = Application.dataPath;
            string relativePath = "Scenes/mamavon";
            myMamavonPath = Path.Combine(projectPath, relativePath);
        }

        [MenuItem("Mamavon/My Editors/DownLoad Mamavon Packs")]
        public static void ShowWindow()
        {
            GetWindow<DownLoadMamavonPacksWindow>("Mamamvonの為だけのダウンローダー");
        }

        private void OnGUI()
        {
            GUILayout.Label("MamavonPacksをダウンロード", EditorStyles.boldLabel);

            if (GUILayout.Button("mamavonベースから最新版をダウンロード"))
            {
                if (EditorUtility.DisplayDialog(
                                        "危険ですからね🤔",
                                        "本当にファイルダウンロードしちゃいます？\nデータが消える恐れあるよ？バックアップ取った？",
                                        "しようぜ🥰", "しねえよ😡"))
                {
                    DownloadAllFiles();
                    EditorUtility.DisplayDialog("実行終了", "ファイルのコピーが完了したよ！", "仕事を始めます😿");
                }
            }
            EditorGUI.EndDisabledGroup();
        }


        private void DownloadAllFiles()
        {
            // まず自分のmamavonフォルダを空にする
            EditorExtension.ClearDirectory(myMamavonPath);

            string[] files = Directory.GetDirectories(selectAssetPath);
            string myFilesStr = "";
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string destinationPath = Path.Combine(myMamavonPath, fileName);
                myFilesStr += $"\n{destinationPath}\n{file}\n";
                CopyFolder(file, destinationPath);
            }
            AssetDatabase.Refresh();
            myFilesStr.Debuglog();
        }

        private void CopyFolder(string sourceFolder, string destinationFolder)
        {
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(destinationFolder, fileName);
                File.Copy(file, destFile, true);
            }

            string[] subfolders = Directory.GetDirectories(sourceFolder);
            foreach (string subfolder in subfolders)
            {
                string folderName = Path.GetFileName(subfolder);
                string destFolder = Path.Combine(destinationFolder, folderName);
                CopyFolder(subfolder, destFolder);
            }
        }
    }
}