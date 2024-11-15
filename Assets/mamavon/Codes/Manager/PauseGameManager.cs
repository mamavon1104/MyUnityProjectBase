// https://gomafrontier.com/unity/3720　コピペ元

using Mamavon.Funcs;
using System;
using UniRx;
using UnityEngine;

namespace Mamavon.Useful
{
    /// <summary>
    /// ゲームをポーズしているかどうかをここで設定します。
    /// </summary>
    public class PauseGameManager : SingletonMonoBehaviour<PauseGameManager>
    {
        private Subject<Unit> pauseSubject = new Subject<Unit>();
        private Subject<Unit> resumeSubject = new Subject<Unit>();

        [Header("ポーズしていますか？"), SerializeField] private bool isPaused = false;

        /// <summary>
        /// ポーズ中かどうか
        /// </summary>
        public bool IsPaused => isPaused;
        public IObservable<Unit> OnPaused
        {
            get { return pauseSubject; }
        }

        public IObservable<Unit> OnResumed
        {
            get { return resumeSubject; }
        }

        public void ChangePauseState()
        {
            if (!isPaused)
                Pause();
            else
                Resume();
        }

        [ContextMenu("ゲームポーズ")]
        private void Pause()
        {
            isPaused = true;
            isPaused.Debuglog("ポーズ中です :");
            pauseSubject.OnNext(Unit.Default);
        }

        [ContextMenu("ゲーム再開")]
        private void Resume()
        {
            isPaused = false;
            isPaused.Debuglog("ポーズを解除しました。 :");
            resumeSubject.OnNext(Unit.Default);
        }
    }
}