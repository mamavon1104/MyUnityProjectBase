using UnityEngine;
using UnityEngine.InputSystem;

namespace Mamavon.Useful
{
    public class SetPlayerInputAction : MonoBehaviour
    {
        [SerializeField] private PlayerInputManagerSessionSystem m_playerInputManager;

        private void Awake()
        {
            m_playerInputManager.deviceChangedAction += ReconnectDeviceAction;
        }


        private void ReconnectDeviceAction(InputDevice device, InputDeviceChange deviceChange)
        {

        }
    }
}
