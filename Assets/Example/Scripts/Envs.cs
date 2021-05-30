using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhotonChatToTask.Example
{
    /// <summary>
    /// Gitで管理してはいけない値を扱うクラス
    /// 値の実体は.gitignoreに放り込むEnvsDefine.cs内に定義するコンストラクタで放り込む
    /// </summary>
    public partial class Envs
    {
        /// <summary>
        /// AppID
        /// </summary>
        /// <value></value>
        public static string AppID { get; private set; } = "";

        private static Envs _Instance = new Envs();
    }
}
