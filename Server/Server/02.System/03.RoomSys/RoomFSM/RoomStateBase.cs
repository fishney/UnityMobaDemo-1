using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public interface IRoomState
    {
        void Enter();
        void Update();
        void Exit();
    }

    public abstract class RoomStateBase : IRoomState
    {
        public PVPRoom room;

        public RoomStateBase(PVPRoom r)
        {
            room = r;
        }

        public abstract void Enter();

        public abstract void Update();

        public abstract void Exit();
    }

    public enum RoomStateEnum
    {
        None,
        Confirm, // 确认匹配结果
        Select, // 选择英雄
        Load,
        Fight,
        End,
    }
}
