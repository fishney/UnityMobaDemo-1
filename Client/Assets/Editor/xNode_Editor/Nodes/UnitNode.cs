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
        
        public override object GetValue(NodePort port)
        {
            if (port.fieldName.Contains("pasvBuff "))
            {
                var index = port.fieldName.GetIndex();
                if (index >= 0 && index < pasvBuff.Length)
                {
                    return pasvBuff[index];
                }
            }
            else if (port.fieldName.Contains("skillArr "))
            {
                var index = port.fieldName.GetIndex();
                if (index >= 0 && index < skillArr.Length)
                {
                    return skillArr[index];
                }
            }
            
            return null; // Replace this
        }
        
        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            if (from.fieldName.Contains("pasvBuff "))
            {
                var index = from.fieldName.GetIndex();
                if (index >= 0)
                {
                    pasvBuff[index] = (to.node as SkillNode).skillId;
                }
            }
            else if (from.fieldName.Contains("skillArr "))
            {
                var index = from.fieldName.GetIndex();
                if (index >= 0)
                {
                    skillArr[index] = (to.node as SkillNode).skillId;
                }
            }
        }
    }
}