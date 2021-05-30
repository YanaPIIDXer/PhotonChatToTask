using System.Threading;
using Cysharp.Threading.Tasks;
using Photon.Chat;
using ExitGames.Client.Photon;
using System;

namespace PhotonChatToTask
{
    /// <summary>
    /// PhotonChatとの接続を行うクラス
    /// </summary>
    public class ChatToTaskNetwork
    {
        /// <summary>
        /// Peer
        /// </summary>
        private ChatClient Client = null;

        /// <summary>
        /// ConnectUsingSettings
        /// </summary>
        /// <param name="Settings">接続の設定</param>
        /// <param name="Token">キャンセラレーショントークン</param>
        /// <returns>UniTask</returns>
        public async UniTask ConnectUsingSettings(ChatAppSettings Settings, CancellationToken Token = default)
        {
            if (Client != null) { return; }

            Client = new ChatClient(Callbacks.Instance, ConnectionProtocol.Udp);

            // TODO:Disconnectも考慮するようにする
            var ConnTask = UniTask.WhenAny(TaskCallback.OnConnectedAsync().AsAsyncUnitUniTask());

            Client.ConnectUsingSettings(Settings);

            var (Idx, _) = await ConnTask.AttachExternalCancellation(Token);

            if (Idx != 0)
            {
                // TODO:自前のExceptionを定義した方がいいかも知れない
                throw new Exception("Connection Failed.");
            }
        }

        #region Singleton
        public static ChatToTaskNetwork Instance { get { return _Instance; } }
        private static ChatToTaskNetwork _Instance = new ChatToTaskNetwork();
        private ChatToTaskNetwork() { }
        #endregion
    }
}
