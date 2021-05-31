using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PhotonChatToTask;
using Photon.Chat;
using Cysharp.Threading.Tasks;
using System;
using ExitGames.Client.Photon;

namespace PhotonChatToTask.Example
{
    /// <summary>
    /// テストボタン
    /// </summary>
    public class TestButton : MonoBehaviour, IClientListener
    {
        void Awake()
        {
            var Btn = GetComponent<Button>();
            Btn.onClick.AddListener(async () =>
            {
                Btn.interactable = false;
                try
                {
                    var Token = this.GetCancellationTokenOnDestroy();

                    // 接続
                    await ChatToTaskNetwork.Instance.Connect(Envs.AppID, "1.0", this, Token);
                    Debug.Log("Hello, PhotonChat!");
                    await UniTask.Delay(3000);

                    // チャンネル購読
                    var SubscribeResult = await ChatToTaskNetwork.Instance.SubscribeChanel(new string[]
                    {
                        "Channel1",
                        "Channel2",
                        "Channel3"
                    }, Token);
                    foreach (var Result in SubscribeResult)
                    {
                        Debug.Log(Result.ChannelName + ":" + Result.Result);
                    }
                    await UniTask.Delay(3000);

                    // チャンネル購読解除
                    var UnsubscribeResult = await ChatToTaskNetwork.Instance.UnsubscribeChannel(new string[]
                    {
                        "Channel2",
                        "Channel3"
                    }, Token);
                    foreach (var Result in UnsubscribeResult)
                    {
                        Debug.Log("Unsubscribe:" + Result);
                    }
                    await UniTask.Delay(3000);

                    // 切断
                    await ChatToTaskNetwork.Instance.Disconnect(Token);
                    Debug.Log("Goodbye, PhotonChat!");
                    Btn.interactable = true;
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                    Btn.interactable = true;
                }
            });
        }

        public void DebugReturn(DebugLevel level, string message)
        {
            switch (level)
            {
                case DebugLevel.ERROR:

                    Debug.LogError(message);
                    break;

                case DebugLevel.WARNING:

                    Debug.LogWarning(message);
                    break;

                default:

                    Debug.Log(message);
                    break;
            }
        }
    }
}
