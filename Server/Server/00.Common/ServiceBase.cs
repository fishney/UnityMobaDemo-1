using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
	public abstract class ServiceBase<T> : Singleton<T> where T : ServiceBase<T>
	{
		protected ServiceBase() { }

		public override void Init()
		{
			base.Init();
			this.Log($"{GetType().Name} init.");
		}

		public override void Update()
		{
			base.Update();
		}

	}
}
