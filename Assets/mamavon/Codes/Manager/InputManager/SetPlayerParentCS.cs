using Mamavon.Useful;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetPlayerParentCS : MonoBehaviour
{
    [SerializeField] PlayerInputManagerSessionSystem manager;
    [SerializeField] private Transform parentObj;
    private void OnEnable()
    {
        if (parentObj == null) parentObj = transform.parent;
        manager.joinAction += SetJoinPlayerName;
    }
    private void OnDisable()
    {
        manager.joinAction -= SetJoinPlayerName;
    }
    private void SetJoinPlayerName(PlayerInput input)
    {
        input.transform.SetParent(parentObj);
    }
}
