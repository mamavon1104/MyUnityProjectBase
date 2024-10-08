//using UnityEngine;
//#if UNITY_EDITOR
//using UnityEditor;
//#endif

//namespace Mamavon.Setting
//{
//#if UNITY_EDITOR
//    /// <summary>
//    /// パッケージ選択用の ScriptableObjectだよ。
//    /// </summary>
//    [CreateAssetMenu(fileName = "PackageSelection", menuName = "Mamavon Packs/Packages Select", order = 1)]
//    public class PackageSelection : ScriptableObject
//    {
//        public UnityPackages selectedPacks;
//        public void SetEverythingPackages() => selectedPacks = UnityPackages.Everything;
//        public void ResetPackages() => selectedPacks = UnityPackages.None;
//    }

//    [CustomEditor(typeof(PackageSelection))]  //typeofってrequireComponentと同じ感じで使えるみたいね
//    public class PackageSelectionEditor : Editor
//    {
//        private UnityPackages[] allPackages;

//        private void OnEnable()
//        {
//            // 一度だけ Enum の全値を取得してキャッシュ
//            allPackages = (UnityPackages[])System.Enum.GetValues(typeof(UnityPackages));
//        }

//        public override void OnInspectorGUI()
//        {
//            PackageSelection myScript = (PackageSelection)target;
//            DrawDefaultInspector();

//            GUILayout.Space(10);
//            EditorGUILayout.LabelField("以下、選択されているパッケージ:");
//            GUILayout.Space(10);

//            DisplayPackageGroup(myScript, "Code Packs", UnityPackages.CodePacks);
//            DisplayPackageGroup(myScript, "Unity Tool Packs", UnityPackages.UnityToolPacks);
//            DisplayPackageGroup(myScript, "Unity Visual Packs", UnityPackages.UnityVisualPacks);
//            DisplayPackageGroup(myScript, "Others", UnityPackages.Others);

//            GUILayout.Space(20);

//            if (GUILayout.Button("選んだパッケージをインストールしちゃいますよ。"))
//            {
//                PackageDownloadClass.DownloadPackage(myScript, PackageManagerAction.Install);
//            }
//            if (GUILayout.Button("選んだパッケージをアンインストールしちゃいますよ。"))
//            {
//                PackageDownloadClass.DownloadPackage(myScript, PackageManagerAction.Uninstall);
//            }

//            GUILayout.Space(10);

//            if (GUILayout.Button("全て選択しましょう。"))
//            {
//                myScript.SetEverythingPackages();
//            }
//            if (GUILayout.Button("リセットします。"))
//            {
//                myScript.ResetPackages();
//            }
//        }

//        /// <summary>
//        /// チェックボックスとかの描画を行っています
//        /// </summary>
//        /// <param name="myScript">そのScriptableObject</param>
//        /// <param name="groupName">グループの名前、例えばCodePacks(rx,task,container)とか</param>
//        /// <param name="groupFlag">nameと一致するようなUnityPackagesの奴、要素が加算(A | B)されているものがここに入る</param>
//        private void DisplayPackageGroup(PackageSelection myScript, string groupName, UnityPackages groupFlag)
//        {
//            bool isGroupSelected = (myScript.selectedPacks & groupFlag) == groupFlag;

//            EditorGUI.BeginChangeCheck();
//            GUILayout.Space(10);
//            isGroupSelected = EditorGUILayout.Toggle(groupName, isGroupSelected);

//            if (EditorGUI.EndChangeCheck())
//            {
//                if (isGroupSelected)
//                {
//                    myScript.selectedPacks |= groupFlag;
//                }
//                else
//                {
//                    myScript.selectedPacks &= ~groupFlag;
//                }
//            }

//            foreach (UnityPackages package in allPackages)
//            {
//                if (package == UnityPackages.None || package == UnityPackages.Everything || package == groupFlag)
//                    continue;

//                if ((groupFlag & package) != package)
//                    continue;

//                GUILayout.BeginHorizontal();
//                GUILayout.Space(20);

//                bool isSelected = (myScript.selectedPacks & package) == package;

//                EditorGUI.BeginChangeCheck();
//                isSelected = EditorGUILayout.Toggle(package.ToString(), isSelected);

//                if (EditorGUI.EndChangeCheck())
//                {
//                    if (isSelected)
//                    {
//                        myScript.selectedPacks |= package;
//                    }
//                    else
//                    {
//                        myScript.selectedPacks &= ~package;
//                    }
//                }
//                GUILayout.EndHorizontal();
//            }
//        }
//    }
//#endif
//}
