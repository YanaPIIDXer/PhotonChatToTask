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
        void DebugReturn(DebugLevel level, string message);
    }
}
