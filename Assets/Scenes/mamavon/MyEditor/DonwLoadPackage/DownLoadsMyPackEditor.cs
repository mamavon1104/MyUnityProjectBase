//#if UNITY_EDITOR
//using UnityEditor;
//using UnityEditor.PackageManager;
//using UnityEditor.PackageManager.Requests;
//using UnityEngine;

//namespace Mamavon.Download
//{
//    public class DownLoadsMyPackEditor : EditorWindow
//    {
//        [MenuItem("Mamavon/MyPacksDownload")]
//        public static void ShowWindow()
//        {
//            GetWindow(typeof(DownLoadsMyPackEditor));
//        }
//        private void OnEnable()
//        {

//        }

//        private void OnGUI()
//        {
//            EditorGUILayout.LabelField("以下、選択されているパッケージ:");

//            if (GUILayout.Button("選んだパッケージをインストールしちゃいますよ。"))
//            {
//                DownLoadMyPacks();
//            }
//        }
//        private void DownLoadMyPacks()
//        {
//            const string MY_PACKS_NAME = "C:\\Users\\vanntann\\Desktop\\ProjectBase\\Assets\\Scenes\\mamavon"; //Publicリポジトリにしなきゃいけない。
//            AddRequest addRequest = Client.Add(MY_PACKS_NAME);
//            Debug.Log($"{MY_PACKS_NAME}をインストールしま～す");
//            EditorApplication.update += Progress;
//            void Progress()
//            {
//                if (addRequest.IsCompleted)
//                {
//                    if (addRequest.Status == StatusCode.Success)
//                        Debug.Log("インストールが完了しました: " + addRequest.Result.packageId);
//                    else if (addRequest.Status >= StatusCode.Failure)
//                        Debug.LogError("インストールに失敗しました: " + addRequest.Error.message);
//                    EditorApplication.update -= Progress;
//                    AssetDatabase.Refresh();
//                }
//            }
//        }
//    }
//}
//#endif