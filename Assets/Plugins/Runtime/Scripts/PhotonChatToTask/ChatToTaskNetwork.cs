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
        /// 接続
        /// </summary>
        /// <param name="AppId">AppID</param>
        /// <param name="AppVersion">アプリケーションのバージョン</param>
        /// <param name="Token">キャンセラレーショントークン</param>
        public async UniTask Connect(string AppId, string AppVersion, CancellationToken Token = default)
        {
            if (Client != null) { return; }

            Client = new ChatClient(Callbacks.Instance, ConnectionProtocol.Udp);
            ClientService.Create(Client);

            // TODO:Disconnectも考慮する
            var ConnTask = UniTask.WhenAny(
                TaskCallback.OnConnectedAsync().AsAsyncUnitUniTask(),
                TaskCallback.OnDisconnectedAsync().AsAsyncUnitUniTask());

            if (!Client.Connect(AppId, AppVersion, null))
            {
                Client = null;
                throw new Exception("Connection Failed. Reason:Connect method returns false.");
            }

            var (Idx, _, __) = await ConnTask.AttachExternalCancellation(Token);

            if (Idx != 0)
            {
                Client = null;
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
