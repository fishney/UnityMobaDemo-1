namespace Editor.xNode_Editor
{
    [CreateNodeMenu("new BuffNode/ExecuteDamageBuffNode"), NodeWidth(NodeHelper.BuffWidth)]
    public class ExecuteDamageBuffNode : BuffNode
    {
        public int damagePct;
    }
}