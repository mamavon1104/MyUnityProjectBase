#if UNITY_EDITOR
using Mamavon.Funcs;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Mamavon.MyEditor
{
    public class MoveToSceneMenuItem : EditorWindow
    {
        string[] sceneNames;
        static string[] scenePaths;

        [MenuItem("Mamavon/Move To Scene")]
        public static void ShowWindows()
        {
            GetWindow<MoveToSceneMenuItem>("�V�[�����[�h�N");
        }

        private void OnEnable()
        {
            LoadSceneData();
        }

        private void LoadSceneData()
        {
            var scenes = EditorBuildSettings.scenes
                .Where(s => s.enabled)
                .ToArray();

            if (scenes.Length == 0)
            {
                sceneNames = new string[0];
                scenePaths = new string[0];
                return;
            }

            scenePaths = scenes.Select(s => s.path).ToArray();
            sceneNames = scenes.Select(s => System.IO.Path.GetFileNameWithoutExtension(s.path)).ToArray();
        }

        private void OnGUI()
        {
            GUILayout.Label("�V�[�����[�h", EditorStyles.boldLabel);
            for (int i = 0; i < sceneNames.Length; i++)
            {
                string name = sceneNames[i];
                if (GUILayout.Button($"{name}�����[�h����I"))
                {
                    LoadScene(i);
                }
            }

            GUILayout.Space(10);
            if (GUILayout.Button("���̃G�f�B�^�[�����Z�b�g"))
            {
                ResetEditor();
            }
        }

        private void ResetEditor()
        {
            LoadSceneData();
            Repaint();
        }

        private static void LoadScene(int sceneIndex)
        {
            string scenePath = scenePaths[sceneIndex];

            if (System.IO.File.Exists(scenePath))
            {
                if (EditorSceneManager.GetActiveScene().isDirty)
                {
                    if (EditorUtility.DisplayDialog("�ۑ����܂���",
                        "���݂̕ύX��ۑ�����ꍇ�̓Z�[�u�������Ă���������",
                        "�Z�[�u", "���܂����I"))
                    {
                        EditorSceneManager.SaveOpenScenes();
                    }
                }

                EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
                $"{scenePath}".Debuglog("�ȉ��̃p�X�ɂ���V�[�������[�h���܂���", TextColor.Cyan);
            }
            else
            {
                Debug.LogError($"Scene file not found: {scenePath}");
            }
        }
    }
}
#endif
