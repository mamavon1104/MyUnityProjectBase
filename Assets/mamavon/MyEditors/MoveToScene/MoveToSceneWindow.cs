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
            GetWindow<MoveToSceneMenuItem>("シーンロード君");
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
            GUILayout.Label("シーンロード", EditorStyles.boldLabel);
            for (int i = 0; i < sceneNames.Length; i++)
            {
                string name = sceneNames[i];
                if (GUILayout.Button($"{name}をロードする！"))
                {
                    LoadScene(i);
                }
            }

            GUILayout.Space(10);
            if (GUILayout.Button("このエディターをリセット"))
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
                    if (EditorUtility.DisplayDialog("保存しますか",
                        "現在の変更を保存する場合はセーブを押してくださいな",
                        "セーブ", "しませんよ！"))
                    {
                        EditorSceneManager.SaveOpenScenes();
                    }
                }

                EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
                $"{scenePath}".Debuglog("以下のパスにあるシーンをロードしました", TextColor.Cyan);
            }
            else
            {
                Debug.LogError($"Scene file not found: {scenePath}");
            }
        }
    }
}
#endif
