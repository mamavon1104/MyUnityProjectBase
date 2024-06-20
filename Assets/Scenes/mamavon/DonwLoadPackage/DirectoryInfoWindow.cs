using System.IO;
using UnityEditor;

public class DirectoryInfoWindow : EditorWindow
{
    private DefaultAsset _directoryAsset;

    [MenuItem("Window/DirectoryInfoWindow")]
    private static void Open()
    {
        GetWindow<DirectoryInfoWindow>();
    }

    void OnGUI()
    {
        // ディレクトリを指定させる
        _directoryAsset = (DefaultAsset)EditorGUILayout.ObjectField("ディレクトリを指定", _directoryAsset, typeof(DefaultAsset), true);
        if (_directoryAsset != null)
        {
            // DefaultAssetのパスを取得する
            string path = AssetDatabase.GetAssetPath(_directoryAsset);
            if (string.IsNullOrEmpty(path)) return;

            // ディレクトリでなければ、指定を解除する
            bool isDirectory = File.GetAttributes(path).HasFlag(FileAttributes.Directory);
            if (isDirectory == false)
            {
                _directoryAsset = null;
            }
        }
    }

    /// <summary>
    /// ディレクトリのパスを取得する
    /// </summary>
    public string GetDirectoryPath()
    {
        if (_directoryAsset == null) return null;

        return AssetDatabase.GetAssetPath(_directoryAsset);
    }
}
