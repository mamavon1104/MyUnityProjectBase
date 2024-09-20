using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mamavon.Useful
{
    public class LoadSceneMamavon : SingletonMonoBehaviour<LoadSceneMamavon>
    {
        [SerializeField] private ReactiveProperty<bool> _isRoadSceneNow = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> IsRoadSceneNow
        {
            get { return _isRoadSceneNow; }
        }
        public async UniTask LoadScene(SceneObject sceneObject)
        {
            _isRoadSceneNow.Value = true;

            var asyncLoad = SceneManager.LoadSceneAsync(sceneObject);
            asyncLoad.allowSceneActivation = true;
            await asyncLoad;

            _isRoadSceneNow.Value = false;
        }
    }
}