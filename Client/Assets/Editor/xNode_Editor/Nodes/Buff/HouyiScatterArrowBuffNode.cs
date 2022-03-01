namespace Editor.xNode_Editor
{
    [CreateNodeMenu("new BuffNode/HouyiScatterArrowBuffNode"), NodeWidth(NodeHelper.BuffWidth)]
    public class HouyiScatterArrowBuffNode : BuffNode
    {
        public int scatterCount;

        public TargetCfg targetCfg;

        public int damagePct;
    }
}