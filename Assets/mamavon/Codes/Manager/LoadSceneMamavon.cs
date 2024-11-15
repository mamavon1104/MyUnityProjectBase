using Cysharp.Threading.Tasks;
using Mamavon.Funcs;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Mamavon.Useful
{
    public enum SceneLoadType
    {
        Direct,
        Fade,
    }

    public class LoadSceneMamavon : SingletonMonoBehaviour<LoadSceneMamavon>
    {
        [SerializeField] private ReactiveProperty<bool> _isLoadingScene = new ReactiveProperty<bool>(false);
        public IReadOnlyReactiveProperty<bool> IsLoadingScene => _isLoadingScene;

        [SerializeField] private Image _fadeImage = null;

        public async UniTask LoadScene(SceneObject targetScene, SceneLoadType loadType, float transitionDuration = 1.0f)
        {
            if (_isLoadingScene.Value)
                return;

            _isLoadingScene.Value = true;

            try
            {
                ISceneLoadProcess loadProcess = loadType switch
                {
                    SceneLoadType.Direct => new DirectSceneLoad(),
                    SceneLoadType.Fade => new FadeSceneLoad(),
                    _ => throw new ArgumentException("未実装のロードタイプよん", nameof(loadType))
                };

                loadProcess.SceneLoadFunc = ExecuteLoadScene;
                await loadProcess.ExecuteLoadProcess(targetScene, transitionDuration, _fadeImage);
            }
            finally
            {
                _isLoadingScene.Value = false;
            }
        }

        private async UniTask ExecuteLoadScene(SceneObject sceneObject)
        {
            var asyncLoad = SceneManager.LoadSceneAsync(sceneObject);
            asyncLoad.allowSceneActivation = true;
            await asyncLoad;
        }

        protected override void OnCreateInstance()
        {
            GameObject canvasObj = CreateCanvas();
            CreateFadeImage(canvasObj);

            GameObject CreateCanvas()
            {
                GameObject canvasObj = new GameObject("Canvas");

                canvasObj.transform.SetParent(transform, false);

                var canvas = canvasObj.AddComponent<Canvas>();

                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.sortingOrder = 9999; // 最前面に表示されるように
                canvasObj.AddComponent<CanvasScaler>();
                canvasObj.AddComponent<GraphicRaycaster>();
                return canvasObj;
            }
            void CreateFadeImage(GameObject canvasObj)
            {
                GameObject imageObj = new GameObject("FadeImage");
                imageObj.transform.SetParent(canvasObj.transform, false);

                imageObj.transform.localScale = new Vector3(100, 100, 100);

                _fadeImage = imageObj.AddComponent<Image>();

                _fadeImage.color = Color.black;
                _fadeImage.raycastTarget = false;
                imageObj.SetActive(false);
            }
        }
        private void OnDestroy()
        {
            _isLoadingScene.Dispose();
        }
    }

    public interface ISceneLoadProcess
    {
        Func<SceneObject, UniTask> SceneLoadFunc { set; }
        UniTask ExecuteLoadProcess(SceneObject sceneObject, float duration, Image fadeImage);
    }

    public class DirectSceneLoad : ISceneLoadProcess
    {
        private Func<SceneObject, UniTask> _sceneLoad;
        public Func<SceneObject, UniTask> SceneLoadFunc
        {
            set => _sceneLoad = value;
        }

        public async UniTask ExecuteLoadProcess(SceneObject sceneObject, float _, Image __)
        {
            await _sceneLoad(sceneObject);          //フェードイン等せずロード
        }
    }

    public class FadeSceneLoad : ISceneLoadProcess
    {
        private Func<SceneObject, UniTask> _sceneLoad;
        public Func<SceneObject, UniTask> SceneLoadFunc
        {
            set => _sceneLoad = value;
        }

        public async UniTask ExecuteLoadProcess(SceneObject sceneObject, float fadeDuration, Image fadeImage)
        {
            await fadeImage.FadeIn(fadeDuration);
            await _sceneLoad(sceneObject);          //フェードインしてロード、ロードが終わり次第フェードアウト
            await fadeImage.FadeOut(fadeDuration);
        }
    }
}
