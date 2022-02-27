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
                skillNode.InitDatas(skillCfg);
                
                // create skill
                var node1 = target.CreateNode(skillNode,0,0);
                //var node2 = target.CreateNode(buffNode,NodeHelper.BuffPaddingWidth,-500);
                // var test = node1.Outputs;
                //var test2 = node2.Inputs;
                
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
                        // TODO Mapping
                        
                        // TODO SaveJson
                    }
                    else if (node is BuffNode bn)
                    {
                        // TODO switch buff
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
                var rootsRaw = serializedObject.targetObject.name.Split('.').ToList();
                string[] roots = new string[rootsRaw.Count - 1];
                for (int i = 1; i < rootsRaw.Count; i++)
                {
                    roots[i - 1] = rootsRaw[i];
                }
                
                // load json
                var buffCfg = new editor.cfg.MoveSpeedBuffCfg();
                if (!buffCfg.SimpleLoadJson(roots))
                {
                    return;
                }
                // mapping
                var buffNode = new MoveSpeedBuffNode();
                buffNode.BuffId = buffCfg.buffId;
                buffNode.buffName = buffCfg.buffName;
                // TODO 补齐Enum
                //buffNode.buffType = editor.cfg.BuffTypeEnum.GetByName(); 方案1.用Json源 方案2.直接用enum 方案3.直接用运行时enum。
                //buffNode.attacher
                //buffNode.staticPosType
                // TODO 补齐TargetCfg
                // buffNode.impacter = new TargetCfg()
                // {
                //     
                //     selectRange = buffCfg.impacter.selectRange,
                //     searchDis = buffCfg.impacter.searchDis,
                // };
                buffNode.buffDelay = buffCfg.buffDelay;
                buffNode.buffInterval = buffCfg.buffInterval;
                buffNode.buffDuration = buffCfg.buffDuration;
                buffNode.buffAudio = buffCfg.buffAudio;
                buffNode.buffEffect = buffCfg.buffEffect;
                buffNode.hitTickAudio = buffCfg.hitTickAudio;
                buffNode.amount = buffCfg.amount;
                buffNode.name = buffCfg.buffName;
                
                var node1 = target.CreateNode(buffNode,0,0);
                var node2 = target.CreateNode(buffNode,NodeHelper.BuffPaddingWidth,-500);
                var test = node1.Outputs;
                var test2 = node2.Inputs;
                
                NodeEditorWindow.Open(target);
            }
            if (GUILayout.Button("保存Buff", GUILayout.Height(40))) {
                //NodeEditorWindow.Open(serializedObject.targetObject as SkillGraph);
            }
            base.OnInspectorGUI();
        }
    }
}