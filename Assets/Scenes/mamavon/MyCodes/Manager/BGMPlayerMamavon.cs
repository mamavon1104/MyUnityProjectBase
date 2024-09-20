using Cysharp.Threading.Tasks;
using Mamavon.Useful;
using System;
using UniRx;
using UnityEngine;
namespace Mamavon.Funcs
{
    public class BGMPlayerMamavon : SingletonMonoBehaviour<BGMPlayerMamavon>
    {
        private AudioSource audioSource;

        [SerializeField] private AudioClip defaultBGM;

        private ReactiveProperty<float> volumeReactiveProperty = new ReactiveProperty<float>(1f);
        public IReadOnlyReactiveProperty<float> Volume => volumeReactiveProperty;

        private Subject<Unit> onBGMChanged = new Subject<Unit>();
        public IObservable<Unit> OnBGMChanged => onBGMChanged;

        protected override void OnCreateInstance()
        {
            audioSource = gameObject.GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.playOnAwake = false;

            SetupReactiveProperties();

            if (defaultBGM != null)
            {
                PlayBGMAsync(defaultBGM).Forget();
            }
        }

        private void SetupReactiveProperties()
        {
            volumeReactiveProperty.Subscribe(volume =>
            {
                audioSource.volume = volume;
            }).AddTo(this);
        }

        public async UniTask PlayBGMAsync(AudioClip clip, float fadeDuration = 0.5f)
        {
            if (audioSource.isPlaying)
            {
                await FadeVolumeAsync(0f, fadeDuration);
                audioSource.Stop();
            }

            audioSource.clip = clip;
            audioSource.Play();
            await FadeVolumeAsync(volumeReactiveProperty.Value, fadeDuration);
            onBGMChanged.OnNext(Unit.Default);
        }

        public async UniTask StopBGMAsync(float fadeDuration = 0.5f)
        {
            await FadeVolumeAsync(0f, fadeDuration);
            audioSource.Stop();
        }

        public void PauseBGM()
        {
            audioSource.Pause();
        }

        public void ResumeBGM()
        {
            audioSource.UnPause();
        }

        public void SetVolume(float volume)
        {
            volumeReactiveProperty.Value = Mathf.Clamp01(volume);
        }

        public async UniTask FadeVolumeAsync(float targetVolume, float duration)
        {
            float startVolume = audioSource.volume;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                float currentVolume = Mathf.Lerp(startVolume, targetVolume, t);
                SetVolume(currentVolume);
                await UniTask.Yield();
            }

            SetVolume(targetVolume);
        }

        public IDisposable SubscribeToVolumeChanges(Action<float> action)
        {
            return Volume.Subscribe(action);
        }
    }
}