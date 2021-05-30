using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PhotonChatToTask;
using Photon.Chat;
using Cysharp.Threading.Tasks;

namespace PhotonChatToTask.Example
{
    /// <summary>
    /// 接続ボタン
    /// </summary>
    public class ConnectionButton : MonoBehaviour
    {
        void Awake()
        {
            var Btn = GetComponent<Button>();
            Btn.onClick.AddListener(async () =>
            {
                Btn.interactable = false;
                await ChatToTaskNetwork.Instance.ConnectUsingSettings(new ChatAppSettings()
                {
                    AppIdChat = Envs.AppID
                }, this.GetCancellationTokenOnDestroy());
                Debug.Log("Hello, PhotonChat!");
            });
        }
    }
}
