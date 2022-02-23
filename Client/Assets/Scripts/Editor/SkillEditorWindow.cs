using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using editor.cfg;

public class SkillEditorWindow: OdinMenuEditorWindow
{
    [MenuItem("My Tool/Skill Editor Window")]
    private static void OpenWindow()
    {
        GetWindow<SkillEditorWindow>().Show();
    }
    
    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();
        tree.DefaultMenuStyle = OdinMenuStyle.TreeViewStyle;

        tree.Add("BuffCfg", new BuffCfgEditor());

        return tree;
    }
    
    private class BuffCfgEditor
    {
        [VerticalGroup("数据"),TableList(ShowIndexLabels = true)]
        public List<editor.cfg.BuffCfg> cfgs;
        public BuffCfgEditor()
        {
            cfgs = new List<editor.cfg.BuffCfg>();
        }
        
        [Button("新建")]
        public void Create()
        {
            var nextId = cfgs == null || cfgs.Count < 1 ? 0 : cfgs.Max(o=>o.buffId) + 1;
            cfgs.Add(new editor.cfg.BuffCfg()
            {
                buffId = nextId,
            });
        }

        [Button("保存数据")]
        public void Save()
        {
            if (cfgs?.Count == 0)
            {
                // TODO 错误弹窗
                return;
            }
            
            foreach (var cfg in cfgs)
            {
                cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/BuffCfg/buffId_{cfg.buffId}.json");
            }
        }
        
        [Button("加载数据")]
        public void Load()
        {
            string[] files = Directory.GetFiles($"{Application.dataPath}/../LubanGens/EditorJsonData/BuffCfg", "*.json");
            if (files?.Length == 0)
            {
                // TODO 错误弹窗
                return;
            }
            
            cfgs.Clear();
            foreach (var file in files)
            {
                var cfg = new editor.cfg.BuffCfg();
                cfg.LoadJsonFile(file);
                cfgs.Add(cfg);
            }

            cfgs = cfgs.OrderBy(o => o.buffId).ToList();
        }
    }
}