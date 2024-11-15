using Cysharp.Threading.Tasks;
using Mamavon.Useful;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mamavon.Code
{
    public class SetPlayerNameCS : MonoBehaviour
    {
        [SerializeField] string playerName;
        [SerializeField] PlayerInputManagerSessionSystem manager;
        private void OnEnable()
        {
            manager.joinAction += SetPlayerName;
        }
        private void OnDisable()
        {
            manager.joinAction -= SetPlayerName;
        }
        private async void SetPlayerName(PlayerInput input)
        {
            await UniTask.WaitForSeconds(2.0f);

            if (input == null) return;

            GameObject player = input.gameObject;
            player.name = $"{playerName}{input.playerIndex}";
        }
    }
}