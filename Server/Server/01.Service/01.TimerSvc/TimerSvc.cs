using System;
using System.Collections.Generic;
using System.Text;
using PETimer;

namespace Server
{
	public class TimerSvc : ServiceBase<TimerSvc>
    {
        private TickTimer timer = new TickTimer(0,false);

		private TimerSvc() { }

		public override void Init()
		{
			base.Init();

            timer.LogFunc = this.Log;
			timer.WarnFunc = this.Warn;
            timer.ErrorFunc = this.Error;
        }

		public override void Update()
		{
			base.Update();

            timer.UpdateTask();
		
        }

        /// <summary>
        /// 添加定时回调任务
        /// </summary>
        /// <param name="delay">延迟时长 毫秒</param>
        /// <param name="taskCB">回调函数</param>
        /// <param name="cancelCB">取消回调函数</param>
        /// <param name="count">执行次数</param>
        /// <returns></returns>
        public int AddTask(uint delay,Action<int> taskCB,Action<int> cancelCB = null,int count = 1)
        {
            return timer.AddTask(delay,taskCB,cancelCB,count);
        }

        public bool DeleteTask(int tid)
        {
            return timer.DeleteTask(tid);
        }
	}
}
