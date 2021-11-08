using System;
using System.Collections.Generic;
using System.Text;
using HOKProtocol;

namespace Server
{
	public class MsgPack
	{
		public ServerSession session;
		public GameMsg msg;

		public MsgPack(ServerSession s, GameMsg m)
		{
			session = s;
			msg = m;
		}
    }
}
