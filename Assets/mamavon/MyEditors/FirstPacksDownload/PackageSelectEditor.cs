#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Mamavon.MyEditor
{
    /// <summary>
    /// パッケージ選択用の ScriptableObjectだよ。
    /// </summary>

    public class PackageSelectionEditorWindow : EditorWindow
    {
        private Vector2 scrollPosition = Vector2.zero; //scrollPosはvector2で保存

        private UnityPackages selectedPacks;
        public UnityPackages SelectedPacks { get => selectedPacks; }

        public void SetEverythingPackages() => selectedPacks = UnityPackages.Everything;
        public void ResetPackages() => selectedPacks = UnityPackages.None;


        [MenuItem("Mamavon/My Editors/DownLoadPacksWindow")]
        public static void ShowWindow()
        {
            GetWindow(typeof(PackageSelectionEditorWindow));
        }

        private UnityPackages[] allPackages;

        private void OnEnable()
        {
            // 一度だけ Enum の全値を取得してキャッシュ
            allPackages = (UnityPackages[])System.Enum.GetValues(typeof(UnityPackages));
        }

        void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(
                scrollPosition, //スクロールポジション、
                false,　//横は動かないように、
                true,   //縦は動けるように、
                GUILayout.ExpandHeight(true) //GUIlayout(可変長引数)をtrue(auto)に設定する。
            );

            GUILayout.Space(10);
            EditorGUILayout.LabelField("以下、選択されているパッケージ:");
            GUILayout.Space(10);

            DisplayPackageGroup("Code Packs", UnityPackages.CodePacks);
            DisplayPackageGroup("Unity Tool Packs", UnityPackages.UnityToolPacks);
            DisplayPackageGroup("Unity Visual Packs", UnityPackages.UnityVisualPacks);
            DisplayPackageGroup("Others", UnityPackages.Others);

            GUILayout.Space(20);

            if (GUILayout.Button("選んだパッケージをインストールしちゃいますよ。"))
            {
                PackageDownloadClass.DownloadPackage(this, PackageManagerAction.Install);
            }
            if (GUILayout.Button("選んだパッケージをアンインストールしちゃいますよ。"))
            {
                PackageDownloadClass.DownloadPackage(this, PackageManagerAction.Uninstall);
            }

            GUILayout.Space(10);

            if (GUILayout.Button("全て選択しましょう。"))
            {
                SetEverythingPackages();
            }
            if (GUILayout.Button("リセットします。"))
            {
                ResetPackages();
            }

            GUILayout.EndScrollView();
        }

        /// <summary>
        /// チェックボックスとかの描画を行っています
        /// </summary>
        /// <param name="myScript">そのScriptableObject</param>
        /// <param name="groupName">グループの名前、例えばCodePacks(rx,task,container)とか</param>
        /// <param name="groupFlag">nameと一致するようなUnityPackagesの奴、要素が加算(A | B)されているものがここに入る</param>
        private void DisplayPackageGroup(string groupName, UnityPackages groupFlag)
        {
            bool isGroupSelected = (selectedPacks & groupFlag) == groupFlag;

            EditorGUI.BeginChangeCheck();
            GUILayout.Space(10);
            isGroupSelected = EditorGUILayout.Toggle(groupName, isGroupSelected);

            if (EditorGUI.EndChangeCheck())
            {
                if (isGroupSelected)
                {
                    selectedPacks |= groupFlag;
                }
                else
                {
                    selectedPacks &= ~groupFlag;
                }
            }

            foreach (UnityPackages package in allPackages)
            {
                if (package == UnityPackages.None || package == UnityPackages.Everything || package == groupFlag)
                    continue;

                if ((groupFlag & package) != package)
                    continue;

                GUILayout.BeginHorizontal();
                GUILayout.Space(20);

                bool isSelected = (selectedPacks & package) == package;

                EditorGUI.BeginChangeCheck();
                isSelected = EditorGUILayout.Toggle(package.ToString(), isSelected);

                if (EditorGUI.EndChangeCheck())
                {
                    if (isSelected)
                    {
                        selectedPacks |= package;
                    }
                    else
                    {
                        selectedPacks &= ~package;
                    }
                }
                GUILayout.EndHorizontal(); // endHorizontal
            }
        }
    }
}
#endif
