using NUnit.Framework.Internal;
using UnityEngine;
using XNodeEditor;

namespace Editor.xNode_Editor
{
    [CustomNodeEditor(typeof(UnitNode))]
    public class UnitNodeEditor : NodeEditor
    {
        public override void OnBodyGUI()
        {
            base.OnBodyGUI();

            foreach (var np in target.DynamicOutputs)
            {
                if (np.fieldName.Contains("pasvBuff ") || np.fieldName.Contains("skillArr "))
                {
                    if (np.IsConnected)
                    {
                        continue;
                    }
                    
                    var skillId = (int)np.GetOutputValue();
                    foreach (var node in target.graph.nodes)
                    {
                        if (node is SkillNode sn && sn.skillId == skillId)
                        {
                            np.Connect(node.GetInputPort("skillId"));
                        }
                    }
                }
            }
        }
        
    }
}