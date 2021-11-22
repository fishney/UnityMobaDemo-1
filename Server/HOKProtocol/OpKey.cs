using System;
using System.Collections.Generic;
using System.Text;

namespace HOKProtocol
{
    [Serializable]
    public class OpKey
    {
        /// 玩家在队列对应的位置Index
        public int opIndex;
        public KeyType keyType;
        public MoveKey moveKey;
        public SkillKey skillKey;
        // TODO add ...
    }



    [Serializable]
    public class MoveKey
    {
        public int keyId;

        public long x;
        public long z;

    }

    [Serializable]
    public class SkillKey
    {
        public int skillId;

        public long x_val;
        public long z_val;

    }

    [Serializable]
    public enum KeyType
    {
        None,
        Move,
        Skill,

    }
}
