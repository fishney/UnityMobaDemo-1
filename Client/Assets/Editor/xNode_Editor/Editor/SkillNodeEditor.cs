using NUnit.Framework.Internal;
using UnityEngine;
using XNodeEditor;

namespace Editor.xNode_Editor
{
    [CustomNodeEditor(typeof(SkillNode))]
    public class SkillNodeEditor : NodeEditor
    {
        public override void OnBodyGUI()
        {
            base.OnBodyGUI();

            foreach (var np in target.DynamicOutputs)
            {
                if (np.fieldName.Contains("buffIdArr "))
                {
                    if (np.IsConnected)
                    {
                        continue;
                    }
                    
                    var buffId = (int)np.GetOutputValue();
                    foreach (var node in target.graph.nodes)
                    {
                        if (node is BuffNode bn && bn.BuffId == buffId)
                        {
                            np.Connect(node.GetInputPort("BuffId"));
                        }
                    }
                }
                // np.GetOutputValue();
                // var test = np.fieldName;
            }

            
        }
    }
}