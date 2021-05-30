using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;

namespace PhotonChatToTask
{
    /// <summary>
    /// ChatClient.Serviceを呼び出し続けるオブジェクト
    /// </summary>
    public class ClientService : MonoBehaviour
    {
        /// <summary>
        /// Client
        /// </summary>
        private ChatClient Client = null;

        /// <summary>
        /// インスタンス
        /// </summary>
        private static ClientService _Instance = null;

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="Client">ChatClient</param>
        public static void Create(ChatClient Client)
        {
            if (_Instance == null)
            {
                var Obj = new GameObject("ClientService");
                DontDestroyOnLoad(Obj);
                _Instance = Obj.AddComponent<ClientService>();
            }
            _Instance.Client = Client;
        }

        void Update()
        {
            if (Client != null)
            {
                Client.Service();
            }
        }
    }
}
