using UnityEngine;
using XNodeEditor;

namespace Editor.xNode_Editor
{
    [NodeGraphEditor.CustomNodeGraphEditorAttribute(typeof(SkillGraph))]
    public class SkillGraphEditor : NodeGraphEditor
    {
        public override void OnGUI()
        {
            base.OnGUI();
            // GUILayout.BeginHorizontal();
            // if (GUILayout.Button("读取Json数据",GUILayout.Height(20), GUILayout.Width(300)))
            // {
            //     
            // }
            // if (GUILayout.Button("保存Json数据",GUILayout.Height(20), GUILayout.Width(300)))
            // {
            //     
            // }
            // GUILayout.EndHorizontal();
        }
    
    }
}