#if UNITY_EDITOR
using Mamavon.Funcs;
using UnityEditor;

namespace Mamavon.Editor
{
    public static class UniTaskDOTweenSetup
    {
        //変更しないでね。
        const string ADD_SUPPORT = "UNITASK_DOTWEEN_SUPPORT";

        [MenuItem("Mamavon/UniTask DOTween Setup ")]
        public static void SetupUniTask()
        {
            AddDOTweenSupport();
            "DOTweenサポートが完了しました。".Debuglog("実行終了！");  //拡張だから気にしないで
        }

        static void AddDOTweenSupport()
        {
            string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup
                 (EditorUserBuildSettings.selectedBuildTargetGroup);

            if (!defines.Contains(ADD_SUPPORT)) //DicのようにContainsで探知
            {
                PlayerSettings.SetScriptingDefineSymbolsForGroup(
                    EditorUserBuildSettings.selectedBuildTargetGroup,
                    (defines + ";" + ADD_SUPPORT).Trim(';').Debuglog("Trimされた値！")
                );
            }
        }
    }
}
#endif