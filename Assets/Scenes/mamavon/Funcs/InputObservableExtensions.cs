using System;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Mamavon.Funcs
{
    /// <summary>
    /// IObservable<long>を利用して対してUniRXを発動します
    /// </summary>
    public static class InputObservableUtility
    {
        /// <summary>
        /// 共有されたUpdateのObservable。
        /// EveryUpdateをShareすることで全体で使えるから軽くて良いねって事らしいですわ。
        /// </summary>
        private static readonly IObservable<long> UpdateObservable = Observable.EveryUpdate().Share();

        /// <summary>
        /// マウスクリック（押した瞬間）
        /// </summary>
        /// <param name="buttonId">マウスボタンのID（デフォルトは左クリック）</param>
        /// <returns>マウスボタンが押された瞬間を通知するObservable</returns>
        public static IObservable<long> OnMouseDown(int buttonId = 0)
            => UpdateObservable.Where(_ => Input.GetMouseButtonDown(buttonId));

        /// <summary>
        /// マウスクリック（離した瞬間）
        /// </summary>
        /// <param name="buttonId">マウスボタンのID（デフォルトは左クリック）</param>
        /// <returns>マウスボタンが離された瞬間を通知するObservable</returns>
        public static IObservable<long> OnMouseUp(int buttonId = 0)
            => UpdateObservable.Where(_ => Input.GetMouseButtonUp(buttonId));

        /// <summary>
        /// マウスクリック（押している間）
        /// </summary>
        /// <param name="buttonId">マウスボタンのID（デフォルトは左クリック）</param>
        /// <returns>マウスボタンが押されている間を通知するObservable</returns>
        public static IObservable<long> OnMouseHold(int buttonId = 0)
            => UpdateObservable.Where(_ => Input.GetMouseButton(buttonId));

        /// <summary>
        /// キー入力（押した瞬間）
        /// </summary>
        /// <param name="key">監視するキー</param>
        /// <returns>指定されたキーが押された瞬間を通知するObservable</returns>
        public static IObservable<long> OnKeyDown(KeyCode key)
            => UpdateObservable.Where(_ => Input.GetKeyDown(key));

        /// <summary>
        /// キー入力（離した瞬間）
        /// </summary>
        /// <param name="key">監視するキー</param>
        /// <returns>指定されたキーが離された瞬間を通知するObservable</returns>
        public static IObservable<long> OnKeyUp(KeyCode key)
            => UpdateObservable.Where(_ => Input.GetKeyUp(key));

        /// <summary>
        /// キー入力（押している間）
        /// </summary>
        /// <param name="key">監視するキー</param>
        /// <returns>指定されたキーが押されている間を通知するObservable</returns>
        public static IObservable<long> OnKeyHold(KeyCode key)
            => UpdateObservable.Where(_ => Input.GetKey(key));

        /// <summary>
        /// 任意のキー入力（押した瞬間）
        /// </summary>
        /// <returns>任意のキーが押された瞬間を通知するObservable</returns>
        public static IObservable<KeyCode> OnAnyKeyDown()
            => UpdateObservable
                .Where(_ => Input.anyKeyDown)
                .SelectMany(_ => Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>())
                .Where(key => Input.GetKeyDown(key));

        /// <summary>
        /// 任意のキー入力（離した瞬間）
        /// </summary>
        /// <returns>任意のキーが離された瞬間を通知するObservable</returns>
        public static IObservable<KeyCode> OnAnyKeyUp()
            => UpdateObservable
                .SelectMany(_ => Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>())
                .Where(key => Input.GetKeyUp(key));

        /// <summary>
        /// 任意のキー入力（押している間）
        /// </summary>
        /// <returns>任意のキーが押されている間を通知するObservable</returns>
        public static IObservable<KeyCode> OnAnyKeyHold()
            => UpdateObservable
                .Where(_ => Input.anyKey)
                .SelectMany(_ => Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>())
                .Where(key => Input.GetKey(key));

        /// <summary>
        /// ボタン入力（押した瞬間）
        /// </summary>
        /// <param name="buttonName">監視するボタンの名前</param>
        /// <returns>指定されたボタンが押された瞬間を通知するObservable</returns>
        public static IObservable<long> OnButtonDown(string buttonName)
            => UpdateObservable.Where(_ => Input.GetButtonDown(buttonName));

        /// <summary>
        /// ボタン入力（離した瞬間）
        /// </summary>
        /// <param name="buttonName">監視するボタンの名前</param>
        /// <returns>指定されたボタンが離された瞬間を通知するObservable</returns>
        public static IObservable<long> OnButtonUp(string buttonName)
            => UpdateObservable.Where(_ => Input.GetButtonUp(buttonName));

        /// <summary>
        /// ボタン入力（押している間）
        /// </summary>
        /// <param name="buttonName">監視するボタンの名前</param>
        /// <returns>指定されたボタンが押されている間を通知するObservable</returns>
        public static IObservable<long> OnButtonHold(string buttonName)
            => UpdateObservable.Where(_ => Input.GetButton(buttonName));

        /// <summary>
        /// 軸入力
        /// </summary>
        /// <param name="axisName">監視する軸の名前</param>
        /// <returns>指定された軸の入力値を通知するObservable</returns>
        public static IObservable<float> OnAxisInput(string axisName)
            => UpdateObservable.Select(_ => Input.GetAxis(axisName));
    }
}