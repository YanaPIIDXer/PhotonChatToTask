using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using ExitGames.Client.Photon;

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

        public void DebugReturn(DebugLevel level, string message)
        {
        }

        public void OnChatStateChange(ChatState state)
        {
        }

        public void OnConnected()
        {
        }

        public void OnDisconnected()
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
    }
}
