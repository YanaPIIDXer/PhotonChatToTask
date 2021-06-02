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
        /// <param name="EventListener">Taskじゃ捌きようがないコールバックを受け取るためのインタフェース</param>
        public async UniTask Connect(string AppId, string AppVersion, IClientListener EventListener, CancellationToken Token = default)
        {
            if (Client != null) { return; }

            Client = new ChatClient(Callbacks.Instance, ConnectionProtocol.Udp);
            ClientService.Create(Client);

            Callbacks.Instance.EventListener = EventListener;

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

        /// <summary>
        /// チャンネルを購読
        /// </summary>
        /// <param name="Channels">購読するチャンネル名のリスト</param>
        /// <returns>購読結果</returns>
        public async UniTask<SubscribeResult[]> SubscribeChanel(string[] Channels, CancellationToken Token = default)
        {
            if (Client == null) { throw new Exception("Client is null!"); }

            Client.Subscribe(Channels);

            var Results = await TaskCallback.OnSubscribedAsync(Channels.Length);
            return Results;
        }

        /// <summary>
        /// チャンネルの購読を解除
        /// </summary>
        /// <param name="Channels">購読を解除するチャンネル名のリスト</param>
        /// <returns>結果</returns>
        public async UniTask<string[]> UnsubscribeChannel(string[] Channels, CancellationToken Token = default)
        {
            if (Client == null) { throw new Exception("Client is null!"); }

            Client.Unsubscribe(Channels);

            var Results = await TaskCallback.OnUnsubscribedAsync();
            return Results;
        }

        /// <summary>
        /// メッセージ送信
        /// </summary>
        /// <param name="ChannelName">チャンネル名</param>
        /// <param name="Message">メッセージ</param>
        public void PublishMessage(string ChannelName, object Message)
        {
            if (Client == null) { throw new Exception("Client is null!"); }
            Client.PublishMessage(ChannelName, Message);
        }

        /// <summary>
        /// プライベートメッセージ送信
        /// </summary>
        /// <param name="Target">対象</param>
        /// <param name="Message">メッセージ</param>
        public void SendPrivateMessage(string Target, object Message)
        {
            if (Client == null) { throw new Exception("Client is null!"); }
            Client.SendPrivateMessage(Target, Message);
        }

        /// <summary>
        /// ステータス変更
        /// </summary>
        /// <param name="Status">ステータス</param>
        /// <param name="Message">メッセージ</param>
        public void SetOnlineStatus(int Status, string Message = null)
        {
            if (Client == null) { throw new Exception("Client is null!"); }
            if (string.IsNullOrEmpty(Message))
            {
                Client.SetOnlineStatus(Status);
            }
            else
            {
                Client.SetOnlineStatus(Status, Message);
            }
        }

        /// <summary>
        /// 切断
        /// </summary>
        public async UniTask Disconnect(CancellationToken Token = default)
        {
            if (Client == null) { return; }

            Client.Disconnect();
            await TaskCallback.OnDisconnectedAsync().AttachExternalCancellation(Token);

            Client = null;
        }

        #region Singleton
        public static ChatToTaskNetwork Instance { get { return _Instance; } }
        private static ChatToTaskNetwork _Instance = new ChatToTaskNetwork();
        private ChatToTaskNetwork() { }
        #endregion
    }
}
