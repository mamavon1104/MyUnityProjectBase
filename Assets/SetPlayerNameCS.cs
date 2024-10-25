using Mamavon.Useful;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetPlayerNameCS : MonoBehaviour
{
    [SerializeField] PlayerInputManagerSessionCS manager;
    private void OnEnable()
    {
        manager.joinAction += SetPlayerName;
    }
    private void OnDisable()
    {
        manager.joinAction -= SetPlayerName;
    }
    private void SetPlayerName(PlayerInput input)
    {
        GameObject player = input.gameObject;
        player.name = $"Player{input.playerIndex}";
    }
}
