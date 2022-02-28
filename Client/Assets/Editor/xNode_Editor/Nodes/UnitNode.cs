using XNode;

namespace Editor.xNode_Editor
{
    [NodeWidth(NodeHelper.UnitWidth)]
    public class UnitNode : Node
    {
        public int unitId;
        public string unitName;//单位角色名
        public string resName;//资源

   
        // 核心属性
        public int hp;
        public int def;
        public int moveSpeed;

        public UnitTypeEnum colliderType;
        // public PEInt hitHeight;// 被子弹命中的高度
        // public ColliderConfig colliCfg;// 碰撞体
   
        // 技能Id数组
        /// <summary>
        /// 被动技能buff
        /// </summary>
        [Node.OutputAttribute(dynamicPortList = true)]public int[] pasvBuff;
        [Node.OutputAttribute(dynamicPortList = true)]public int[] skillArr;
    }
}