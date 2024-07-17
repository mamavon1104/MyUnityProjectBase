namespace Mamavon.MyEditor
{
    public class ScripObjCSGeneratorWindow : EditorWindow
    {
        private string className = "NewScriptableObject";
        private string folderPath = "Assets/ScriptableObjects";

        [MenuItem("Mamavon/ScriptableObjC#")]
        public static void ShowWindow()
        {
            GetWindow<ScripObjCSGeneratorWindow>("ScripObj CS Generator");
        }

        private void OnGUI()
        {
            GUILayout.Label("C#テンプレート生成くん", EditorStyles.boldLabel);
            className = EditorGUILayout.TextField("クラス名", className);

            EditorGUILayout.BeginHorizontal();
            folderPath = EditorGUILayout.TextField("保存フォルダパス", folderPath);
            if (GUILayout.Button("フォルダ選択", GUILayout.Width(100)))
            {
                string initialPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this)));
                string selectedPath = EditorUtility.OpenFolderPanel("保存フォルダを選択", initialPath, "");
                if (!string.IsNullOrEmpty(selectedPath))
                {
                    folderPath = FileUtil.GetProjectRelativePath(selectedPath);
                }
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("ScriptableObjectを生成"))
            {
                GenerateScriptableObject();
            }
        }

        private void GenerateScriptableObject()
        {
            if (string.IsNullOrEmpty(className))
            {
                Debug.LogError("クラス名を入力してください。");
                return;
            }

            // フォルダが存在しない場合は作成
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // テンプレートの内容
            string template = @"using UniRx;
using UnityEngine;

/// <summary>
/// ScriptableObjectだよ。
/// </summary>
[CreateAssetMenu(fileName = ""CardGameTotalCost"", menuName = ""Mamavon Packs/ScriptableObject/CardGameTotalCost"")]
public class CardGameTotalCost : ScriptableObject
{{
    public ReactiveProperty<int> testReactiveProperty;
    
    [SerializeField] int test;
    public int Test
    {{
        get {{ return test; }}
    }}


    public void TestFunc(int i)
    {{

    }}

    [ContextMenu(""実行テスト"")]
    private void TestFunc()
    {{

    }}
}}";

            // クラス名を挿入してテンプレートを完成させる
            string scriptContent = string.Format(template, className);

            // スクリプトファイルを作成
            string scriptPath = Path.Combine(folderPath, $"{className}.cs");
            File.WriteAllText(scriptPath, scriptContent);

            AssetDatabase.Refresh();
            Debug.Log($"新しいScriptableObjectクラス {className} を {folderPath} に生成しました。");
        }
    }
}