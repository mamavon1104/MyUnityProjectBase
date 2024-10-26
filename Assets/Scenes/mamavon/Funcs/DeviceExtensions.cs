using System;
using UnityEngine.InputSystem;
namespace Mamavon.Funcs
{
    public static class DeviceExtensions
    {
        public enum DeviceState
        {
            Connect,
            Disconnect,
            Both,
        }
        /// <summary>
        /// InputDeviceChangeに対して、<br>
        /// --そのコントローラーが接続した<br>
        /// --そのコントローラーが切断した<br>
        /// --そのコントローラーが接続と接続どちらもした<br>
        /// それを返します
        /// </summary>
        /// <returns>
        /// Connect: 接続
        /// Disconnect: 切断
        /// Both: 両方
        /// </returns>
        public static DeviceState GetDevicesState(InputDeviceChange change)
        {
            return change switch
            {
                InputDeviceChange.Added or                              // Added: 新しいデバイスが追加された
                InputDeviceChange.Reconnected or                        // Reconnected: デバイスが再接続された
                InputDeviceChange.Enabled or                            // Enabled: デバイスが有効化された
                InputDeviceChange.SoftReset => DeviceState.Connect,   // SoftReset: デバイスがソフトリセットされた(バックグラウンドからの復帰とか)

                InputDeviceChange.Removed or                            // Removed: デバイスが削除された
                InputDeviceChange.Disconnected or                       // Disconnected: デバイスが切断された
                InputDeviceChange.Disabled or                           // Disabled: デバイスが無効化された
                InputDeviceChange.HardReset => DeviceState.Disconnect,  // HardReset: デバイスがハードリセットされた

                InputDeviceChange.ConfigurationChanged or               // ConfigurationChanged: 設定が変更された(switchみたいに)
                InputDeviceChange.UsageChanged => DeviceState.Both,     // UsageChanged: 使用状況が変更された(VRとかで)

                _ => throw new ArgumentException($"{change}が無効どす") // まあ十中八九「Destory」、InputSystemがアップデート来たらおわち(^-^)-☆
            };
        }
    }
}