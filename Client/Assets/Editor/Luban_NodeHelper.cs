using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Bright.Config;
using Editor.xNode_Editor;
using UnityEngine;
using editor.cfg;
using XNode;

namespace Editor
{
    public static class LubanHelper
    {

        public static bool SimpleLoadJson<T>(this T jsonInstance,string[] roots) where T : EditorBeanBase
        {
            if (roots?.Length < 1)
            {
                return false;
            }
            StringBuilder pathBuilder = new StringBuilder();
            pathBuilder.Append($"{Application.dataPath}/../LubanGens/EditorJsonData/");
            
            foreach (var root in roots)
            {
                pathBuilder.Append(root);
                pathBuilder.Append("/");
            }
            pathBuilder.Remove(pathBuilder.Length - 1, 1);
            pathBuilder.Append(".json");
            var path = pathBuilder.ToString();
            
            if (File.Exists(path))
            {
                jsonInstance.LoadJsonFile(path);
            }
            else
            {
                Debug.LogWarning("cant find file in path:"+path);
                return false;
            }
            
            return true;
        }
        
        

        public static BuffNode GetBuffNodeById(int buffId)
        {
            var findingFile = "buffId" + buffId + ".json";
            StringBuilder pathBuilder = new StringBuilder();
            pathBuilder.Append($"{Application.dataPath}/../LubanGens/EditorJsonData/BuffCfg");
            var root = pathBuilder.ToString();
            var dicPaths = Directory.GetDirectories(root);
            foreach (var dicPath in dicPaths)
            {
                var tmpFilePath = dicPath + "/" + findingFile;
                if (File.Exists(tmpFilePath))
                {
                    var typeString = dicPath.Substring(dicPath.LastIndexOf(@"\") + 1); 
                    return GetBuffNode(typeString, tmpFilePath);
                }
            }

            return null;
        }

        public static BuffNode GetBuffNode(string name, string jsonFilePath)
        {
            if (name == "MoveSpeedBuffCfg")
            {
                var cfg = new editor.cfg.MoveSpeedBuffCfg();
                cfg.LoadJsonFile(jsonFilePath);
                var node = new MoveSpeedBuffNode();
                node.InitDatas(cfg);
                // TODO 特殊属性
                node.amount = cfg.amount;
                return node;
            }
            
            return null;
        }
        
        
    }

    public static class NodeHelper
    {
        #region graph calc const
        
        public const int PaddingWidth = 80; 
        public const int PaddingHeight = 50;
        public const int BuffWidth = 300;
        public const int BuffHeight = 500;
        public const int BuffPaddingWidth = BuffWidth + PaddingWidth;
        public const int BuffPaddingHeight = BuffHeight + PaddingHeight;
        
        #endregion

        public static void InitDatas(this SkillNode skillNode, editor.cfg.SkillCfg skillCfg)
        {
            skillNode.skillId = skillCfg.skillId; 
            skillNode.iconName = skillCfg.iconName; 
            skillNode.aniName = skillCfg.aniName;
            skillNode.audio_start = skillCfg.audio_start;
            skillNode.audio_work = skillCfg.audio_work;
            skillNode.audio_hit = skillCfg.audio_hit;
            skillNode.cdTime = skillCfg.cdTime;
            skillNode.spellTime = skillCfg.spellTime;
            skillNode.damage = skillCfg.damage;
            skillNode.buffIdArr = skillCfg.buffIdArr;
            skillNode.isNormalAttack = skillCfg.isNormalAttack;
            // TODO skillNode.releaseMode = skillCfg.releaseMode;
            // TODO skillNode.targetCfg = skillCfg.targetCfg;
            // TODO skillNode.bulletCfg = skillCfg.bulletCfg;
        }
        
        public static void InitDatas(this BuffNode node, editor.cfg.BuffCfg cfg)
        {
            node.BuffId = cfg.buffId;
            node.buffName = cfg.buffName;
            // node.buffType = cfg.buffType;
            // node.attacher = cfg.attacher;
            // node.staticPosType = cfg.staticPosType;
            // node.impacter = cfg.impacter;
            node.buffDelay = cfg.buffDelay;
            node.buffInterval = cfg.buffInterval;
            node.buffDuration = cfg.buffDuration;
            node.buffAudio = cfg.buffAudio;
            node.buffEffect = cfg.buffEffect;
            node.hitTickAudio = cfg.hitTickAudio;
        }
    }
}