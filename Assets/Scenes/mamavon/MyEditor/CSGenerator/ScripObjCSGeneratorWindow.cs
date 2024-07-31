#if UNITY_EDITOR
using Mamavon.Funcs;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Mamavon.MyEditor
{
    public class ScripObjCSGeneratorWindow : EditorWindow
    {
        private string className = "NewScriptableObjectCS";
        private string folderPath = "Assets/ScriptableObjects";
        private string templateContent;

        [MenuItem("Mamavon/My Editors/ScriptableObjC#")]
        public static void ShowWindow()
        {
            GetWindow<ScripObjCSGeneratorWindow>("ScriptableObjectC#生成ツール");
        }

        private void OnEnable()
        {
            // テンプレートの内容を初期化
            templateContent = @"using UniRx;
using UnityEngine;

/// <summary>
/// ScriptableObjectだよ。
/// </summary>
[CreateAssetMenu(fileName = ""{0}"", menuName = ""Mamavon Packs/ScriptableObject/{0}"")]
public class {0} : ScriptableObject
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
        }

        private Vector2 scrollPosition;

        private void OnGUI()
        {
            GUILayout.Label("ScriptableObjC#テンプレート生成くん", EditorStyles.boldLabel);

            className = EditorGUILayout.TextField("クラス名", className);

            #region 横並び開始
            EditorGUILayout.BeginHorizontal();
            folderPath = EditorGUILayout.TextField("保存フォルダパス", folderPath);
            if (GUILayout.Button("フォルダ選択", GUILayout.Width(100)))
            {
                folderPath = EditorExtension.OpenFolderPanel();
            }
            EditorGUILayout.EndHorizontal();
            #endregion 横並び開始

            #region テンプレート表示
            EditorGUILayout.LabelField("テンプレートはこちら！：");
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(350)); //スクロール領域を作成
            EditorGUILayout.LabelField(templateContent, EditorStyles.textArea); // テンプレート内容を表示（LabelFieldで編集不可）
            EditorGUILayout.EndScrollView();  //終了
            #endregion 

            if (GUILayout.Button("ScriptableObjectを生成"))
            {
                GenerateScriptableObject();
            }
        }

        private void GenerateScriptableObject()
        {
            // 既存のコードと同じ
            if (string.IsNullOrEmpty(className))
            {
                Debug.LogError("クラス名を入力してください。");
                return;
            }

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string scriptContent = string.Format(templateContent, className);
            string scriptPath = Path.Combine(folderPath, $"{className}.cs");
            File.WriteAllText(scriptPath, scriptContent);
            AssetDatabase.Refresh();
            Debug.Log($"新しいScriptableObjectクラス {className} を {folderPath} に生成しました。");
        }
    }
}
#endif