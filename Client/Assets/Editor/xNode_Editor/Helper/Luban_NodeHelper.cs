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
        #region Unit Node

        public static UnitNode GetUnitNodeById(int unitId)
        {
            var findingFile = "unitId_" + unitId + ".json";
            var findingPath = $"{Application.dataPath}/../LubanGens/EditorJsonData/UnitCfg/{findingFile}";
            if (File.Exists(findingPath))
            {
                var cfg = new editor.cfg.UnitInfoCfg();
                cfg.LoadJsonFile(findingPath);
                var node = new UnitNode();
                node.InitData(cfg);
                return node;
            }
            else
            {
                Debug.LogWarning("cant find file in path:"+findingPath);
                return null;
            }
        }
        
        public static void SaveUnitNode(this UnitNode node)
        {
            var cfg = new editor.cfg.UnitInfoCfg();
            cfg.InitData(node);
            cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/UnitCfg/unitId_{cfg.unitId}.json");
            Debug.Log($"unitId_{cfg.unitId} 保存成功!");
        }

        #endregion
       
        
        #region Skill Node

        public static SkillNode GetSkillNodeById(int skillId)
        {
            var findingFile = "skillId_" + skillId + ".json";
            var findingPath = $"{Application.dataPath}/../LubanGens/EditorJsonData/SkillCfg/{findingFile}";
            if (File.Exists(findingPath))
            {
                var cfg = new editor.cfg.SkillCfg();
                cfg.LoadJsonFile(findingPath);
                var node = new SkillNode();
                node.InitData(cfg);
                return node;
            }
            else
            {
                Debug.LogWarning("cant find file in path:"+findingPath);
                return null;
            }
        }
        
        public static void SaveSkillNode(this SkillNode sn)
        {
            var cfg = new editor.cfg.SkillCfg();
            cfg.InitData(sn);
            cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/SkillCfg/skillId_{cfg.skillId}.json");
            Debug.Log($"skillId_{cfg.skillId} 保存成功!");
        }

        #endregion
        
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

        /// <summary>
        /// 添加新buff后需要添加case，TODO 改成反射
        /// </summary>
        public static void SaveBuffNode(this BuffNode bn)
        {
            if (bn.GetType().Name == "BuffNode")
            {
                var cfg = new editor.cfg.BuffCfg();
                cfg.InitData(bn);
                cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/BuffCfg/{cfg.GetType().Name}/buffId_{cfg.buffId}.json");
                Debug.Log($"{cfg.GetType().Name}/buffId_{cfg.buffId} 保存成功!");
                return;
            }
            
            if (bn is ArthurMarkBuffNode n1)
            {
                var cfg = new editor.cfg.ArthurMarkBuffCfg();
                cfg.InitData(bn);
                // 特殊属性
                cfg.damagePct = n1.damagePct;
                
                cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/BuffCfg/{cfg.GetType().Name}/buffId_{cfg.buffId}.json");
                Debug.Log($"{cfg.GetType().Name}/buffId_{cfg.buffId} 保存成功!");
                return;
            }
            
            if (bn is CommonModifySkillBuffNode n2)
            {
                var cfg = new editor.cfg.CommonModifySkillBuffCfg();
                cfg.InitData(bn);
                // 特殊属性
                cfg.originalID = n2.originalID;
                cfg.replaceID = n2.replaceID;
                
                cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/BuffCfg/{cfg.GetType().Name}/buffId_{cfg.buffId}.json");
                Debug.Log($"{cfg.GetType().Name}/buffId_{cfg.buffId} 保存成功!");
                return;
            }
            
            if (bn is DamageBuffCfg_DynamicGroupNode n3)
            {
                var cfg = new editor.cfg.DamageBuffCfg_DynamicGroup();
                cfg.InitData(bn);
                // 特殊属性
                cfg.damage = n3.damage;

                cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/BuffCfg/{cfg.GetType().Name}/buffId_{cfg.buffId}.json");
                Debug.Log($"{cfg.GetType().Name}/buffId_{cfg.buffId} 保存成功!");
                return;
            }
            
            if (bn is DamageBuffCfg_StaticGroupNode n4)
            {
                var cfg = new editor.cfg.DamageBuffCfg_StaticGroup();
                cfg.InitData(bn);
                // 特殊属性
                cfg.damage = n4.damage;

                cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/BuffCfg/{cfg.GetType().Name}/buffId_{cfg.buffId}.json");
                Debug.Log($"{cfg.GetType().Name}/buffId_{cfg.buffId} 保存成功!");
                return;
            }
            
            if (bn is ExecuteDamageBuffNode n5)
            {
                var cfg = new editor.cfg.ExecuteDamageBuffCfg();
                cfg.InitData(bn);
                // 特殊属性
                cfg.damagePct = n5.damagePct;

                cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/BuffCfg/{cfg.GetType().Name}/buffId_{cfg.buffId}.json");
                Debug.Log($"{cfg.GetType().Name}/buffId_{cfg.buffId} 保存成功!");
                return;
            }
            
            if (bn is HouyiMixedMultiScatterBuffNode n6)
            {
                var cfg = new editor.cfg.HouyiMixedMultiScatterBuffCfg();
                cfg.InitData(bn);
                // 特殊属性
                cfg.scatterCount = n6.scatterCount;
                cfg.damagePct = n6.damagePct;
                cfg.arrowCount = n6.arrowCount;
                cfg.arrowDelay = n6.arrowDelay;
                cfg.posOffset = n6.posOffset;
                cfg.targetCfg = n6.targetCfg.ToJsonCfg();

                cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/BuffCfg/{cfg.GetType().Name}/buffId_{cfg.buffId}.json");
                Debug.Log($"{cfg.GetType().Name}/buffId_{cfg.buffId} 保存成功!");
                return;
            }
            
            if (bn is HouyiMultipleArrowBuffNode n7)
            {
                var cfg = new editor.cfg.HouyiMultipleArrowBuffCfg();
                cfg.InitData(bn);
                // 特殊属性
                cfg.arrowCount = n7.arrowCount;
                cfg.arrowDelay = n7.arrowDelay;
                cfg.posOffset = n7.posOffset;
                
                cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/BuffCfg/{cfg.GetType().Name}/buffId_{cfg.buffId}.json");
                Debug.Log($"{cfg.GetType().Name}/buffId_{cfg.buffId} 保存成功!");
                return;
            }
            
            if (bn is HouyiMultipleSkillModifyBuffNode n8)
            {
                var cfg = new editor.cfg.HouyiMultipleSkillModifyBuffCfg();
                cfg.InitData(bn);
                // 特殊属性
                cfg.originalID = n8.originalID;
                cfg.powerID = n8.powerID;
                cfg.superPowerID = n8.superPowerID;
                cfg.triggerOverCount = n8.triggerOverCount;
                cfg.resetTime = n8.resetTime;
                
                cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/BuffCfg/{cfg.GetType().Name}/buffId_{cfg.buffId}.json");
                Debug.Log($"{cfg.GetType().Name}/buffId_{cfg.buffId} 保存成功!");
                return;
            }
            
            if (bn is HouyiPasvAttackSpeedBuffNode n9)
            {
                var cfg = new editor.cfg.HouyiPasvAttackSpeedBuffCfg();
                cfg.InitData(bn);
                // 特殊属性
                cfg.overCount = n9.overCount;
                cfg.speedAddtion = n9.speedAddtion;
                cfg.resetTime = n9.resetTime;

                cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/BuffCfg/{cfg.GetType().Name}/buffId_{cfg.buffId}.json");
                Debug.Log($"{cfg.GetType().Name}/buffId_{cfg.buffId} 保存成功!");
                return;
            }
            
            if (bn is HouyiScatterArrowBuffNode n10)
            {
                var cfg = new editor.cfg.HouyiScatterArrowBuffCfg();
                cfg.InitData(bn);
                // 特殊属性
                cfg.scatterCount = n10.scatterCount;
                cfg.damagePct = n10.damagePct;
                cfg.targetCfg = n10.targetCfg.ToJsonCfg();

                cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/BuffCfg/{cfg.GetType().Name}/buffId_{cfg.buffId}.json");
                Debug.Log($"{cfg.GetType().Name}/buffId_{cfg.buffId} 保存成功!");
                return;
            }
            
            if (bn is HouyiScatterSkillModifyBuffNode n11)
            {
                var cfg = new editor.cfg.HouyiScatterSkillModifyBuffCfg();
                cfg.InitData(bn);
                // 特殊属性
                cfg.originalID = n11.originalID;
                cfg.powerID = n11.powerID;
                cfg.superPowerID = n11.superPowerID;

                cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/BuffCfg/{cfg.GetType().Name}/buffId_{cfg.buffId}.json");
                Debug.Log($"{cfg.GetType().Name}/buffId_{cfg.buffId} 保存成功!");
                return;
            }
            
            if (bn is HPCureBuffNode n12)
            {
                var cfg = new editor.cfg.HPCureBuffCfg();
                cfg.InitData(bn);
                // 特殊属性
                cfg.cureHPpct = n12.cureHPpct;

                cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/BuffCfg/{cfg.GetType().Name}/buffId_{cfg.buffId}.json");
                Debug.Log($"{cfg.GetType().Name}/buffId_{cfg.buffId} 保存成功!");
                return;
            }

            if (bn is MoveSpeedBuffNode n13)
            {
                var cfg = new editor.cfg.MoveSpeedBuffCfg();
                cfg.InitData(bn);
                // 特殊属性
                cfg.amount = n13.amount;
                
                cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/BuffCfg/{cfg.GetType().Name}/buffId_{cfg.buffId}.json");
                Debug.Log($"{cfg.GetType().Name}/buffId_{cfg.buffId} 保存成功!");
                return;
            }
            
            if (bn is StunBuffCfg_DynamicTimeNode n14)
            {
                var cfg = new editor.cfg.StunBuffCfg_DynamicTime();
                cfg.InitData(bn);
                // 特殊属性
                cfg.minStunTime = n14.minStunTime;
                cfg.maxStunTime = n14.maxStunTime;

                cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/BuffCfg/{cfg.GetType().Name}/buffId_{cfg.buffId}.json");
                Debug.Log($"{cfg.GetType().Name}/buffId_{cfg.buffId} 保存成功!");
                return;
            }
            
            if (bn is TargetFlashMoveBuffNode n15)
            {
                var cfg = new editor.cfg.TargetFlashMoveBuffCfg();
                cfg.InitData(bn);
                // 特殊属性
                cfg.offset = n15.offset;

                cfg.SaveJsonFile($"{Application.dataPath}/../LubanGens/EditorJsonData/BuffCfg/{cfg.GetType().Name}/buffId_{cfg.buffId}.json");
                Debug.Log($"{cfg.GetType().Name}/buffId_{cfg.buffId} 保存成功!");
                return;
            }
            
        }

        /// <summary>
        /// 添加新buff后需要添加case，TODO 改成反射
        /// </summary>
        public static BuffNode GetBuffNode(string buffType, string jsonFilePath)
        {
            if (buffType == "BuffCfg")
            {
                var cfg = new editor.cfg.BuffCfg();
                cfg.LoadJsonFile(jsonFilePath);
                var node = new BuffNode();
                node.InitData(cfg);

                return node;
            }
            
            if (buffType == "ArthurMarkBuffCfg")
            {
                var cfg = new editor.cfg.ArthurMarkBuffCfg();
                cfg.LoadJsonFile(jsonFilePath);
                var node = new ArthurMarkBuffNode();
                node.InitData(cfg);
                // 特殊属性
                node.damagePct = cfg.damagePct;
                
                return node;
            }
            
            if (buffType == "CommonModifySkillBuffCfg")
            {
                var cfg = new editor.cfg.CommonModifySkillBuffCfg();
                cfg.LoadJsonFile(jsonFilePath);
                var node = new CommonModifySkillBuffNode();
                node.InitData(cfg);
                // 特殊属性
                node.originalID = cfg.originalID;
                node.replaceID = cfg.replaceID;
                
                return node;
            }
            
            if (buffType == "DamageBuffCfg_DynamicGroup")
            {
                var cfg = new editor.cfg.DamageBuffCfg_DynamicGroup();
                cfg.LoadJsonFile(jsonFilePath);
                var node = new DamageBuffCfg_DynamicGroupNode();
                node.InitData(cfg);
                // 特殊属性
                node.damage = cfg.damage;
                
                return node;
            }
            
            if (buffType == "DamageBuffCfg_StaticGroup")
            {
                var cfg = new editor.cfg.DamageBuffCfg_StaticGroup();
                cfg.LoadJsonFile(jsonFilePath);
                var node = new DamageBuffCfg_StaticGroupNode();
                node.InitData(cfg);
                // 特殊属性
                node.damage = cfg.damage;
                
                return node;
            }
            
            if (buffType == "ExecuteDamageBuffCfg")
            {
                var cfg = new editor.cfg.ExecuteDamageBuffCfg();
                cfg.LoadJsonFile(jsonFilePath);
                var node = new ExecuteDamageBuffNode();
                node.InitData(cfg);
                // 特殊属性
                node.damagePct = cfg.damagePct;
                
                return node;
            }
            
            if (buffType == "HouyiMixedMultiScatterBuffCfg")
            {
                var cfg = new editor.cfg.HouyiMixedMultiScatterBuffCfg();
                cfg.LoadJsonFile(jsonFilePath);
                var node = new HouyiMixedMultiScatterBuffNode();
                node.InitData(cfg);
                // 特殊属性
                node.scatterCount = cfg.scatterCount;
                node.damagePct = cfg.damagePct;
                node.arrowCount = cfg.arrowCount;
                node.arrowDelay = cfg.arrowDelay;
                node.posOffset = cfg.posOffset;
                node.targetCfg = cfg.targetCfg.ToView();
                
                return node;
            }
            
            if (buffType == "HouyiMultipleArrowBuffCfg")
            {
                var cfg = new editor.cfg.HouyiMultipleArrowBuffCfg();
                cfg.LoadJsonFile(jsonFilePath);
                var node = new HouyiMultipleArrowBuffNode();
                node.InitData(cfg);
                // 特殊属性
                node.arrowCount = cfg.arrowCount;
                node.arrowDelay = cfg.arrowDelay;
                node.posOffset = cfg.posOffset;
                
                return node;
            }
            
            if (buffType == "HouyiMultipleSkillModifyBuffCfg")
            {
                var cfg = new editor.cfg.HouyiMultipleSkillModifyBuffCfg();
                cfg.LoadJsonFile(jsonFilePath);
                var node = new HouyiMultipleSkillModifyBuffNode();
                node.InitData(cfg);
                // 特殊属性
                node.originalID = cfg.originalID;
                node.powerID = cfg.powerID;
                node.superPowerID = cfg.superPowerID;
                node.triggerOverCount = cfg.triggerOverCount;
                node.resetTime = cfg.resetTime;

                return node;
            }
            
            if (buffType == "HouyiPasvAttackSpeedBuffCfg")
            {
                var cfg = new editor.cfg.HouyiPasvAttackSpeedBuffCfg();
                cfg.LoadJsonFile(jsonFilePath);
                var node = new HouyiPasvAttackSpeedBuffNode();
                node.InitData(cfg);
                // 特殊属性
                node.overCount = cfg.overCount;
                node.speedAddtion = cfg.speedAddtion;
                node.resetTime = cfg.resetTime;

                return node;
            }
            
            if (buffType == "HouyiScatterArrowBuffCfg")
            {
                var cfg = new editor.cfg.HouyiScatterArrowBuffCfg();
                cfg.LoadJsonFile(jsonFilePath);
                var node = new HouyiScatterArrowBuffNode();
                node.InitData(cfg);
                // 特殊属性
                node.scatterCount = cfg.scatterCount;
                node.targetCfg = cfg.targetCfg.ToView();
                node.damagePct = cfg.damagePct;

                return node;
            }
            
            if (buffType == "HouyiScatterSkillModifyBuffCfg")
            {
                var cfg = new editor.cfg.HouyiScatterSkillModifyBuffCfg();
                cfg.LoadJsonFile(jsonFilePath);
                var node = new HouyiScatterSkillModifyBuffNode();
                node.InitData(cfg);
                // 特殊属性
                node.originalID = cfg.originalID;
                node.powerID = cfg.powerID;
                node.superPowerID = cfg.superPowerID;
                
                return node;
            }
            
            if (buffType == "HPCureBuffCfg")
            {
                var cfg = new editor.cfg.HPCureBuffCfg();
                cfg.LoadJsonFile(jsonFilePath);
                var node = new HPCureBuffNode();
                node.InitData(cfg);
                // 特殊属性
                node.cureHPpct = cfg.cureHPpct;
                
                return node;
            }
            
            if (buffType == "MoveSpeedBuffCfg")
            {
                var cfg = new editor.cfg.MoveSpeedBuffCfg();
                cfg.LoadJsonFile(jsonFilePath);
                var node = new MoveSpeedBuffNode();
                node.InitData(cfg);
                // 特殊属性
                node.amount = cfg.amount;
                
                return node;
            }
            
            if (buffType == "StunBuffCfg_DynamicTime")
            {
                var cfg = new editor.cfg.StunBuffCfg_DynamicTime();
                cfg.LoadJsonFile(jsonFilePath);
                var node = new StunBuffCfg_DynamicTimeNode();
                node.InitData(cfg);
                // 特殊属性
                node.minStunTime = cfg.minStunTime;
                node.maxStunTime = cfg.maxStunTime;
                
                return node;
            }
            
            if (buffType == "TargetFlashMoveBuffCfg")
            {
                var cfg = new editor.cfg.TargetFlashMoveBuffCfg();
                cfg.LoadJsonFile(jsonFilePath);
                var node = new TargetFlashMoveBuffNode();
                node.InitData(cfg);
                // 特殊属性
                node.offset = cfg.offset;

                return node;
            }
            
            
            
            return null;
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
            cfg.ViewState = ValState.NotNull;
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
            cfg.ViewState = ValState.NotNull;
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
                return new T[0];;
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
                return new string[0];
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
        public const int SkillWidth = 300;
        public const int SkillHeight = 500;
        public const int UnitWidth = 300;
        public const int UnitHeight = 500;
        public const int BuffPaddingWidth = BuffWidth + PaddingWidth;
        public const int BuffPaddingHeight = BuffHeight + PaddingHeight;
        public const int SkillPaddingWidth = SkillWidth + PaddingWidth;
        public const int SkillPaddingHeight = SkillHeight + PaddingHeight;
        #endregion

        #region UnitInfoCfg_UnitNode Convert

        public static void InitData(this UnitNode unitNode, editor.cfg.UnitInfoCfg cfg)
        {
            unitNode.unitId = cfg.unitId;
            unitNode.unitName = cfg.unitName;
            unitNode.resName = cfg.resName;
            unitNode.hp = cfg.hp;
            unitNode.def = cfg.def;
            unitNode.moveSpeed = cfg.moveSpeed;
            unitNode.colliderType = cfg.colliderType.ToEnum<UnitTypeEnum>();
            unitNode.pasvBuff = cfg.pasvBuff;
            unitNode.skillArr = cfg.skillArr;

        }
        
        public static void InitData(this editor.cfg.UnitInfoCfg cfg, UnitNode unitNode)
        {
            cfg.unitId = unitNode.unitId; 
            cfg.unitName = unitNode.unitName;
            cfg.resName = unitNode.resName;
            cfg.hp = unitNode.hp;
            cfg.def = unitNode.def;
            cfg.moveSpeed = unitNode.moveSpeed;
            cfg.colliderType = unitNode.colliderType.ToString();
            cfg.pasvBuff = unitNode.pasvBuff;
            cfg.skillArr = unitNode.skillArr;
        }

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
            skillNode.bulletCfg = skillCfg.bulletCfg.ToView();
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
            cfg.bulletCfg = node.bulletCfg.ToJsonCfg();
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