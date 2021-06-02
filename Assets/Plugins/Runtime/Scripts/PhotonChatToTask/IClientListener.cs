using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using ExitGames.Client.Photon;

namespace PhotonChatToTask
{
    /// <summary>
    /// イベントリスナ
    /// async/awaitでは対応し切れないコールバックに対応するためのもの
    /// </summary>
    public interface IClientListener
    {
        void OnGetMessages(string channelName, string[] senders, object[] messages);
        void DebugReturn(DebugLevel level, string message);
        void OnStatusUpdate(string user, int status, bool gotMessage, object message);
        void OnUserSubscribed(string channel, string user);
        void OnUserUnsubscribed(string channel, string user);
    }
}
