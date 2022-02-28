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


        #region Buff Node

        public static BuffNode GetBuffNodeById(int buffId)
        {
            var findingFile = "buffId_" + buffId + ".json";
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

        public static void SaveBuffNode(this BuffNode bn)
        {
            var buffType = bn.GetType().Name;
            if (buffType == "MoveSpeedBuffNode")
            {
                var cfg = new editor.cfg.MoveSpeedBuffCfg();
                cfg.InitData(bn);
                // TODO 特殊属性
                cfg.amount = ((MoveSpeedBuffNode) bn).amount;
                cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/BuffCfg/{cfg.GetType().Name}/buffId{cfg.buffId}.json");
            }
        }

        public static BuffNode GetBuffNode(string buffType, string jsonFilePath)
        {
            if (buffType == "MoveSpeedBuffCfg")
            {
                var cfg = new editor.cfg.MoveSpeedBuffCfg();
                cfg.LoadJsonFile(jsonFilePath);
                var node = new MoveSpeedBuffNode();
                node.InitData(cfg);
                // TODO 特殊属性
                node.amount = cfg.amount;
                return node;
            }
            
            return null;
        }

        #endregion

        #region Skill Node

        public static void SaveSkillNode(this SkillNode sn)
        {
            var cfg = new editor.cfg.SkillCfg();
            cfg.InitData(sn);
            cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/SkillCfg/skillId_{cfg.skillId}.json");
        }

        #endregion

        #region TargetCfg_JsonCfg

        public static TargetCfg ToView(this editor.cfg.TargetCfg editorCfg)
        {
            if (editorCfg == null)
            {
                return new TargetCfg(){ViewState = ValState.Null};
            }
            
            var cfg = new TargetCfg();
            cfg.selectRange = editorCfg.selectRange;
            cfg.searchDis = editorCfg.searchDis;
            cfg.targetTypeArr = editorCfg.targetTypeArr.ToEnums<UnitTypeEnum>();
            cfg.targetTeam = editorCfg.targetTeam.ToEnum<TargetTeamEnum>();
            cfg.selectRule = editorCfg.selectRule.ToEnum<SelectRuleEnum>();
            return cfg;
        }
        
        public static editor.cfg.TargetCfg ToJsonCfg(this TargetCfg cfg)
        {
            if (cfg == null || cfg.ViewState == ValState.Null)
            {
                return null;
            }
            
            var editorCfg = new editor.cfg.TargetCfg();
            editorCfg.selectRange = cfg.selectRange;
            editorCfg.searchDis = cfg.searchDis;
            editorCfg.targetTypeArr = cfg.targetTypeArr.ToStrings();
            editorCfg.targetTeam = cfg.targetTeam.ToString();
            editorCfg.selectRule = cfg.selectRule.ToString();
            return editorCfg;
        }

        #endregion
        
        #region BulletCfg_JsonCfg
        
        public static BulletCfg ToView(this editor.cfg.BulletCfg editorCfg)
        {
            if (editorCfg == null)
            {
                return new BulletCfg(){ViewState = ValState.Null};
            }
            
            var cfg = new BulletCfg();
            cfg.bulletType = editorCfg.bulletType.ToEnum<BulletTypeEnum>();
            cfg.bulletName = editorCfg.bulletName;
            cfg.resPath = editorCfg.resPath;
            cfg.bulletSpeed = editorCfg.bulletSpeed;
            cfg.bulletSize = editorCfg.bulletSize;
            cfg.bulletHeight = editorCfg.bulletHeight;
            cfg.bulletOffset = editorCfg.bulletOffset;
            cfg.bulletDelay = editorCfg.bulletDelay;
            cfg.canBlock = editorCfg.canBlock;
            cfg.bulletDuration = editorCfg.bulletDuration;
            cfg.impacter = editorCfg.impacter.ToView();
            
            return cfg;
        }
        
        public static editor.cfg.BulletCfg ToJsonCfg(this BulletCfg cfg)
        {
            if (cfg == null || cfg.ViewState == ValState.Null)
            {
                return null;
            }
            
            var editorCfg = new editor.cfg.BulletCfg();
            
            editorCfg.bulletType = cfg.bulletType.ToString();
            editorCfg.bulletName = cfg.bulletName;
            editorCfg.resPath = cfg.resPath;
            editorCfg.bulletSpeed = cfg.bulletSpeed;
            editorCfg.bulletSize = cfg.bulletSize;
            editorCfg.bulletHeight = cfg.bulletHeight;
            editorCfg.bulletOffset = cfg.bulletOffset;
            editorCfg.bulletDelay = cfg.bulletDelay;
            editorCfg.canBlock = cfg.canBlock;
            editorCfg.bulletDuration = cfg.bulletDuration;
            editorCfg.impacter = cfg.impacter.ToJsonCfg();

            return editorCfg;
        }
        
        #endregion

        #region Enum and String Convert

        internal static T ToEnum<T>(this string enumString) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), enumString);
        }
        
        internal static T[] ToEnums<T>(this string[] enumStrings) where T : Enum
        {
            if (enumStrings?.Length < 1)
            {
                return null;
            }
            
            var selectedEnumList = new T[enumStrings.Length];
            for (int i = 0; i < enumStrings.Length; i++)
            {
                selectedEnumList[i] = enumStrings[i].ToEnum<T>();
            }

            return selectedEnumList;
        }
        
        internal static string[] ToStrings<T>(this T[] enums) where T : Enum
        {
            if (enums?.Length < 1)
            {
                return null;
            }
            
            return enums.Select(o=>o.ToString()).ToArray();
        }

        #endregion
        
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

        #region SkillCfg_SkillNode Convert

        public static void InitData(this SkillNode skillNode, editor.cfg.SkillCfg skillCfg)
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
            skillNode.releaseMode = skillCfg.releaseMode.ToEnum<ReleaseModeEnum>();
            skillNode.targetCfg = skillCfg.targetCfg.ToView();
            skillNode.bulletCfg = null; //skillCfg.bulletCfg;
        }
        
        public static void InitData(this editor.cfg.SkillCfg cfg, SkillNode node)
        {
            cfg.skillId = node.skillId; 
            cfg.iconName = node.iconName; 
            cfg.aniName = node.aniName;
            cfg.audio_start = node.audio_start;
            cfg.audio_work = node.audio_work;
            cfg.audio_hit = node.audio_hit;
            cfg.cdTime = node.cdTime;
            cfg.spellTime = node.spellTime;
            cfg.damage = node.damage;
            cfg.buffIdArr = node.buffIdArr;
            cfg.isNormalAttack = node.isNormalAttack;
            cfg.releaseMode = node.releaseMode.ToString();
            cfg.targetCfg = node.targetCfg.ToJsonCfg();
            // cfg.bulletCfg = node.bulletCfg;
        }

        #endregion

        #region BuffCfg_BuffNode Convert

        public static void InitData(this BuffNode node, editor.cfg.BuffCfg cfg)
        {
            node.BuffId = cfg.buffId;
            node.buffName = cfg.buffName;
            node.buffDelay = cfg.buffDelay;
            node.buffInterval = cfg.buffInterval;
            node.buffDuration = cfg.buffDuration;
            node.buffAudio = cfg.buffAudio;
            node.buffEffect = cfg.buffEffect;
            node.hitTickAudio = cfg.hitTickAudio;
            
            node.buffType = cfg.buffType.ToEnum<BuffTypeEnum>();
            node.attacher = cfg.attacher.ToEnum<AttachTypeEnum>();
            node.staticPosType = cfg.staticPosType.ToEnum<StaticPosTypeEnum>();
            node.impacter = cfg.impacter.ToView();
        }
        
        public static void InitData(this editor.cfg.BuffCfg cfg, BuffNode node)
        {
            cfg.buffId = node.BuffId;
            cfg.buffName = node.buffName;
            cfg.buffType = node.buffType.ToString();
            cfg.attacher = node.attacher.ToString();
            cfg.staticPosType = node.staticPosType.ToString();
            cfg.impacter = node.impacter.ToJsonCfg();
            cfg.buffDelay = node.buffDelay;
            cfg.buffInterval = node.buffInterval;
            cfg.buffDuration = node.buffDuration;
            cfg.buffAudio = node.buffAudio;
            cfg.buffEffect = node.buffEffect;
            cfg.hitTickAudio = node.hitTickAudio;
        }

        #endregion
    }
}