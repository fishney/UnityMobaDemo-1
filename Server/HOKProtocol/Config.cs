using System;
using System.Collections.Generic;
using System.Text;

namespace proto.HOKProtocol
{
	public class ServerConfig
	{
        /// <summary>
        /// 本地IP
        /// </summary>
		public const string LocalDevInnerIp = "127.0.0.1";

        /// <summary>
        /// 远端IP（公网）
        /// </summary>
        public const string RemoteGateIp = "47.102.42.164";

        /// <summary>
        /// 云服务器IP 远端转发绑定IP（私网）
        /// </summary>
        public const string RemoteServerIp = "172.22.18.183";

        public const int UdpPort = 17666;

		// 确认匹配倒计时
        public const int ConfirmCountDown = 15;

        // 选择英雄倒计时
        public const int SelectCountDown = 15;
    }

    public class Configs
    {
        /// 逻辑帧事件 66ms = 1秒15帧
        public const float ClientLogicFromDeltaSec = 0.066f;
        // 逻辑帧事件 66ms
        public const int ServerLogicFrameIntervelMs = 66;
    }
}
