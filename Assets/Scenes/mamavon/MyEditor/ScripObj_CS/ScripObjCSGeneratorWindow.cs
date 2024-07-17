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
            GUILayout.Label("C#�e���v���[�g��������", EditorStyles.boldLabel);
            className = EditorGUILayout.TextField("�N���X��", className);

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

            if (GUILayout.Button("ScriptableObject�𐶐�"))
            {
                GenerateScriptableObject();
            }
        }

        private void GenerateScriptableObject()
        {
            if (string.IsNullOrEmpty(className))
            {
                Debug.LogError("�N���X������͂��Ă��������B");
                return;
            }

            // �t�H���_�����݂��Ȃ��ꍇ�͍쐬
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // �e���v���[�g�̓��e
            string template = @"using UniRx;
using UnityEngine;

/// <summary>
/// ScriptableObject����B
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

    [ContextMenu(""���s�e�X�g"")]
    private void TestFunc()
    {{

    }}
}}";

            // �N���X����}�����ăe���v���[�g������������
            string scriptContent = string.Format(template, className);

            // �X�N���v�g�t�@�C�����쐬
            string scriptPath = Path.Combine(folderPath, $"{className}.cs");
            File.WriteAllText(scriptPath, scriptContent);

            AssetDatabase.Refresh();
            Debug.Log($"�V����ScriptableObject�N���X {className} �� {folderPath} �ɐ������܂����B");
        }
    }
}