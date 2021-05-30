using UnityEngine;
using Cysharp.Threading.Tasks;

namespace PhotonChatToTask
{
    /// <summary>
    /// Taskコールバック
    /// </summary>
    public static class TaskCallback
    {
        /// <summary>
        /// 接続された
        /// </summary>
        public static UniTask OnConnectedAsync()
        {
            return Callbacks.Instance.OnConnectedAsync;
        }

        /// <summary>
        /// 切断された
        /// </summary>
        public static UniTask OnDisconnectedAsync()
        {
            return Callbacks.Instance.OnDisconnectedAsync;
        }
    }
}
