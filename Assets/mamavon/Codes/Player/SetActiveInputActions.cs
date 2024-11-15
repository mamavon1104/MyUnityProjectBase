using Mamavon.Data;
using Mamavon.Funcs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mamavon.Code
{
    public class SetActiveInputActions : MonoBehaviour
    {
        [SerializeField] PlayerInput playerInput;
        [SerializeField] InputSystemNameActionData[] inputActionsDatas;

        [Header("InputAction")][SerializeField] InputAction[] inputActionArray;

        private void CompareActions()
        {
            if (playerInput == null) playerInput = GetComponent<PlayerInput>();
            inputActionArray = new InputAction[inputActionsDatas.Length];

            for (int i = 0; i < inputActionsDatas.Length; i++)
            {
                InputAction action = inputActionsDatas[i].actionReference.action;
                foreach (InputAction playerInputAction in playerInput.actions)
                {
                    // セットしたactiondata[i]とplayerInutのaction.name、そしてActionMapの名前が一致していたらセットする
                    // → actiondata[0]{jump}とplayerInputActions{jump}を紐づける行動をする。
                    if (playerInputAction.name != action.name || playerInputAction.actionMap?.name != action.actionMap?.name)
                        continue;

                    inputActionArray[i] = InputActionReference.Create(playerInput.actions[action.name]);
                    break;
                }
            }
        }

        private void OnEnable()
        {
            "InputActionをEnabledで有効化します".Debuglog(TextColor.Blue);
            CompareActions();
            EnabledActions();
        }
        /// <summary>
        /// ActionsをUniRxで待てるように設定します。
        /// もう設定されている場合は上書きをする為何度読んでも大丈夫
        ///
        /// </summary>
        public void EnabledActions()
        {
            for (int i = 0; i < inputActionArray.Length; i++)
            {
                inputActionsDatas[i].EnableAction(playerInput, inputActionArray[i]);
            }
        }
        private void OnDisable()
        {
            "InputActionを無効化します".Debuglog(TextColor.Red);
            for (int i = 0; i < inputActionArray.Length; i++)
            {
                inputActionsDatas[i].DisableAction(playerInput, inputActionArray[i]);
            }
        }
        private void OnDestroy()
        {
            "InputActionを破棄します".Debuglog(TextColor.Black);
            foreach (var act in inputActionArray)
            {
                InputWrapperManager.Instance.DestroyAction(playerInput.playerIndex, act.name);
            }
        }
    }
}