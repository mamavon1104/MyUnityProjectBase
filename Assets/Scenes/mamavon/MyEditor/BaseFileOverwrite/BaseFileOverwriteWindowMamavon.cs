#if UNITY_EDITOR
using Mamavon.Funcs;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Mamavon.MyEditor
{
    public class BaseFileOverwriteWindowMamavon : EditorWindow
    {
        private string sourceFolderPath = "Assets/Scenes/mamavon";
        private readonly string targetFolderPath = "C:\\Users\\vanntann\\Desktop\\ProjectBase\\Assets\\Scenes\\mamavon";
        private float progress = 0f;
        private bool isCopying = false;

        [MenuItem("Mamavon /My Editors/BaseFile Overwrite")]
        public static void ShowWindow()
        {
            GetWindow<BaseFileOverwriteWindowMamavon>("フォルダコピーツール");
        }

        private void OnGUI()
        {
            GUILayout.Label("フォルダコピーツール", EditorStyles.boldLabel);
            sourceFolderPath = EditorGUILayout.TextField("ソースフォルダのパス", sourceFolderPath);

            EditorGUI.BeginDisabledGroup(isCopying);
            if (GUILayout.Button("フォルダをコピー"))
            {
                if (EditorUtility.DisplayDialog(
                                        "危険ですからね",
                                        "本当にファイルを上書きします？\nこの操作は元に戻せんけどね！",
                                        "はい", "しねえよ😡"))
                {
                    CopyFolderAsyncWrapper();
                }
            }

            EditorGUI.EndDisabledGroup();
            if (isCopying)
            {
                EditorGUI.ProgressBar(GUILayoutUtility.GetRect(18, 18, "TextField"), progress, "コピー中...");
            }
        }

        private void CopyFolderAsyncWrapper()
        {
            CopyFolderAsync();
            EditorUtility.DisplayDialog("実行終了", "ファイルの上書きが完了しましたよん", "OK...♠");
        }

        private async void CopyFolderAsync()
        {
            string fullSourcePath = Path.Combine(Application.dataPath,
                                                "..",
                                                sourceFolderPath).Debuglog(TextColor.Red);

            if (!Directory.Exists(fullSourcePath))
            {
                Debug.LogError("ソースフォルダが存在しません: " + fullSourcePath);
                return;
            }

            isCopying = true;
            progress = 0f;

            try
            {
                await Task.Run(() => CopyFolder(fullSourcePath, targetFolderPath));
                Debug.Log("フォルダのコピーが完了しました: " + targetFolderPath);
            }
            catch (IOException ex)
            {
                Debug.LogError("フォルダのコピーに失敗しました: " + ex.Message);
            }
            finally
            {
                isCopying = false;
                progress = 1f;
                Repaint();
            }
        }

        private void CopyFolder(string sourcePath, string targetPath)
        {
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            CopyAll(new DirectoryInfo(sourcePath), new DirectoryInfo(targetPath));
        }

        private void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            int totalItems = source.GetFiles().Length + source.GetDirectories().Length;
            int itemsCopied = 0;

            foreach (FileInfo file in source.GetFiles())
            {
                string targetFilePath = Path.Combine(target.FullName, file.Name);
                file.CopyTo(targetFilePath, true);
                itemsCopied++;
                UpdateProgress(itemsCopied, totalItems);
            }

            foreach (DirectoryInfo subdir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(subdir.Name);
                CopyAll(subdir, nextTargetSubDir);
                itemsCopied++;
                UpdateProgress(itemsCopied, totalItems);
            }
        }

        private void UpdateProgress(int itemsCopied, int totalItems)
        {
            progress = (float)itemsCopied / totalItems;
            UnityEditor.EditorApplication.delayCall += Repaint;
        }
    }
}
#endif