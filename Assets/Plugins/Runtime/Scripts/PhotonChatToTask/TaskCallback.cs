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
        /// 購読された
        /// </summary>
        public static UniTask<SubscribeResult[]> OnSubscribedAsync(int Count)
        {
            return Callbacks.Instance.OnSubscribedAsync(Count);
        }

        /// <summary>
        /// 購読が解除された
        /// </summary>
        public static UniTask<string[]> OnUnsubscribedAsync()
        {
            return Callbacks.Instance.OnUnsubscribedAsync;
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
