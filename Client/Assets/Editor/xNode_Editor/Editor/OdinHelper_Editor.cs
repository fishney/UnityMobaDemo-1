using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Sirenix.OdinInspector.Editor;
using UnityEngine;
using XNodeEditor;

namespace Editor.xNode_Editor
{
    [UnityEditor.CustomEditor(typeof(SkillGraph), true)]
    public class Odin_SkillGraphEditor : OdinEditor {
        public override void OnInspectorGUI() {
            if (GUILayout.Button("读取Json并编辑技能", GUILayout.Height(40)))
            {
                // clear nodes
                var target = serializedObject.targetObject as SkillGraph;
                target.nodes.Clear();
                
                // get path by targetObject.name
                var rootsRaw = serializedObject.targetObject.name.Split('.').ToList();
                string[] roots = new string[rootsRaw.Count - 1];
                for (int i = 1; i < rootsRaw.Count; i++)
                {
                    roots[i - 1] = rootsRaw[i];
                }
                
                // load json
                var skillCfg = new editor.cfg.SkillCfg();
                if (!skillCfg.SimpleLoadJson(roots))
                {
                    return;
                }
                // mapping
                var skillNode = new SkillNode();
                skillNode.InitData(skillCfg);
                
                // create skill
                var node1 = target.CreateNode(skillNode,0,0);

                // create buffs
                foreach (var buff in skillNode.buffIdArr)
                {
                    var buffNode = LubanHelper.GetBuffNodeById(buff);
                    if (buffNode == null)
                    {
                        continue;
                    }
                    var node = target.CreateNode(buffNode,NodeHelper.BuffPaddingWidth,-500);
                    var test = skillNode.GetOutputPort("buffIdArr");
                    foreach (var np in skillNode.DynamicOutputs)
                    {
                        var tes123t = np;
                    }
                    //.Connect(node.GetPort("BuffId"));
                }

                NodeEditorWindow.Open(target);
            }
            
            if (GUILayout.Button("保存技能", GUILayout.Height(40))) {
                // get nodes
                var graph = serializedObject.targetObject as SkillGraph;
                foreach (var node in graph.nodes)
                {
                    if (node is SkillNode sn)
                    {
                        sn.SaveSkillNode();
                    }
                    else if (node is BuffNode bn)
                    {
                        bn.SaveBuffNode();
                    }
                }
            }
            base.OnInspectorGUI();
        }
    }
    
    [UnityEditor.CustomEditor(typeof(BuffGraph), true)]
    public class Odin_BuffGraphEditor : OdinEditor {
        public override void OnInspectorGUI() {
            if (GUILayout.Button("读取Json并编辑Buff", GUILayout.Height(40)))
            {
                // clear nodes
                var target = serializedObject.targetObject as BuffGraph;
                target.nodes.Clear();
                
                // get path by targetObject.name
                var buffString = serializedObject.targetObject.name.Split('.').Last();
                var buffId = int.Parse(buffString.Replace("buffId_", "").Trim());
                
                // load json
                var buffNode = LubanHelper.GetBuffNodeById(buffId);
                // TODO 补齐Enum buffNode.buffType = editor.cfg.BuffTypeEnum.GetByName(); 方案1.用Json源 方案2.直接用enum 方案3.直接用运行时enum。
                
                var node1 = target.CreateNode(buffNode,0,0);
                var node2 = target.CreateNode(buffNode,NodeHelper.BuffPaddingWidth,-500);
                var test = node1.Outputs;
                var test2 = node2.Inputs;
                
                NodeEditorWindow.Open(target);
            }
            
            if (GUILayout.Button("保存Buff", GUILayout.Height(40))) {
                var graph = serializedObject.targetObject as BuffGraph;
                foreach (var node in graph.nodes)
                { 
                    if (node is BuffNode bn)
                    {
                        bn.SaveBuffNode();
                    }
                }
            }
            base.OnInspectorGUI();
        }
    }
}