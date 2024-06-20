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
        // �f�B���N�g�����w�肳����
        _directoryAsset = (DefaultAsset)EditorGUILayout.ObjectField("�f�B���N�g�����w��", _directoryAsset, typeof(DefaultAsset), true);
        if (_directoryAsset != null)
        {
            // DefaultAsset�̃p�X���擾����
            string path = AssetDatabase.GetAssetPath(_directoryAsset);
            if (string.IsNullOrEmpty(path)) return;

            // �f�B���N�g���łȂ���΁A�w�����������
            bool isDirectory = File.GetAttributes(path).HasFlag(FileAttributes.Directory);
            if (isDirectory == false)
            {
                _directoryAsset = null;
            }
        }
    }

    /// <summary>
    /// �f�B���N�g���̃p�X���擾����
    /// </summary>
    public string GetDirectoryPath()
    {
        if (_directoryAsset == null) return null;

        return AssetDatabase.GetAssetPath(_directoryAsset);
    }
}
