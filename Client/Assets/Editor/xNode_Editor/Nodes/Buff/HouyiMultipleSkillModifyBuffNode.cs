namespace Editor.xNode_Editor
{
    [CreateNodeMenu("new BuffNode/HouyiMultipleSkillModifyBuffNode"), NodeWidth(NodeHelper.BuffWidth)]
    public class HouyiMultipleSkillModifyBuffNode : BuffNode
    { 
        public int originalID;
        
        public int powerID;

        public int superPowerID;
        
        public int triggerOverCount;
        
        public int resetTime;
    }
}