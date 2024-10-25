using Cysharp.Threading.Tasks;
using Mamavon.Code;
using Mamavon.Funcs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mamavon.Useful
{
    public class SetPlayerInputAction : MonoBehaviour
    {
        [SerializeField] PlayerInputManagerSessionCS m_playerInpManager;
        [SerializeField] List<SetInputAction> _playerInputActions = new List<SetInputAction>();

        [ContextMenu("Actionê›íËÇµÇ»Ç®Çµ")]

        private void Awake()
        {
            m_playerInpManager.joinAction += AddPlayerInpAct;
            m_playerInpManager.leftAction += RemovePlayerInpAct;
        }
        private void ResetPlayersInpAct(PlayerInput inp)
        {
            //foreach (var player in _playerInputActions)
            //{
            //    player.Debuglog($"{player.GetComponent<PlayerInput>().user.index}", TextColor.Blue).EnabledActions();
            //}
        }

        private void AddPlayerInpAct(PlayerInput inp)
        {
            inp.TryGetComponent<SetInputAction>(out var inpAct);

            //if (_playerInputActions.Contains(inpAct))
            //    "Ç»ÇÒÇ©listÇ…ìoò^Ç≥ÇÍÇƒÇ¢ÇÈSetInputActionCSÇ‹ÇΩì¸é∫ÇµÇΩÇÒÇæÇØÇ«".DebuglogWarning(inpAct.ToString());

            _playerInputActions.Add(inpAct);
        }
        private async void RemovePlayerInpAct(PlayerInput inp)
        {
            inp.TryGetComponent<SetInputAction>(out var inpAct);

            if (!_playerInputActions.Contains(inpAct))
                "Ç»ÇÒÇ©listÇ…ìoò^Ç≥ÇÍÇƒÇ¢Ç»Ç¢SetInputActionCSëﬁèoÇµÇΩÇÒÇæÇØÇ«".DebuglogWarning();

            _playerInputActions.Remove(inpAct);

            await UniTask.WaitUntil(() => inp.playerIndex.Debuglog() == -1); //äÆëSÇ…çÌèúÇ≥ÇÍÇÈÇ‹Ç≈ë“Ç¬

            ResetPlayersInpAct(inp);
        }
    }

}