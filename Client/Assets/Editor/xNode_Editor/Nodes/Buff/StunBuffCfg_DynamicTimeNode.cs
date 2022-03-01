namespace Editor.xNode_Editor
{
    [CreateNodeMenu("new BuffNode/StunBuffCfg_DynamicTimeNode"), NodeWidth(NodeHelper.BuffWidth)]
    public class StunBuffCfg_DynamicTimeNode : BuffNode
    {
        public int minStunTime;

        public int maxStunTime;
    }
}