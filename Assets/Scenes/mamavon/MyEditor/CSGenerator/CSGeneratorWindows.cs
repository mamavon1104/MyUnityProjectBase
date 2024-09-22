#if UNITY_EDITOR
using Mamavon.Funcs;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Mamavon.MyEditor
{
    # region C#のコードの保管庫、ここに追加していってくれ。
    interface StringsContainerBase
    {
        public string GetCode();
    }
    class ScriptableObjectCode : StringsContainerBase
    {
        public string GetCode()
        {
            return @"using UniRx;
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
    }
    class UnityWindowsCode : StringsContainerBase
    {
        public string GetCode()
        {
            return "aaa";
        }
    }
    class AnyExtensionsCode : StringsContainerBase
    {
        public string GetCode()
        {
            return @"using UnityEngine;
namespace Mamavon.Funcs
{{
    public static class {0}
    {{
        /// <summary>
        ///
        /// </summary>
        public static int Test(this int num, int num2)
        {{
            return 0;
        }}
    }}
}}";
        }
    }
    #endregion
    public class MultiTabScriptGeneratorWindow : EditorWindow
    {
        private string[] classNames = new string[] { "ScriptableObject_CS", "UnityWindow_CS", "AnyExtensions_CS" };
        private string[] tabNames = { "ScriptableObject", "UnityWindow", "AnyExtensions" };

        private StringsContainerBase[] containerBases = new StringsContainerBase[]{
            new ScriptableObjectCode(),
            new UnityWindowsCode(),
            new AnyExtensionsCode()
        };

        private string folderPath = "Assets/Scenes/mamavon";
        private string templateContents;
        private int selectedTab = 0;

        private Vector2 mainScrollPosition;
        private Vector2[] scrollPositions = new Vector2[3];

        [MenuItem("Mamavon/My Editors/C# Generator")]
        public static void ShowWindow()
        {
            GetWindow<MultiTabScriptGeneratorWindow>("C#生成ツールマン");
        }

        private void OnGUI()
        {
            mainScrollPosition = EditorGUILayout.BeginScrollView(mainScrollPosition);


            selectedTab = GUILayout.Toolbar(selectedTab, tabNames);

            GUILayout.Label($"{tabNames[selectedTab]}C#テンプレート生成くん", EditorStyles.boldLabel);
            classNames[selectedTab] = EditorGUILayout.TextField("クラス名", classNames[selectedTab]);

            EditorGUILayout.BeginHorizontal();
            folderPath = EditorGUILayout.TextField("保存フォルダパス", folderPath);
            if (GUILayout.Button("フォルダ選択", GUILayout.Width(100)))
            {
                folderPath = EditorExtension.OpenFolderPanel();
            }
            EditorGUILayout.EndHorizontal();

            templateContents = containerBases[selectedTab].GetCode();

            EditorGUILayout.LabelField("テンプレートはこちら ： ");
            scrollPositions[selectedTab] = EditorGUILayout.BeginScrollView(scrollPositions[selectedTab], GUILayout.Height(200));
            EditorGUILayout.LabelField(string.Format(templateContents, classNames[selectedTab]), EditorStyles.textArea);
            EditorGUILayout.EndScrollView();

            if (GUILayout.Button($"{tabNames[selectedTab]}クラスを生成"))
            {
                GenerateScript(selectedTab);
            }

            EditorGUILayout.EndScrollView();
        }

        private void GenerateScript(int tabIndex)
        {
            if (string.IsNullOrEmpty(classNames[tabIndex]))
            {
                Debug.LogError("クラス名を入力してください。");
                return;
            }

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string scriptContent = string.Format(templateContents, classNames[tabIndex]);
            string scriptPath = Path.Combine(folderPath, $"{classNames[tabIndex]}.cs");
            File.WriteAllText(scriptPath, scriptContent);
            AssetDatabase.Refresh();
            Debug.Log($"新しい{(tabIndex == 0 ? "ScriptableObject" : tabIndex == 1 ? "B" : "C")}クラス {classNames[tabIndex]} を {folderPath} に生成しました。");
        }
    }
}
#endif