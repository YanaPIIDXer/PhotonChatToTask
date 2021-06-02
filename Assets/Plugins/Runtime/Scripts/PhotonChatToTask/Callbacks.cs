using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using ExitGames.Client.Photon;
using Cysharp.Threading.Tasks;

namespace PhotonChatToTask
{

    /// <summary>
    /// 購読結果
    /// TODO:別のソースに移した方がいいんじゃね？
    /// </summary>
    public struct SubscribeResult
    {
        /// <summary>
        /// チャンネル名
        /// </summary>
        public string ChannelName;

        /// <summary>
        /// 結果
        /// </summary>
        public bool Result;
    }

    /// <summary>
    /// コールバック
    /// </summary>
    public class Callbacks : MonoBehaviour, IChatClientListener
    {
        /// <summary>
        /// インスタンス
        /// </summary>
        public static Callbacks Instance
        {
            get
            {
                if (_Instance == null)
                {
                    var Obj = new GameObject("PhotonChatCallbacks");
                    DontDestroyOnLoad(Obj);
                    _Instance = Obj.AddComponent<Callbacks>();

                }
                return _Instance;
            }
        }
        private static Callbacks _Instance = null;

        /// <summary>
        /// イベントリスナ
        /// </summary>
        public IClientListener EventListener { set; private get; } = null;

        #region OnConnected

        private AsyncReactiveProperty<AsyncUnit> OnConnectedProp = null;

        public UniTask OnConnectedAsync
        {
            get
            {
                if (OnConnectedProp == null)
                {
                    OnConnectedProp = new AsyncReactiveProperty<AsyncUnit>(AsyncUnit.Default);
                    OnConnectedProp.AddTo(this.GetCancellationTokenOnDestroy());
                }
                return OnConnectedProp.WaitAsync();
            }
        }

        public void OnConnected()
        {
            if (OnConnectedProp != null)
            {
                OnConnectedProp.Value = AsyncUnit.Default;
            }
        }

        #endregion

        #region OnDisconnected

        private AsyncReactiveProperty<AsyncUnit> OnDisconnectedProp = null;

        public UniTask OnDisconnectedAsync
        {
            get
            {
                if (OnDisconnectedProp == null)
                {
                    OnDisconnectedProp = new AsyncReactiveProperty<AsyncUnit>(AsyncUnit.Default);
                    OnDisconnectedProp.AddTo(this.GetCancellationTokenOnDestroy());
                }
                return OnDisconnectedProp.WaitAsync();
            }
        }

        public void OnDisconnected()
        {
            if (OnDisconnectedProp != null)
            {
                OnDisconnectedProp.Value = AsyncUnit.Default;
            }
        }

        #endregion

        #region OnSubscribed
        private AsyncReactiveProperty<SubscribeResult[]> OnSubscribedProp = null;
        private List<SubscribeResult> SubscribeResults = new List<SubscribeResult>();
        private int SubscribeCount = 0;

        public UniTask<SubscribeResult[]> OnSubscribedAsync(int SubscribeCount)
        {
            if (OnSubscribedProp == null)
            {
                OnSubscribedProp = new AsyncReactiveProperty<SubscribeResult[]>(null);
                OnSubscribedProp.AddTo(this.GetCancellationTokenOnDestroy());
            }
            SubscribeResults.Clear();
            this.SubscribeCount = SubscribeCount;
            return OnSubscribedProp.WaitAsync();
        }

        public void OnSubscribed(string[] channels, bool[] results)
        {
            if (OnSubscribedProp != null)
            {
                for (int i = 0; i < channels.Length; i++)
                {
                    SubscribeResults.Add(new SubscribeResult()
                    {
                        ChannelName = channels[i],
                        Result = results[i]
                    });
                }
                // コールバック１発で全部帰ってくるとは限らないようなのでこういう作りになっている
                if (SubscribeResults.Count >= SubscribeCount)
                {
                    OnSubscribedProp.Value = SubscribeResults.ToArray();
                }
            }
        }
        #endregion

        #region OnUnsubscribed

        private AsyncReactiveProperty<string[]> OnUnsubscribedProp = null;
        public UniTask<string[]> OnUnsubscribedAsync
        {
            get
            {
                if (OnUnsubscribedProp == null)
                {
                    OnUnsubscribedProp = new AsyncReactiveProperty<string[]>(null);
                    OnUnsubscribedProp.AddTo(this.GetCancellationTokenOnDestroy());
                }
                return OnUnsubscribedProp.WaitAsync();
            }
        }

        public void OnUnsubscribed(string[] channels)
        {
            if (OnUnsubscribedProp != null)
            {
                OnUnsubscribedProp.Value = channels;
            }
        }
        #endregion

        public void OnGetMessages(string channelName, string[] senders, object[] messages)
        {
            EventListener.OnGetMessages(channelName, senders, messages);
        }

        public void OnChatStateChange(ChatState state)
        {
        }

        public void OnPrivateMessage(string sender, object message, string channelName)
        {
            EventListener.OnPrivateMessage(sender, message, channelName);
        }

        public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
        {
            EventListener.OnStatusUpdate(user, status, gotMessage, message);
        }

        public void OnUserSubscribed(string channel, string user)
        {
            EventListener.OnUserSubscribed(channel, user);
        }

        public void OnUserUnsubscribed(string channel, string user)
        {
            EventListener.OnUserUnsubscribed(channel, user);
        }

        public void DebugReturn(DebugLevel level, string message)
        {
            EventListener.DebugReturn(level, message);
        }
    }
}
