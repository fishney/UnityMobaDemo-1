namespace Editor.xNode_Editor
{
    [CreateNodeMenu("new BuffNode/DamageBuffCfg_StaticGroupNode"), NodeWidth(NodeHelper.BuffWidth)]
    public class DamageBuffCfg_StaticGroupNode : BuffNode
    {
        public int damage;
    }
}