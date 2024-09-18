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
            var scenes = EditorBuildSettings.scenes
                .Where(s => s.enabled)
                .ToArray();

            if (scenes.Length == 0)
            {
                return;
            }

            scenePaths = scenes.Select(s => s.path).ToArray();

            //これでファイルの名前が取得できるとの事。
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
        }

        private static void LoadScene(int sceneIndex)
        {
            string scenePath = scenePaths[sceneIndex];

            //ファイルからパスで指定してあげて読み込むとシーンが呼び出せる。
            if (System.IO.File.Exists(scenePath))
            {
                // isDirtyでシーンが変わっているかどうかを判別できる。
                // 頑張って作ったものをDirty呼ばわりするなよ、聞いていますか？
                if (EditorSceneManager.GetActiveScene().isDirty)
                {
                    if (EditorUtility.DisplayDialog("保存しますか",
                        "現在の変更を保存する場合はセーブを押してくださいな",
                        "セーブ", "しませんよ！"))
                    {
                        EditorSceneManager.SaveOpenScenes();
                    }
                }

                // Load the new scene
                EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
            }
            else
            {
                Debug.LogError($"Scene file not found: {scenePath}");
            }
        }
    }

}