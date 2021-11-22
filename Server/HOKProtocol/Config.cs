using System;
using System.Collections.Generic;
using System.Text;

namespace HOKProtocol
{
	public class ServerConfig
	{
		public const string LocalDevInnerIp = "127.0.0.1";
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
    }
}
