using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
	public abstract class SystemBase<T> : Singleton<T> where T : SystemBase<T>
	{
		protected SystemBase() { }


		protected NetSvc netSvc;
		protected CacheSvc cacheSvc;
		protected TimerSvc timerSvc;
		protected CfgSvc cfgSvc;

		public override void Init()
		{
			base.Init();

			netSvc = NetSvc.Instance();
			cacheSvc = CacheSvc.Instance();
			timerSvc = TimerSvc.Instance();
            cfgSvc = CfgSvc.Instance();

			this.Log($"{GetType().Name} init.");
		}

		public override void Update()
		{
			base.Update();
		}


	}
}
