using Mamavon.Useful;
using UnityEngine;

namespace Mamavon.Code
{
    public class MoveScene : MonoBehaviour
    {
        [SerializeField] private float m_fadeTime = 1.0f;
        [SerializeField] private SceneObject m_targetScene;

        private static bool movingScene = false;

        [ContextMenu("シーンをロードします。")]
        public async void LoadScene()
        {
            if (movingScene)
                return;

            movingScene = true;
            //awaitこれで解決できる
            await LoadSceneMamavon.Instance.LoadScene(m_targetScene, SceneLoadType.Fade, m_fadeTime);
            movingScene = false;
        }
    }
}