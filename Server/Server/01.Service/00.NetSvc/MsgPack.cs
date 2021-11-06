using System;
using System.Collections.Generic;
using System.Text;
using HOKProtocol;

namespace Server
{
	public class MsgPack
	{
		public ServerSession session;
		public HokMsg msg;

		public MsgPack(ServerSession s, HokMsg m)
		{
			session = s;
			msg = m;
		}
    }
}
