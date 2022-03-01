namespace Editor.xNode_Editor
{
    [CreateNodeMenu("new BuffNode/HouyiPasvAttackSpeedBuffNode"), NodeWidth(NodeHelper.BuffWidth)]
    public class HouyiPasvAttackSpeedBuffNode : BuffNode
    {
        public int overCount;

        public int speedAddtion;

        public int resetTime;
    }
}