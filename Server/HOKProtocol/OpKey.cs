using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;

namespace HOKProtocol
{
    [Serializable]
    [ProtoContract]
    public class OpKey
    {
        // 玩家在队列对应的位置Index
        [ProtoMember(1)]
        public int opIndex;
        [ProtoMember(2)]
        public KeyType keyType;
        [ProtoMember(3)]
        public MoveKey moveKey;
        [ProtoMember(4)]
        public SkillKey skillKey;
        // TODO add ...
    }



    [Serializable]
    [ProtoContract]
    public class MoveKey
    {
        [ProtoMember(1)]
        public int keyId;

        [ProtoMember(2)]
        public long x;
        [ProtoMember(3)]
        public long z;

    }

    [Serializable]
    [ProtoContract]
    public class SkillKey
    {
        [ProtoMember(1)]
        public int skillId;

        [ProtoMember(2)]
        public long x_val;
        [ProtoMember(3)]
        public long z_val;

    }

    [Serializable]
    [ProtoContract]
    public enum KeyType
    {
        None,
        Move,
        Skill,
    }
}
