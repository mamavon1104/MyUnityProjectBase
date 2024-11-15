using Cysharp.Threading.Tasks;
using Mamavon.Code;
using System;
using System.Linq;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mamavon.Useful
{

    public class PlayerSelectManager : MonoBehaviour
    {
        [SerializeField] PlayerInputManager inputManager;

        [SerializeField]
        private BoolReactiveProperty[] _playerActiveStatus = new BoolReactiveProperty[]
        {
            new BoolReactiveProperty(true),
            new BoolReactiveProperty(false),
            new BoolReactiveProperty(false),
            new BoolReactiveProperty(false),
        };

        public BoolReactiveProperty[] PlayerActiveStatus
        {
            get => _playerActiveStatus;
        }
        public void UpdatePlayerJoinStatus(int playerIndex, bool isActive)
        {
            _playerActiveStatus[playerIndex].Value = isActive;
        }

        [SerializeField] private MoveScene sceneTransitionManager;
        private CompositeDisposable disposables = new CompositeDisposable();
        private CancellationTokenSource cts;

        private void Start()
        {
            InitializePlayerMonitoring();
        }

        private void InitializePlayerMonitoring()
        {
            cts = new CancellationTokenSource();

            Observable.CombineLatest(_playerActiveStatus)
                .Select(values => values.All(v => v == false))
                .DistinctUntilChanged()
                .Where(allInactive => allInactive)
                .Subscribe(_ => StartInactivityTimer(cts.Token).Forget())
                .AddTo(disposables);
        }

        private async UniTaskVoid StartInactivityTimer(CancellationToken token)
        {
            try
            {
                Debug.Log("全員参加していません");
                await UniTask.Delay(TimeSpan.FromSeconds(2.5f), cancellationToken: token);
                if (_playerActiveStatus.All(v => v.Value == false))
                {
                    HandleAllPlayersInactive();
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("キャンセルされました");
            }
        }

        private void HandleAllPlayersInactive()
        {
            Debug.Log("2.5秒が経ちました");
            sceneTransitionManager.LoadScene();
        }

        private void OnDestroy()
        {
            disposables.Dispose();
            cts?.Cancel();
            cts?.Dispose();
        }
    }
}