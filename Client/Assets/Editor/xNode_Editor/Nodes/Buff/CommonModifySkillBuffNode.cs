namespace Editor.xNode_Editor
{
    [CreateNodeMenu("new BuffNode/CommonModifySkillBuffNode"), NodeWidth(NodeHelper.BuffWidth)]
    public class CommonModifySkillBuffNode : BuffNode
    {
        public int originalID;
        public int replaceID;
    }
}