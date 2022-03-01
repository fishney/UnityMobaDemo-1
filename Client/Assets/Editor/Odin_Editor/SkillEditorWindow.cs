using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using editor.cfg;

namespace Editor.Odin_Editor
{
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
        tree.Selection.SupportsMultiSelect = false;
        
        // TODO 如果添加新buff，需要添加
        tree.Add("Buff/BuffCfg", new BuffCfgEditor<editor.cfg.BuffCfg>());
        tree.Add("Buff/ArthurMarkBuffCfg", new BuffCfgEditor<editor.cfg.ArthurMarkBuffCfg>());
        tree.Add("Buff/CommonModifySkillBuffCfg", new BuffCfgEditor<editor.cfg.CommonModifySkillBuffCfg>());
        tree.Add("Buff/DamageBuffCfg_DynamicGroup", new BuffCfgEditor<editor.cfg.DamageBuffCfg_DynamicGroup>());
        tree.Add("Buff/DamageBuffCfg_StaticGroup", new BuffCfgEditor<editor.cfg.DamageBuffCfg_StaticGroup>());
        tree.Add("Buff/ExecuteDamageBuffCfg", new BuffCfgEditor<editor.cfg.ExecuteDamageBuffCfg>());
        tree.Add("Buff/HouyiMixedMultiScatterBuffCfg", new BuffCfgEditor<editor.cfg.HouyiMixedMultiScatterBuffCfg>());
        tree.Add("Buff/HouyiMultipleArrowBuffCfg", new BuffCfgEditor<editor.cfg.HouyiMultipleArrowBuffCfg>());
        tree.Add("Buff/HouyiMultipleSkillModifyBuffCfg", new BuffCfgEditor<editor.cfg.HouyiMultipleSkillModifyBuffCfg>());
        tree.Add("Buff/HouyiPasvAttackSpeedBuffCfg", new BuffCfgEditor<editor.cfg.HouyiPasvAttackSpeedBuffCfg>());
        tree.Add("Buff/HouyiScatterArrowBuffCfg", new BuffCfgEditor<editor.cfg.HouyiScatterArrowBuffCfg>());
        tree.Add("Buff/HouyiScatterSkillModifyBuffCfg", new BuffCfgEditor<editor.cfg.HouyiScatterSkillModifyBuffCfg>());
        tree.Add("Buff/HPCureBuffCfg", new BuffCfgEditor<editor.cfg.HPCureBuffCfg>());
        tree.Add("Buff/MoveSpeedBuffCfg", new BuffCfgEditor<editor.cfg.MoveSpeedBuffCfg>());
        tree.Add("Buff/StunBuffCfg_DynamicTime", new BuffCfgEditor<editor.cfg.StunBuffCfg_DynamicTime>());
        tree.Add("Buff/TargetFlashMoveBuffCfg", new BuffCfgEditor<editor.cfg.TargetFlashMoveBuffCfg>());
        tree.Add("Skill",new SkillCfgEditor());
        tree.Add("Unit",new UnitInfoCfgEditor());

        return tree;
    }

    private class UnitInfoCfgEditor
    {
        [VerticalGroup("数据"),TableList(ShowIndexLabels = true, HideToolbar = true, AlwaysExpanded = true)]
        public List<editor.cfg.UnitInfoCfg> cfgs;
        public UnitInfoCfgEditor()
        {
            cfgs = new List<editor.cfg.UnitInfoCfg>();
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
                cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/UnitCfg/unitId_{cfg.unitId}.json");
            }
        }
        
        [Button("加载数据"),HorizontalGroup("A")]
        public void Load()
        {
            string[] files = Directory.GetFiles($"{Application.dataPath}/../LubanGens/EditorJsonData/UnitCfg", "*.json");
            if (files?.Length == 0)
            {
                // TODO 错误弹窗
                return;
            }
            
            cfgs.Clear();
            foreach (var file in files)
            {
                var cfg = new editor.cfg.UnitInfoCfg();
                cfg.LoadJsonFile(file);
                cfgs.Add(cfg);
            }
            
            cfgs = cfgs.OrderBy(o => o.unitId).ToList();
        }
        
        [Button("新建"),HorizontalGroup("A"),GUIColor(1,0,1)]
        public void Create()
        {
            cfgs.Add(new editor.cfg.UnitInfoCfg());
        }
    }
    
    private class SkillCfgEditor
    {
        
        [VerticalGroup("数据"),TableList(ShowIndexLabels = true, HideToolbar = true, AlwaysExpanded = true)]
        public List<editor.cfg.SkillCfg> cfgs;
        public SkillCfgEditor()
        {
            cfgs = new List<editor.cfg.SkillCfg>();
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
                cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/SkillCfg/skillId_{cfg.skillId}.json");
            }
        }
        
        [Button("加载数据"),HorizontalGroup("A")]
        public void Load()
        {
            string[] files = Directory.GetFiles($"{Application.dataPath}/../LubanGens/EditorJsonData/SkillCfg", "*.json");
            if (files?.Length == 0)
            {
                // TODO 错误弹窗
                return;
            }
            
            cfgs.Clear();
            foreach (var file in files)
            {
                var cfg = new editor.cfg.SkillCfg();
                cfg.LoadJsonFile(file);
                cfgs.Add(cfg);
            }
            
            cfgs = cfgs.OrderBy(o => o.skillId).ToList();
        }
        
        [Button("新建"),HorizontalGroup("A"),GUIColor(1,0,1)]
        public void Create()
        {
            cfgs.Add(new editor.cfg.SkillCfg());
        }
    }
    
    private class BuffCfgEditor<T> where T : editor.cfg.BuffCfg, new()
    {
        [VerticalGroup("数据"),TableList(ShowIndexLabels = true, HideToolbar = true, AlwaysExpanded = true)]
        public List<T> cfgs;
        public BuffCfgEditor()
        {
            cfgs = new List<T>();
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
                cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/BuffCfg/{typeof(T).Name}/buffId_{cfg.buffId}.json");
            }
        }
        
        [Button("加载数据"),HorizontalGroup("A")]
        public void Load()
        {
            string[] files = Directory.GetFiles($"{Application.dataPath}/../LubanGens/EditorJsonData/BuffCfg/{typeof(T).Name}", "*.json");
            if (files?.Length == 0)
            {
                // TODO 错误弹窗
                return;
            }
            
            cfgs.Clear();
            foreach (var file in files)
            {
                var cfg = new T();
                cfg.LoadJsonFile(file);
                cfgs.Add(cfg);
            }
            
            cfgs = cfgs.OrderBy(o => o.buffId).ToList();
        }
        
        [Button("新建"),HorizontalGroup("A"),GUIColor(1,0,1)]
        public void Create()
        {
            cfgs.Add(new T());
        }
    }
}  
}
