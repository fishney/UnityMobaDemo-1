namespace Editor.xNode_Editor
{
    [CreateNodeMenu("new BuffNode/HouyiMixedMultiScatterBuffNode"), NodeWidth(NodeHelper.BuffWidth)]
    public class HouyiMixedMultiScatterBuffNode : BuffNode
    {
        public int scatterCount;
        
        public TargetCfg targetCfg;
        
        public int damagePct;
        
        public int arrowCount;

        public int arrowDelay;

         public float posOffset;
    }
}