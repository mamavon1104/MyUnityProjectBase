using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Mamavon.Funcs;



#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Mamavon.Data
{
    /// <summary>
    /// ScriptableObjectだよ。
    /// </summary>
    [CreateAssetMenu(fileName = "InputSystemNameActionData", menuName = "Mamavon Packs/ScriptableObject/InputSystemNameActionData")]
    [Serializable]
    public class InputSystemNameActionData : ScriptableObject
    {
        [Header("InputAction")] public InputActionReference actionReference;
        [Header("ValueType")] public ValueType myValueType;
        [Header("離したときもイベント発行するか")] public bool triggerOnRelease;
        public enum ValueType
        {
            Bool,
            Unit,
            Float,
            Vector2,
        }
        private static Dictionary<ValueType, object> dic = new Dictionary<ValueType, object>()
        {
            { ValueType.Bool,typeof(bool)},
            { ValueType.Unit,typeof(Unit)},
            { ValueType.Float,typeof(float)},
            { ValueType.Vector2,typeof(Vector2)},
        };

        /// <summary>
        /// アクションを有効化します(SetActiveInputActionsクラスから)<br />
        /// 有効化というよりかはアクションを登録って感じだけどね。
        /// </summary>
        public void EnableAction(PlayerInput playerInput, InputAction action)
        {
            var manager = InputWrapperManager.Instance;

            (playerInput.playerIndex, action, triggerOnRelease).Debuglog(TextColor.BlueViolet);

            switch (myValueType)
            {
                case ValueType.Bool:
                    manager.EnableAction<bool>(playerInput, action, this);
                    break;
                case ValueType.Unit:
                    manager.EnableAction<Unit>(playerInput, action, this);
                    break;
                case ValueType.Float:
                    manager.EnableAction<float>(playerInput, action, this);
                    break;
                case ValueType.Vector2:
                    manager.EnableAction<Vector2>(playerInput, action, this);
                    break;
            }
        }

        /// <summary>
        /// アクションを無効化します(SetActiveInputActionsクラスから)<br />
        /// 無効化というよりかはアクションを除くって感じだけどね。
        /// </summary>
        public void DisableAction(PlayerInput playerInput, InputAction action)
        {
            var manager = InputWrapperManager.Instance;
            switch (myValueType)
            {
                case ValueType.Bool:
                    manager.DisableAction<bool>(playerInput, action, this);
                    break;
                case ValueType.Unit:
                    manager.DisableAction<Unit>(playerInput, action, this);
                    break;
                case ValueType.Float:
                    manager.DisableAction<float>(playerInput, action, this);
                    break;
                case ValueType.Vector2:
                    manager.DisableAction<Vector2>(playerInput, action, this);
                    break;
            }
        }

        /// <summary>
        /// UniRXでIObservableを発行します。<br />
        /// GetObservable<Vector2>とか、GetObservable<Unit>とかで取得してください<br />
        /// 型が違えばエラー吐きます。 <br />
        /// </summary>
        /// <param name="playerIndex">playerInput.playerIndex</param>
        /// <param name="timeSpan">大体10ミリ秒位は連打を無視します。</param>
        /// <returns> IObservable(SubScribeの形で使って) </returns>
        public IObservable<T> GetObservable<T>(int playerIndex, int timeSpan = 10)
        {
            var manager = InputWrapperManager.Instance;

            if (typeof(T) != (Type)dic[myValueType])
                $"指定したValueType{myValueType}と型{typeof(T)}が違います".DebuglogError();

            return manager.GetObservable<T>(playerIndex, actionReference.name).ThrottleFirst(TimeSpan.FromMilliseconds(timeSpan));
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(InputSystemNameActionData))]
    public class InputSystemNameActionDataEditor : UnityEditor.Editor
    {
        private const string IconPath = "Assets/Scenes/mamavon/Images/InputSystemIcon.png";

        private void OnEnable()
        {
            SetScriptableObjectIcon();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            SetScriptableObjectIcon();
        }

        private void SetScriptableObjectIcon()
        {
            InputSystemNameActionData myObject = (InputSystemNameActionData)target;

            Texture2D icon = AssetDatabase.LoadAssetAtPath<Texture2D>(IconPath);

            if (icon != null)
            {
                EditorGUIUtility.SetIconForObject(myObject, icon);
                AssetDatabase.Refresh();
            }
        }
    }
#endif
}