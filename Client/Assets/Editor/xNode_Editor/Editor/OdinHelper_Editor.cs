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
                target.Clear();

                // get path by targetObject.name
                var rootsRaw = serializedObject.targetObject.name.Split('.').Last();
                Debug.Log(rootsRaw.Split('_').Last());
                var skillId = int.Parse(rootsRaw.Split('_').Last());
                
                // load json and mapping
                var skillNode = LubanHelper.GetSkillNodeById(skillId);

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
                target.Clear();

                // get path by targetObject.name
                var buffString = serializedObject.targetObject.name.Split('.').Last();
                var buffId = int.Parse(buffString.Replace("buffId_", "").Trim());
                
                // load json
                var buffNode = LubanHelper.GetBuffNodeById(buffId);
                
                var node = target.CreateNode(buffNode,0,0);
                
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
    
    [UnityEditor.CustomEditor(typeof(UnitGraph), true)]
    public class Odin_UnitGraphEditor : OdinEditor {
        public override void OnInspectorGUI() {
            if (GUILayout.Button("读取Json并编辑单位信息", GUILayout.Height(40)))
            {
                // clear nodes
                var target = serializedObject.targetObject as UnitGraph;
                target.Clear();

                // get path by targetObject.name
                var rootsRaw = serializedObject.targetObject.name.Split('.').Last();
                var unitId = int.Parse(rootsRaw.Split('_').Last());
                
                // load json and mapping
                var unitNode = LubanHelper.GetUnitNodeById(unitId);

                // create unit
                target.CreateNode(unitNode,0,0);
                
                // create active skills
                foreach (var skillId in unitNode.skillArr)
                {
                    var skillNode = LubanHelper.GetSkillNodeById(skillId);
                    if (skillNode == null)
                    {
                        continue;
                    }
                    
                    target.CreateNode(skillNode,NodeHelper.SkillPaddingWidth,-500);
                    
                    // create buffs
                    foreach (var buff in skillNode.buffIdArr)
                    {
                        var buffNode = LubanHelper.GetBuffNodeById(buff);
                        if (buffNode == null)
                        {
                            continue;
                        }
                        target.CreateNode(buffNode,NodeHelper.SkillPaddingWidth + NodeHelper.BuffPaddingWidth,-800);
                    }
                }
                
                // create psv skills
                foreach (var skillId in unitNode.pasvBuff)
                {
                    var skillNode = LubanHelper.GetSkillNodeById(skillId);
                    if (skillNode == null)
                    {
                        continue;
                    }
                    
                    target.CreateNode(skillNode,NodeHelper.SkillPaddingWidth,-500);
                    
                    // create buffs
                    foreach (var buff in skillNode.buffIdArr)
                    {
                        var buffNode = LubanHelper.GetBuffNodeById(buff);
                        if (buffNode == null)
                        {
                            continue;
                        }
                        target.CreateNode(buffNode,NodeHelper.SkillPaddingWidth + NodeHelper.BuffPaddingWidth,-800);
                    }
                }
                
                NodeEditorWindow.Open(target);
            }
            
            if (GUILayout.Button("保存单位信息", GUILayout.Height(40))) {
                // get nodes
                var graph = serializedObject.targetObject as UnitGraph;
                foreach (var node in graph.nodes)
                {
                    if (node is UnitNode un)
                    {
                        un.SaveUnitNode();
                    }
                    else if (node is SkillNode sn)
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
}