using UnityEngine;
using UnityEngine.InputSystem;

public class SetInputAction : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;

    private void OnEnable()
    {
        var manager = InputWrapperManager.Instance;
        manager.RegisterAction<Vector2>("move", inputActions.FindAction("move"));
        manager.RegisterAction<float>("jump", inputActions.FindAction("jump"));
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}