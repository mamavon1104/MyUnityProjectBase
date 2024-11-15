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
namespace {0}
{{
    /// <summary>
    /// ScriptableObjectだよ。
    /// </summary>
    [CreateAssetMenu(fileName = ""{1}"", menuName = ""Mamavon Packs/ScriptableObject/{1}"")]
    public class {1} : ScriptableObject
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
namespace {0}
{{
    public static class {1}
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

    public enum AnyExtensionsNamespace
    {
        Mamavon_Funcs,
        Mamavon_Useful,
        Mamavon_Data,
        Mamavon_Code
    }

    public class MultiTabScriptGeneratorWindow : EditorWindow
    {
        private string[] classNames = new string[] { "ScriptableObject", "UnityWindow_CS", "AnyExtensions" };
        private string[] tabNames = { "ScriptableObject", "UnityWindow", "AnyExtensions" };

        private StringsContainerBase[] containerBases = new StringsContainerBase[]{
            new ScriptableObjectCode(),
            new UnityWindowsCode(),
            new AnyExtensionsCode()
        };

        private string folderPath = "Assets/Scenes/mamavon";
        private string templateContents;
        private int selectedTab = 0;
        private AnyExtensionsNamespace selectedNamespace = AnyExtensionsNamespace.Mamavon_Funcs;

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

            if (selectedTab != 1) // AnyExtensions tab
            {
                selectedNamespace = (AnyExtensionsNamespace)EditorGUILayout.EnumPopup("Namespace", selectedNamespace);
            }

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
            if (selectedTab != 1) // AnyExtensions tab
            {
                EditorGUILayout.LabelField(string.Format(templateContents, selectedNamespace.ToString().Replace("_", "."), classNames[selectedTab]), EditorStyles.textArea);
            }
            else
            {
                EditorGUILayout.LabelField(string.Format(templateContents, classNames[selectedTab]), EditorStyles.textArea);
            }
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

            string scriptContent;
            if (tabIndex != 1) // AnyExtensions tab
            {
                scriptContent = string.Format(templateContents, selectedNamespace.ToString().Replace("_", "."), classNames[tabIndex]);
            }
            else
            {
                scriptContent = string.Format(templateContents, classNames[tabIndex]);
            }

            string scriptPath = Path.Combine(folderPath, $"{classNames[tabIndex]}.cs");
            File.WriteAllText(scriptPath, scriptContent);
            AssetDatabase.Refresh();
            Debug.Log($"新しいクラス {classNames[tabIndex]} を {folderPath} に生成しました。");
        }
    }
}
#endif