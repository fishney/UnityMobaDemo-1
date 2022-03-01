namespace Editor.xNode_Editor
{
    [CreateNodeMenu("new BuffNode/HPCureBuffNode"), NodeWidth(NodeHelper.BuffWidth)]
    public class HPCureBuffNode : BuffNode
    {
        public int cureHPpct;
    }
}