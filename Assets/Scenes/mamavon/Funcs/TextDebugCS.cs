#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Mamavon.Funcs.DebugScripts
{
    /// <summary>
    /// �p�b�P�[�W�I��p�� ScriptableObject����B
    /// </summary>
    [CreateAssetMenu(fileName = "DebugTextObjs", menuName = "Package Manager/Text Debug")]
    public class TextDebugCS : ScriptableObject
    {
        public TextColor textColor;
        public string text;
    }

    [CustomEditor(typeof(TextDebugCS))] //typeof����requireComponent�Ɠ��������Ŏg����݂�����
    public class TextDebugEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            TextDebugCS myScript = (TextDebugCS)target;
            DrawDefaultInspector();

            GUILayout.Space(10);

            if (GUILayout.Button("���f�o�b�O���܂��B"))
            {
                //�l���Ԃ��Ă���̂Ŕz��ɒl��ǉ����Ȃ����������Debug�o����B
                var a = myScript.text.Debuglog(myScript.textColor);
            }
        }
    }
}
#endif
