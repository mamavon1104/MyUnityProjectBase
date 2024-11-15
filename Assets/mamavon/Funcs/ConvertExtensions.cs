using System;
using UnityEngine.Events;

namespace Mamavon.Funcs
{
    public static class ConvertExtensions
    {
        /// <summary>
        /// UnityEventをActionに変換します。
        /// </summary>
        /// <param name="unityEvent">変換元のUnityEvent</param>
        /// <returns>UnityEventに相当するAction</returns>
        public static Action ConvertToAction(this UnityEvent unityEvent)
        {
            return () => unityEvent?.Invoke();
        }

        /// <summary>
        /// UnityEvent<T>をAction<T>に変換します。
        /// </summary>
        /// <typeparam name="T">パラメータの型</typeparam>
        /// <param name="unityEvent">変換元のUnityEvent<T></param>
        /// <returns>UnityEvent<T>に相当するAction<T></returns>
        public static Action<T> ConvertToAction<T>(this UnityEvent<T> unityEvent)
        {
            return (arg) => unityEvent?.Invoke(arg);
        }

        /// <summary>
        /// ActionをUnityEventに変換します。
        /// </summary>
        /// <param name="action">変換元のAction</param>
        /// <returns>Actionに相当するUnityEvent</returns>
        public static UnityEvent ConvertToUnityEvent(this Action action)
        {
            UnityEvent unityEvent = new UnityEvent();
            unityEvent.AddListener(() => action?.Invoke());
            return unityEvent;
        }

        /// <summary>
        /// Action<T>をUnityEvent<T>に変換します。
        /// </summary>
        /// <typeparam name="T">パラメータの型</typeparam>
        /// <param name="action">変換元のAction<T></param>
        /// <returns>Action<T>に相当するUnityEvent<T></returns>
        public static UnityEvent<T> ConvertToUnityEvent<T>(this Action<T> action)
        {
            UnityEvent<T> unityEvent = new UnityEvent<T>();
            unityEvent.AddListener((arg) => action?.Invoke(arg));
            return unityEvent;
        }
    }
}