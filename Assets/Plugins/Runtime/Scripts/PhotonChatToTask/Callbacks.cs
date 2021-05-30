using UnityEngine;
using Photon.Chat;
using ExitGames.Client.Photon;
using Cysharp.Threading.Tasks;

namespace PhotonChatToTask
{
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
            Debug.Log("OnConnected");
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
            Debug.LogError("OnDisconnected");
            if (OnDisconnectedProp != null)
            {
                OnDisconnectedProp.Value = AsyncUnit.Default;
            }
        }

        #endregion

        public void OnChatStateChange(ChatState state)
        {
        }
        public void OnGetMessages(string channelName, string[] senders, object[] messages)
        {
        }

        public void OnPrivateMessage(string sender, object message, string channelName)
        {
        }

        public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
        {
        }

        public void OnSubscribed(string[] channels, bool[] results)
        {
        }

        public void OnUnsubscribed(string[] channels)
        {
        }

        public void OnUserSubscribed(string channel, string user)
        {
        }

        public void OnUserUnsubscribed(string channel, string user)
        {
        }

        public void DebugReturn(DebugLevel level, string message)
        {
            Debug.Log(message);
        }
    }
}
