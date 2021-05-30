using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PhotonChatToTask;
using Photon.Chat;
using Cysharp.Threading.Tasks;
using System;

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
                try
                {
                    var ConnectTask = ChatToTaskNetwork.Instance.Connect(Envs.AppID, "1.0", this.GetCancellationTokenOnDestroy());
                    await ConnectTask;
                    Debug.Log("Hello, PhotonChat!");
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                    Btn.interactable = true;
                }
            });
        }
    }
}
