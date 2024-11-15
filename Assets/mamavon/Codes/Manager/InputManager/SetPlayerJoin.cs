using Mamavon.Funcs;
using Mamavon.Useful;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mamavon.Code
{
    public class PlayerJoinHandler : MonoBehaviour
    {
        [SerializeField] PlayerSelectManager playerSelectionManager;
        [SerializeField] PlayerInputManagerSessionSystem inputManager;

        private void OnEnable()
        {
            inputManager.joinAction += JoinEvent;
            inputManager.leftAction += LeftEvent;
        }

        private void JoinEvent(PlayerInput input)
        {
            input.Debuglog("join");
            playerSelectionManager.UpdatePlayerJoinStatus(input.playerIndex, true);
        }
        private void LeftEvent(PlayerInput input)
        {
            input.Debuglog("left");
            playerSelectionManager.UpdatePlayerJoinStatus(input.playerIndex, false);
        }
    }
}
