using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Mamavon.Funcs.Scriptables
{
    /// <summary>
    /// パッケージ選択用の ScriptableObjectだよ。
    /// </summary>
    [CreateAssetMenu(fileName = "DebugTextObjs", menuName = "Mamavon Packs/Debug ScripObjs/Text Debug")]
    public class TextDebugCS : ScriptableObject
    {
        public TextColor textColor;
        public string text;
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(TextDebugCS))] //typeofってrequireComponentと同じ感じで使えるみたいね
    public class TextDebugEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            TextDebugCS myScript = (TextDebugCS)target;
            DrawDefaultInspector();

            GUILayout.Space(10);

            if (GUILayout.Button("一回デバッグします。"))
            {
                //値が返ってくるので配列に値を追加しながらもちゃんとDebug出来ますよ。
                var a = myScript.text.Debuglog(myScript.textColor);
            }
        }
    }
#endif
}
