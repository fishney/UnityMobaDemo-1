using System;
using System.Collections.Generic;
using System.Text;
using PEUtils;

namespace Server
{
	public class ServerRoot : Singleton<ServerRoot>
	{
		private ServerRoot(){}
		private int sessionId = 0;

		public override void Init()
		{
			base.Init();

			// Log
			PELog.InitSettings();

			// Service
			NetSvc.Instance().Init();
			TimerSvc.Instance().Init();
			CacheSvc.Instance().Init();

			// System
			LoginSys.Instance().Init();
			MatchSys.Instance().Init();
			RoomSys.Instance().Init();

			this.ColorLog(LogColor.Green,"ServerRoot init Completed.");
		}

		public override void Update()
		{
			base.Update();

			// Service
			NetSvc.Instance().Update();
			TimerSvc.Instance().Update();
			CacheSvc.Instance().Update();

			// System
			LoginSys.Instance().Update();
			MatchSys.Instance().Update();
			RoomSys.Instance().Update();

		}

		/// <summary>
		/// 分配新的SessionId给客户端
		/// </summary>
		public int GetSessionId()
		{
			return sessionId == int.MaxValue ? 0 : sessionId++;
		}


	}
}
