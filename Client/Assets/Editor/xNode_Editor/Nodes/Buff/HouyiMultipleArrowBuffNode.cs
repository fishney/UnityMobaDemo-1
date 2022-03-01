namespace Editor.xNode_Editor
{
    [CreateNodeMenu("new BuffNode/HouyiMultipleArrowBuffNode"), NodeWidth(NodeHelper.BuffWidth)]
    public class HouyiMultipleArrowBuffNode : BuffNode
    {
        public int arrowCount;
        public int arrowDelay;
        public float posOffset;
    }
}