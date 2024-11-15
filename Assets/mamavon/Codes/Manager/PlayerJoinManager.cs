using Mamavon.Funcs;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mamavon.Useful
{
    /// <summary>
    /// プレイヤーの入退室の管理クラス（アウトゲーム 特にキャラクター選択）
    /// </summary>
    public class PlayerJoinManager : MonoBehaviour
    {
        [Header("JoinActionを設定しておいて"), SerializeField]
        private InputActionReference playerJoinInputAction = default;
        private InputAction act;

        [Header("セッションシステム"), SerializeField] PlayerInputManagerSessionSystem sessionSystem;

        [Header("プレイヤーのプレファブ"), SerializeField] private PlayerInput playerPrefab = default;

        [Header("プレイヤーのマックス人数"), SerializeField] private int maxPlayerCount;

        // Join済みのデバイス情報
        private InputDevice[] joinedDevices = default;

        private void Awake()
        {
            act = new InputAction(name: playerJoinInputAction.action.name);

            // 参照されたアクションからバインディングをコピー (InputActionAssetsをC#化しないと挙動が変わるので注意)
            foreach (var binding in playerJoinInputAction.action.bindings)
            {
                act.AddBinding(binding.path);
            }

            joinedDevices = new InputDevice[maxPlayerCount];
        }
        private void OnEnable()
        {
            sessionSystem.joinAction += SetPlayerDevices;
            sessionSystem.leftAction += LeftPlayerProcess;

            act.Enable();
            act.performed += OnJoin;
        }

        private void OnDestroy()
        {
            sessionSystem.joinAction -= SetPlayerDevices;
            sessionSystem.leftAction -= LeftPlayerProcess;
            act.Dispose();
        }

        /// <summary>
        /// デバイスによってJoin要求が発火したときに呼ばれる処理
        /// </summary>
        private void OnJoin(InputAction.CallbackContext context)
        {
            int playerNum = GetPlayerIndex();

            // プレイヤー数が最大数に達していたら、処理を終了
            if (playerNum >= maxPlayerCount)
                return;

            if (joinedDevices.Contains(context.control.device)) //もうデバイスが入ってても終了
                return;

            // contextDeviceとplayerInputを紐づけてインスタンスを生成する
            PlayerInput.Instantiate(
                    prefab: playerPrefab.gameObject,
                    playerIndex: playerNum,
                    pairWithDevice: context.control.device
                );

        }
        private void SetPlayerDevices(PlayerInput input)
        {
            joinedDevices[input.playerIndex] = input.devices[0];
        }
        private void LeftPlayerProcess(PlayerInput playerInp)
        {
            playerInp.playerIndex.Debuglog();
            int index = playerInp.playerIndex;
            if (index >= 0 && index < maxPlayerCount)
            {
                joinedDevices[index] = null;
                Debug.Log($"プレイヤー {index + 1} が退出しました。");
            }
        }

        private int GetPlayerIndex()
        {
            for (int i = 0; i < joinedDevices.Length; i++)
            {
                if (joinedDevices[i] == null)
                    return i;
            }
            return maxPlayerCount; //こいつを返しておけば returnValue(maxPlayerCount) >= maxPlayerCountで消える。
        }
    }
}