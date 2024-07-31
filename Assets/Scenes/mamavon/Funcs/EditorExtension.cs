using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Mamavon.Funcs
{
    internal static class EditorExtension
    {
        /// <summary>
        /// フォルダを選択可能なパネルを表示します、
        /// </summary>
        /// <returns>
        /// [ProjectName/Assets～]で始まっていたらそのまま、\n
        /// 逆に、はじまっていなかったら null("") を返す。
        /// </returns>
        internal static string OpenFolderPanel()
        {
            string assetsPath = Application.dataPath;
            string selectedFolder = EditorUtility.OpenFolderPanel("Assetsフォルダ内のフォルダを選択", assetsPath, "");

            if (!string.IsNullOrEmpty(selectedFolder))
            {
                if (selectedFolder.StartsWith(assetsPath)) //selectFileが [ProjectName/Assets～]で始まっているかどうかを確認
                {
                    return "Assets" + selectedFolder.Substring(assetsPath.Length);
                }
                else
                {
                    EditorUtility.DisplayDialog("おいごら😡", "選択されたフォルダはAssetsフォルダ内ではないぞ😡", "すみません🥺");
                }
            }

            return "";
        }
        internal static void ClearDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                return;

            DirectoryInfo di = new DirectoryInfo(directoryPath);

            foreach (FileInfo file in di.GetFiles()) //全ファイル削除の後
            {
                file.Delete();
            }

            foreach (DirectoryInfo dir in di.GetDirectories()) //フォルダも消してmamavon以外破滅って訳
            {
                dir.Delete(true);
            }

            "mamavonフォルダを空にしました".Debuglog(TextColor.Cyan);
        }
    }
}
#endif