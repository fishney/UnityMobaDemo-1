namespace Editor.xNode_Editor
{
    [CreateNodeMenu("new BuffNode/MoveSpeedBuffNode"), NodeWidth(NodeHelper.BuffWidth)]
    public class MoveSpeedBuffNode : BuffNode
    {
        public int amount;
    }
}