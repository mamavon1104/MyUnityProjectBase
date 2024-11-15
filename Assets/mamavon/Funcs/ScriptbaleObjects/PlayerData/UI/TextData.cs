using UnityEngine;
namespace Mamavon.Data
{
    /// <summary>
    /// ScriptableObjectだよ。
    /// </summary>
    [CreateAssetMenu(fileName = "TextData", menuName = "Mamavon Packs/ScriptableObject/TextData")]
    public class TextData : ScriptableObject
    {
        [TextArea(3, 10)] // 最小3行、最大10行のテキストエリアを作成
        [SerializeField]
        private string m_text;

        /// <summary>
        /// テキストコンテンツを取得します。
        /// </summary>
        public string Text => m_text;

        /// <summary>
        /// テキストの行数を取得します。
        /// </summary>
        public int TextCount => m_text.Split('\n').Length;
    }
}