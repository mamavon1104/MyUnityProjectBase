#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace Mamavon.MyEditor
{
    public class ScriptableObjectGenerator : EditorWindow   //ここにEditorを継承して
    {
        //これをすることでC#を設定できるらしい
        private MonoScript selectedScript;

        private int numberOfObjects = 10;
        private string objectName = "NewScriptableObject";
        private string folderPath = "Assets/ScriptableObjects";

        [MenuItem("Mamavon/My Editors/Generate ScriptableObjs")]
        public static void ShowWindow()                     //ここで表示させるという事もできるんだね
        {
            GetWindow<ScriptableObjectGenerator>("ScriptableObject生成ツール"); //タイトルみたいな事
        }

        private void OnGUI()                                //ここから下がUI表示
        {
            GUILayout.Label("ScriptableObject生成くん", EditorStyles.boldLabel);

            numberOfObjects = EditorGUILayout.IntField("生成するオブジェクト数", numberOfObjects);
            objectName = EditorGUILayout.TextField("基本オブジェクト名", objectName);

            #region 横並び開始
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
            #endregion

            /*
              UnityEngine.Object obj(selectedScript) → 
                新しい値が選択されるまで、このオブジェクトがフィールドに表示されるってばよ。

              System.Type objType(typeof(MonoScript))　→
            　  フィールドで許可されるオブジェクトの型、この型、または継承のオブジェクトのみがフィールドにいれられまし。

              bool allowSceneObjects(false)　→
                シーン内のオブジェクトを許可するかどうかを指定するbool。
                true: プロジェクト内のアセットとシーン内のオブジェクトの両方を許可します。
                false: プロジェクト内のアセットのみを許可します。

              as MonoScript: 戻り値をMonoScript型にキャストする

                らしい、最近のAIは優秀か？
             */
            selectedScript = EditorGUILayout.ObjectField("ScriptableObjectスクリプト", selectedScript, typeof(MonoScript), false)
                             as MonoScript;

            if (GUILayout.Button("ScriptableObjectsを生成"))
            {
                GenerateScriptableObjects();
            }
        }

        private void GenerateScriptableObjects()
        {
            if (selectedScript == null)
            {
                Debug.LogError("ScriptableObjectスクリプトを選択してくださいよ！");
                return;
            }

            Type scriptType = selectedScript.GetClass();
            if (scriptType == null || !scriptType.IsSubclassOf(typeof(ScriptableObject)))
            {
                Debug.LogError("選択されたスクリプトは有効なScriptableObjectではありません。");
                return;
            }

            // フォルダが存在しない場合は作成
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            for (int i = 0; i < numberOfObjects; i++)
            {
                // 選択されたScriptableObjectを作成
                ScriptableObject obj = CreateInstance(scriptType);

                // アセットとして保存　(folderPath/AAA_1 ,folderPath/AAA_2. のようになる。)
                string assetPath = $"{folderPath}/{objectName}_{i + 1}.asset";
                AssetDatabase.CreateAsset(obj, assetPath);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"{numberOfObjects}個の{scriptType.Name}型ScriptableObjectsを{folderPath}に生成したぜ、あとはよろしく");
        }
    }
}
#endif