#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace Mamavon.MyEditor
{
    public class ScriptableObjectGenerator : EditorWindow   //������Editor���p������
    {
        //��������邱�Ƃ�C#��ݒ�ł���炵��
        private MonoScript selectedScript;

        private int numberOfObjects = 10;
        private string objectName = "NewScriptableObject";
        private string folderPath = "Assets/ScriptableObjects";

        [MenuItem("Mamavon/My Editors/Generate ScriptableObjs")]
        public static void ShowWindow()                     //�����ŕ\��������Ƃ��������ł���񂾂�
        {
            GetWindow<ScriptableObjectGenerator>("ScriptableObject�����c�[��"); //�^�C�g���݂����Ȏ�
        }

        private void OnGUI()                                //�������牺��UI�\��
        {
            GUILayout.Label("ScriptableObject��������", EditorStyles.boldLabel);

            numberOfObjects = EditorGUILayout.IntField("��������I�u�W�F�N�g��", numberOfObjects);
            objectName = EditorGUILayout.TextField("��{�I�u�W�F�N�g��", objectName);

            #region �����ъJ�n
            EditorGUILayout.BeginHorizontal();
            folderPath = EditorGUILayout.TextField("�ۑ��t�H���_�p�X", folderPath);
            if (GUILayout.Button("�t�H���_�I��", GUILayout.Width(100)))
            {
                string initialPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this)));
                string selectedPath = EditorUtility.OpenFolderPanel("�ۑ��t�H���_��I��", initialPath, "");
                if (!string.IsNullOrEmpty(selectedPath))
                {
                    folderPath = FileUtil.GetProjectRelativePath(selectedPath);
                }
            }
            EditorGUILayout.EndHorizontal();
            #endregion

            /*
              UnityEngine.Object obj(selectedScript) �� 
                �V�����l���I�������܂ŁA���̃I�u�W�F�N�g���t�B�[���h�ɕ\���������Ă΂�B

              System.Type objType(typeof(MonoScript))�@��
            �@  �t�B�[���h�ŋ������I�u�W�F�N�g�̌^�A���̌^�A�܂��͌p���̃I�u�W�F�N�g�݂̂��t�B�[���h�ɂ�����܂��B

              bool allowSceneObjects(false)�@��
                �V�[�����̃I�u�W�F�N�g�������邩�ǂ������w�肷��bool�B
                true: �v���W�F�N�g���̃A�Z�b�g�ƃV�[�����̃I�u�W�F�N�g�̗����������܂��B
                false: �v���W�F�N�g���̃A�Z�b�g�݂̂������܂��B

              as MonoScript: �߂�l��MonoScript�^�ɃL���X�g����

                �炵���A�ŋ߂�AI�͗D�G���H
             */
            selectedScript = EditorGUILayout.ObjectField("ScriptableObject�X�N���v�g", selectedScript, typeof(MonoScript), false)
                             as MonoScript;

            if (GUILayout.Button("ScriptableObjects�𐶐�"))
            {
                GenerateScriptableObjects();
            }
        }

        private void GenerateScriptableObjects()
        {
            if (selectedScript == null)
            {
                Debug.LogError("ScriptableObject�X�N���v�g��I�����Ă���������I");
                return;
            }

            Type scriptType = selectedScript.GetClass();
            if (scriptType == null || !scriptType.IsSubclassOf(typeof(ScriptableObject)))
            {
                Debug.LogError("�I�����ꂽ�X�N���v�g�͗L����ScriptableObject�ł͂���܂���B");
                return;
            }

            // �t�H���_�����݂��Ȃ��ꍇ�͍쐬
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            for (int i = 0; i < numberOfObjects; i++)
            {
                // �I�����ꂽScriptableObject���쐬
                ScriptableObject obj = CreateInstance(scriptType);

                // �A�Z�b�g�Ƃ��ĕۑ��@(folderPath/AAA_1 ,folderPath/AAA_2. �̂悤�ɂȂ�B)
                string assetPath = $"{folderPath}/{objectName}_{i + 1}.asset";
                AssetDatabase.CreateAsset(obj, assetPath);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"{numberOfObjects}��{scriptType.Name}�^ScriptableObjects��{folderPath}�ɐ����������A���Ƃ͂�낵��");
        }
    }
}
#endif