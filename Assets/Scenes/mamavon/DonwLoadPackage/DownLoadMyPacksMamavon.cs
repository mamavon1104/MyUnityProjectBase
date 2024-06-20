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
        /// �A�Z�b�g�̃p�X
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
                Debug.LogError("�A�Z�b�g�p�X����ł��B�_�E�����[�h�ł��܂���B");
                return;
            }

            // �A�Z�b�g�p�X����t�@�C�����擾
            string[] files = Directory.GetFiles(asset_path);

            // �t�@�C�����R�s�[���ĕۑ�
            foreach (string file in files)
            {
                // �t�@�C�����̂ݎ擾
                string fileName = Path.GetFileName(file);

                // �t�@�C���̕ۑ���p�X
                string destinationPath = Path.Combine(parentDirectoryPath, fileName);

                // �t�@�C�����R�s�[
                File.Copy(file, destinationPath, true);
            }

            Debug.Log("���ׂẴt�@�C�����_�E�����[�h���܂����B");
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
            string parentDirectory = Directory.GetParent(directory).Name;   // MyscriptableObjs�̐e��mamavon.name
            string parentDirectoryPath = Path.Combine(directory, "..");     // Assets/Scenes/mamavon/-MyScriptableObjs-/.. 
                                                                            // ..���\���̂͐e�̂���mamavon�܂ł̃p�X���擾�B

            base.OnInspectorGUI();

            if (parentDirectory == "mamavon")
            {
                EditorGUILayout.HelpBox("�e�t�H���_�̖��O�́umamavon�v�ł��B", MessageType.Info);
            }
            else
            {
                EditorGUILayout.HelpBox($"�e�t�H���_�̖��O���u{parentDirectory}�v�ł��B\n �umamavon�v�ɐݒ肵�Ȃ����Ă��������B", MessageType.Error);
            }

            //���̃{�^���������璴��f���炵���p�X�I����ʂֈڍs
            if (GUILayout.Button("�p�X�̐ݒ������"))
            {
                myScript.SetAssetPath();
            }

            if (parentDirectory != "mamavon")
                return;

            if (GUILayout.Button("mamavon�x�[�X����ŐV�ł��_�E�����[�h"))
            {
                myScript.DownloadAllFiles(parentDirectoryPath);
            }
        }
    }
#endif
}