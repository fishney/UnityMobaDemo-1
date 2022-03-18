/****************************************************
    文件：ResSvc.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Bright.Serialization;
using cfg.Datas;
using cfg;
using CodingK_Session;
using PEMath;
using PEPhysx;
using proto.HOKProtocol;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResSvc : GameRootMonoSingleton<ResSvc>
{
    public void InitSvc()
    {
        // tables
        configTable = new cfg.Tables(LoadByteBuf);
        
        // bag
        LoadItemInfo();

        // unit
        LoadUnitInfo();
        
        // buff
        LoadBuffs();
        
        // skill
        LoadSkills();
        
        Debug.Log("ResSvc Init Completed.");
    }

    private Tables configTable;

    #region Bag

    private Dictionary<int, ItemCfg> itemCfgDic;
    
    public void LoadItemInfo()
    {
        itemCfgDic = new Dictionary<int, ItemCfg>();
        foreach (var cfg in configTable.TbItemCfg.DataList)
        {
            itemCfgDic.Add(cfg.id, cfg);
        }
    }
    
    private static ByteBuf LoadByteBuf(string file)
    {
        var text = Resources.Load($"ResCfg/{file}") as TextAsset;
        return new ByteBuf(text.bytes);
        //return new ByteBuf(File.ReadAllBytes($"{Application.dataPath}/Resources/ResCfg/{file}.bytes"));
    }
    
    public ItemCfg GetItemCfgById(int id)
    {
        if (itemCfgDic.TryGetValue(id,out var itemCfg))
        {
            return itemCfg;
        }

        return null;
    }

    #endregion

    #region Audio
    
    /// <summary>
    /// Audio暂存池
    /// </summary>
    private Dictionary<string, AudioClip> audioDic = new Dictionary<string, AudioClip>();
    
    /// <summary>
    /// 加载Audio
    /// </summary>
    /// <param name="path"></param>
    /// <param name="isCache">是否需要放进缓存字典中</param>
    /// <returns></returns>
    public AudioClip LoadAudio(string path, bool isCache = true)
    {
        AudioClip au = null;
        if (!audioDic.TryGetValue(path,out au))
        {
            au = Resources.Load<AudioClip>(path);
            if (isCache)
            {
                audioDic[path] = au;
            }
        }
        
        return au;
    }

    #endregion
    
    #region GameObject
    
    /// <summary>
    /// GameObject暂存池
    /// </summary>
    private Dictionary<string, GameObject> goDic = new Dictionary<string, GameObject>();
    
    /// <summary>
    /// 加载Prefab
    /// </summary>
    /// <param name="path"></param>
    /// <param name="isCache">是否需要放进缓存字典中</param>
    /// <returns></returns>
    public GameObject LoadPrefab(string path, bool isCache = true)
    {
        GameObject prefab = null;
        if (!goDic.TryGetValue(path,out prefab))
        {
            prefab = Resources.Load<GameObject>(path);
            if (isCache)
            {
                goDic[path] = prefab;
            }
        }

        GameObject go = null;
        if (prefab != null)
        {
            go = Instantiate(prefab);
        }
        else
        {
            Debug.Log($"Load Prefab Error:{path}");
        }
        
        return go;
    }

    #endregion

    #region Sprite

    /// <summary>
    /// Sprite暂存池
    /// </summary>
    private Dictionary<string, Sprite> spriteDic = new Dictionary<string, Sprite>();
    
    public Sprite LoadSprite(string path,bool cache = false)
    {
        Sprite sp = null;
        if (!spriteDic.TryGetValue(path,out sp))
        {
            sp = Resources.Load<Sprite>(path);
            if (cache)
            {
                spriteDic.Add(path,sp);
            }
        }

        return sp;
    }

    #endregion

    #region 英雄信息
    
    private Dictionary<int, UnitCfg> unitDic;

    public void LoadUnitInfo()
    {
        unitDic = new Dictionary<int, UnitCfg>();
        
        foreach (var cfg in configTable.TbUnitInfoCfg.DataList)
        {
            var unitCfg = new UnitCfg();
            unitCfg.info = cfg;

            ColliderConfig colliCfg = new ColliderConfig();
            PEInt hitHeight = new PEInt();
            switch (cfg.colliderType)
            {
                case UnitTypeEnum.Hero:
                    colliCfg = new ColliderConfig
                    {
                        mType = ColliderType.Cylinder, //所有角色都是圆柱体
                        mRadius = (PEInt) 0.5f,
                    };
                    hitHeight = (PEInt) 1.5F;
                    break;
                case UnitTypeEnum.Soldier:
                    colliCfg = new ColliderConfig
                    {
                        mType = ColliderType.Cylinder,
                        mRadius = (PEInt) 0.25f,
                    };
                    hitHeight = (PEInt) 0.6F;
                    break;
                case UnitTypeEnum.Tower:
                    colliCfg = new ColliderConfig
                    {
                        mType = ColliderType.Cylinder,
                        mRadius = (PEInt) 0.25f,
                    };
                    hitHeight = (PEInt) 1.5F;
                    break;
                case UnitTypeEnum.Crystal:
                    colliCfg = new ColliderConfig
                    {
                        mType = ColliderType.Cylinder,
                        mRadius = (PEInt) 1f,
                    };
                    hitHeight = (PEInt) 1F;
                    break;
            }

            unitCfg.colliCfg = colliCfg;
            unitCfg.hitHeight = hitHeight;
            
            unitDic.Add(unitCfg.info.unitId, unitCfg);
        }
    }

    public UnitCfg GetUnitConfigById(int id)
    {
        if (unitDic.TryGetValue(id,out var cfg))
        {
            return cfg;
        }
        
        // TODO TEST hero
        // if (id == 103)
        // {
        //     return new UnitCfg()
        //     {
        //         info = new UnitInfoCfg
        //         {
        //             unitId = 103,
        //             unitName = "金克斯",
        //             resName = "Jinx",
        //             hp = 3500,
        //             def = 10,
        //             moveSpeed = 5,
        //             pasvBuff = new int[] { 10300,10301 },//new int[] { 10200, 10201 },
        //             skillArr = new []{1030,1031,1032,1033},
        //         },
        //        
        //         hitHeight = (PEInt)1.5F,
        //
        //         colliCfg = new ColliderConfig
        //         {
        //             mType = ColliderType.Cylinder, //所有角色都是圆柱体
        //             mRadius = (PEInt)0.5f,
        //         },
        //         
        //     };
        // }
        
        return null;
        
        // TODO 1
        // switch (unitId)
        // {
        //     case 101:
        //         return new UnitCfg()
        //         {
        //             unitId = 101,
        //             unitName = "亚瑟",
        //             resName = "arthur",
        //             hitHeight = (PEInt)1.5F,
        //             
        //             hp = 6500,
        //             def = 0,
        //             moveSpeed = 5,
        //             
        //             colliCfg = new ColliderConfig
        //             {
        //                 mType = ColliderType.Cylinder, //所有角色都是圆柱体
        //                 mRadius = (PEInt)0.5f,
        //             },
        //             pasvBuff = new []{10100},
        //             skillArr = new []{1010,1011,1012,1013},
        //         };
        //     case 102:
        //         return new UnitCfg()
        //         {
        //             unitId = 102,
        //             unitName = "后羿",
        //             resName = "houyi",
        //             hitHeight = (PEInt)1.5F,
        //             
        //             hp = 3500,
        //             def = 10,
        //             moveSpeed = 5,
        //             
        //             colliCfg = new ColliderConfig
        //             {
        //                 mType = ColliderType.Cylinder, //所有角色都是圆柱体
        //                 mRadius = (PEInt)0.5f,
        //             },
        //             pasvBuff = new int[] { 10200, 10201 },
        //             skillArr = new []{1020,1021,1022,1023},
        //         };
        //     
        //     case 1001:
        //         return new UnitCfg {
        //             unitId = 1001,
        //             unitName = "蓝方一塔",
        //             resName = "blueTower",
        //             hitHeight = (PEInt)1.5f,
        //             hp = 400,
        //             def = 0,
        //             colliCfg = new ColliderConfig {
        //                 mType = ColliderType.Cylinder,
        //                 mRadius = (PEInt)0.25f,
        //             },
        //             pasvBuff = null,
        //             skillArr = new int[] { 10010 }
        //         };
        //     case 1002:
        //         return new UnitCfg {
        //             unitId = 1002,
        //             unitName = "蓝方水晶",
        //             resName = "blueCrystal",
        //             hitHeight = (PEInt)1f,
        //             hp = 800,
        //             def = 0,
        //             colliCfg = new ColliderConfig {
        //                 mType = ColliderType.Cylinder,
        //                 mRadius = (PEInt)1f,
        //             },
        //             pasvBuff = null,
        //             skillArr = new int[] { 10020 }
        //         };
        //     case 1003:
        //         return new UnitCfg {
        //             unitId = 1003,
        //             unitName = "蓝方近战小兵",
        //             resName = "xb_blue_jz",
        //             hitHeight = (PEInt)0.6f,
        //             hp = 500,
        //             def = 0,
        //             moveSpeed = 2,
        //             colliCfg = new ColliderConfig {
        //                 mType = ColliderType.Cylinder,
        //                 mRadius = (PEInt)0.25f,
        //             },
        //             pasvBuff = null,
        //             skillArr = new int[] { 10030 }
        //         };
        //     case 1004:
        //         return new UnitCfg {
        //             unitId = 1004,
        //             unitName = "蓝方远程小兵",
        //             resName = "xb_blue_yc",
        //             hitHeight = (PEInt)0.6f,
        //             hp = 300,
        //             def = 0,
        //             moveSpeed = 2,
        //             colliCfg = new ColliderConfig {
        //                 mType = ColliderType.Cylinder,
        //                 mRadius = (PEInt)0.25f,
        //             },
        //             pasvBuff = null,
        //             skillArr = new int[] { 10040 }
        //         };
        //     case 2001:
        //         return new UnitCfg {
        //             unitId = 2001,
        //             unitName = "红方一塔",
        //             resName = "redTower",
        //             hitHeight = (PEInt)1.5f,
        //             hp = 400,
        //             def = 0,
        //             colliCfg = new ColliderConfig {
        //                 mType = ColliderType.Cylinder,
        //                 mRadius = (PEInt)0.25f,
        //             },
        //             pasvBuff = null,
        //             skillArr = new int[] { 20010 }
        //         };
        //     case 2002:
        //         return new UnitCfg {
        //             unitId = 2002,
        //             unitName = "红方水晶",
        //             resName = "redCrystal",
        //             hitHeight = (PEInt)1f,
        //             hp = 800,
        //             def = 0,
        //             colliCfg = new ColliderConfig {
        //                 mType = ColliderType.Cylinder,
        //                 mRadius = (PEInt)1f,
        //             },
        //             pasvBuff = null,
        //             skillArr = new int[] { 20020 }
        //         };
        //     case 2003:
        //         return new UnitCfg {
        //             unitId = 2003,
        //             unitName = "红方近战小兵",
        //             resName = "xb_red_jz",
        //             hitHeight = (PEInt)0.6f,
        //             hp = 500,
        //             def = 0,
        //             moveSpeed = 2,
        //             colliCfg = new ColliderConfig {
        //                 mType = ColliderType.Cylinder,
        //                 mRadius = (PEInt)0.25f,
        //             },
        //             pasvBuff = null,
        //             skillArr = new int[] { 20030 }
        //         };
        //     case 2004:
        //         return new UnitCfg {
        //             unitId = 2004,
        //             unitName = "红方远程小兵",
        //             resName = "xb_red_yc",
        //             hitHeight = (PEInt)0.6f,
        //             hp = 300,
        //             def = 0,
        //             moveSpeed = 2,
        //             colliCfg = new ColliderConfig {
        //                 mType = ColliderType.Cylinder,
        //                 mRadius = (PEInt)0.25f,
        //             },
        //             pasvBuff = null,
        //             skillArr = new int[] { 20040 }
        //         };
        // }

        return null;
    }

    #endregion

    #region 技能信息
    
    private Dictionary<int, SkillCfg> skillCfgDic;
    
    public void LoadSkills()
    {
        skillCfgDic = new Dictionary<int, SkillCfg>();
        foreach (var cfg in configTable.TbSkillCfg.DataList)
        {
            skillCfgDic.Add(cfg.skillId, cfg);
        }
    }
    public SkillCfg GetSkillConfigById(int id)
    {
        if (skillCfgDic.TryGetValue(id,out var cfg))
        {
            return cfg;
        }
        
        // TODO Test skill
     //    return Test(id);
     //    SkillCfg Test(int test_id)
     //    {
     //        SkillCfg sk_1030 = new SkillCfg {
     //     skillId = 1030,
     //     iconName = null,
     //     aniName = "atk",
     //     releaseMode = ReleaseModeEnum.None,
     //     //最近的敌方目标
     //     targetCfg = new TargetCfg {
     //         targetTeam = TargetTeamEnum.Enemy,
     //         selectRule = SelectRuleEnum.TargetClosestSingle,
     //         targetTypeArr = new UnitTypeEnum[] {
     //             UnitTypeEnum.Hero,
     //             UnitTypeEnum.Tower,
     //             UnitTypeEnum.Soldier,
     //         },
     //         selectRange = 5f,
     //         searchDis = 15f,
     //     },
     //     bulletCfg = new BulletCfg {
     //         bulletType = BulletTypeEnum.SkillTarget,
     //         bulletName = "金克斯普攻子弹",
     //         resPath = "jinx_bullets/Minigun",
     //         bulletSpeed = 1f,
     //         bulletSize = 0.1f,
     //         bulletHeight = 1.5f,
     //         bulletOffset = 0.5f,
     //         bulletDelay = 0,
     //     },
     //     cdTime = 0,
     //     spellTime = 350,//施法时间（技能前摇）
     //     isNormalAttack = true,
     //     skillTime = 1000,
     //     damage = 120,
     //
     //     buffIdArr = null,
     //     audio_start = null,
     //     audio_work = "jinx_ska_mode1_rls",
     //     audio_hit = "jinx_mode1_hit",
     //     
     // };  
     //        // TODO 替换技能1 + 普攻
     //        SkillCfg sk_1031 = new SkillCfg {
     //     skillId = 1031,
     //     iconName = "jinx_sk1_mode1",
     //     releaseMode = ReleaseModeEnum.Click,
     //     aniName = "sk1_mode1",
     //     targetCfg = null,
     //     bulletCfg = null,
     //     cdTime = 1500,//ms
     //     spellTime = 300,
     //     skillTime = 800,
     //     isNormalAttack = false,
     //     damage = 0,
     //     //普攻强化buff（技能修改）
     //     buffIdArr = new int[] { 10310,10311 },
     //     
     //     audio_start = "jinx_sk1_mode1_rls"
     // };
     //        SkillCfg sk_1032 = new SkillCfg {
     //            skillId = 1032,
     //            iconName = "jinx_sk2",
     //            aniName = "sk2",
     //            releaseMode = ReleaseModeEnum.Direction,
     //            targetCfg = null,
     //            bulletCfg = new BulletCfg {
     //                bulletType = BulletTypeEnum.UIDirection,//技能锁定的目标
     //                bulletName = "振荡电磁波",
     //                resPath = "jinx_bullets/Beam",
     //                bulletSpeed = 3f,
     //                bulletSize = 0.1f,
     //                bulletHeight = 1.5f,
     //                bulletOffset = 1f,
     //                bulletDelay = 0,
     //
     //                canBlock = true,
     //                //受影响的目标
     //                impacter = new TargetCfg {
     //                    targetTeam = TargetTeamEnum.Enemy,
     //                    selectRule = SelectRuleEnum.Hero,
     //                    targetTypeArr = new UnitTypeEnum[] { UnitTypeEnum.Hero , UnitTypeEnum.Soldier},
     //                },
     //                bulletDuration = 150,//确保不击中目标的情况下能飞出地图
     //            },
     //            cdTime = 1000,
     //            spellTime = 670,//施法时间（技能前摇）
     //            isNormalAttack = false,
     //            skillTime = 1500,
     //            damage = 500,
     //            buffIdArr = new []{10320},//new int[] { 10230, 10231 },
     //
     //            audio_start = "jinx_sk2_rls",
     //            audio_work = null,
     //            audio_hit = "jinx_sk2_hit",//技能命中后，命中目标播放音效
     //        };
     //        
     //        SkillCfg sk_1033 = new SkillCfg {
     //     skillId = 1033,
     //     iconName = "jinx_sk3",
     //     aniName = "sk3",
     //     releaseMode = ReleaseModeEnum.Direction,
     //     targetCfg = null,
     //     bulletCfg = new BulletCfg {
     //         bulletType = BulletTypeEnum.UIDirection,//技能锁定的目标
     //         bulletName = "超究极死神飞弹",
     //         resPath = "jinx_bullets/Bullet",
     //         bulletSpeed = 1f,
     //         bulletSize = 0.5f,
     //         bulletHeight = 1.5f,
     //         bulletOffset = 1f,
     //         bulletDelay = 200,
     //
     //         canBlock = true,
     //         //受影响的目标
     //         impacter = new TargetCfg {
     //             targetTeam = TargetTeamEnum.Enemy,
     //             selectRule = SelectRuleEnum.Hero,
     //             targetTypeArr = new UnitTypeEnum[] { UnitTypeEnum.Hero },
     //         },
     //         bulletDuration = 5000,//确保不击中目标的情况下能飞出地图
     //     },
     //     cdTime = 3000,
     //     spellTime = 900,//施法时间（技能前摇）
     //     isNormalAttack = false,
     //     skillTime = 1800,
     //     damage = 0,
     //     buffIdArr = new int[] { 10330 },
     //
     //     audio_start = "jinx_sk3_rls",
     //     audio_work = null,
     //     audio_hit = "jinx_sk3_hit",//技能命中后，命中目标播放音效
     // };
     //        // 炮状态普攻
     //        SkillCfg sk_1034 = new SkillCfg {
     //            skillId = 1034,
     //            iconName = null,
     //            aniName = "sk1",
     //            releaseMode = ReleaseModeEnum.None,
     //            //最近的敌方目标
     //            targetCfg = new TargetCfg {
     //                targetTeam = TargetTeamEnum.Enemy,
     //                selectRule = SelectRuleEnum.TargetClosestSingle,
     //                targetTypeArr = new UnitTypeEnum[] {
     //                    UnitTypeEnum.Hero,
     //                    UnitTypeEnum.Tower,
     //                    UnitTypeEnum.Soldier,
     //                },
     //                selectRange = 7.5f,//5f to 7.5f
     //                searchDis = 15f,
     //            },
     //            bulletCfg = new BulletCfg {
     //                bulletType = BulletTypeEnum.SkillTarget,
     //                bulletName = "金克斯普攻爆炸子弹",
     //                resPath = "jinx_bullets/Rocket",
     //                bulletSpeed = 1f,
     //                bulletSize = 0.1f,
     //                bulletHeight = 1.5f,
     //                bulletOffset = 0.5f,
     //                bulletDelay = 0,
     //            },
     //            cdTime = 0,
     //            spellTime = 720,//施法时间（技能前摇）
     //            isNormalAttack = true,
     //            skillTime = 1333,
     //            damage = 0,//伤害由buff判定
     //
     //            buffIdArr = new int[]{10314},
     //            audio_start = null,
     //            audio_work = "jinx_ska_mode2_rls",
     //            audio_hit = "jinx_mode2_hit"
     //        };
     //        
     //        // 炮状态1技能
     //        SkillCfg sk_1035 = new SkillCfg {
     //            skillId = 1035,
     //            iconName = "jinx_sk1_mode2",
     //            releaseMode = ReleaseModeEnum.Click,
     //            aniName = "sk1_mode2",
     //            targetCfg = null,
     //            bulletCfg = null,
     //            cdTime = 1500,//ms
     //            spellTime = 300,
     //            skillTime = 1000,
     //            isNormalAttack = false,
     //            damage = 0,
     //            //普攻,1技能修改
     //            buffIdArr = new int[] { 10312, 10313},
     //     
     //            audio_start = "jinx_sk1_mode2_rls"
     //        };
     //        
     //        switch (test_id)
     //        {
     //            case 1030: return sk_1030;
     //            case 1031: return sk_1031;
     //            case 1032: return sk_1032;
     //            case 1033: return sk_1033;
     //            case 1034: return sk_1034;
     //            case 1035: return sk_1035;
     //        }
     //
     //        return null;
     //    }
        
        return null; 
        // TODO 1
        // switch (skillId)
        // {
        //     case 1010:
        //         return ResSkillConfigs.sk_1010;
        //     case 1011:
        //         return ResSkillConfigs.sk_1011;
        //     case 1012:
        //         return ResSkillConfigs.sk_1012;
        //     case 1013:
        //         return ResSkillConfigs.sk_1013;
        //     case 1014:
        //         return ResSkillConfigs.sk_1014;
        //     case 1020:
        //         return ResSkillConfigs.sk_1020;
        //     case 1021:
        //         return ResSkillConfigs.sk_1021;
        //     case 1022:
        //         return ResSkillConfigs.sk_1022;
        //     case 1023:
        //         return ResSkillConfigs.sk_1023;
        //     case 1024:
        //         return ResSkillConfigs.sk_1024;
        //     case 1025:
        //         return ResSkillConfigs.sk_1025;
        //     case 1026:
        //         return ResSkillConfigs.sk_1026;
        //     
        //     //防御塔与水晶的技能
        //     case 10010:
        //         return ResSkillConfigs.sk_10010;
        //     case 10020:
        //         return ResSkillConfigs.sk_10020;
        //     case 10030:
        //         return ResSkillConfigs.sk_10030;
        //     case 10040:
        //         return ResSkillConfigs.sk_10040;
        //     case 20010:
        //         return ResSkillConfigs.sk_20010;
        //     case 20020:
        //         return ResSkillConfigs.sk_20020;
        //     case 20030:
        //         return ResSkillConfigs.sk_20030;
        //     case 20040:
        //         return ResSkillConfigs.sk_20040;
        //     default:
        //         this.Error("Get SkillCfg Failed,Id: " + skillId);
        //         return null;
        // }
    }

    #endregion
    
    #region Buff信息

    
    public Buff CreateBuff(MainLogicUnit source, MainLogicUnit owner, Skill skill, int buffID, object[] args) {
        // TODO 1
        //return null;
        BuffCfg cfg = GetBuffConfigById(buffID);
        switch(cfg.buffType) {
            case BuffTypeEnum.MoveAttack:
                return new MoveAttackBuff(source, owner, skill, buffID, args);
            case BuffTypeEnum.MoveSpeed_Single:
                return new MoveSpeedBuff_Single(source, owner, skill, buffID, args);
            case BuffTypeEnum.MoveSpeed_DynamicGroup:
                return new MoveSpeedBuff_DynamicGroup(source, owner, skill, buffID, args);
            case BuffTypeEnum.ModifySkill:
                return new CommonModifySkillBuff(source, owner, skill, buffID, args);
            case BuffTypeEnum.Silense_Single:
                return new SilenseBuff_Single(source, owner, skill, buffID, args);
            case BuffTypeEnum.ArthurMark:
                return new ArthurMarkBuff(source, owner, skill, buffID, args);
            case BuffTypeEnum.HPCure:
                return new HPCureBuff_Single(source, owner, skill, buffID, args);
            case BuffTypeEnum.Damage_DynamicGroup:
                return new DamageBuff_DynamicGroup(source, owner, skill, buffID, args);
            case BuffTypeEnum.Knockup_Group:
                 return new KnockUpBuff_Group(source, owner, skill, buffID, args);
            case BuffTypeEnum.TargetFlashMove:
                return new TargetFlashMoveBuff(source, owner, skill, buffID, args);
            case BuffTypeEnum.ExecuteDamage:
                return new ExecuteDamageBuff(source, owner, skill, buffID, args);
             case BuffTypeEnum.Damage_StaticGroup:
                 return new DamageBuff_StaticGroup(source, owner, skill, buffID, args);
             
             case BuffTypeEnum.HouyiPasvAttackSpeed:
                 return new HouyiPasvAttackSpeedBuff(source, owner, skill, buffID, args);
             case BuffTypeEnum.HouyiPasvSkillModify:
                 return new HouyiMultipleSkillModifyBuff(source, owner, skill, buffID, args);
             case BuffTypeEnum.HouyiPasvMultiArrow:
                 return new HouyiMultipleArrowBuff(source, owner, skill, buffID, args);
            case BuffTypeEnum.HouyiActiveSkillModify:
                return new HouyiScatterSkillModifyBuff(source, owner, skill, buffID, args);
            case BuffTypeEnum.Scatter:
                return new HouyiScatterArrowBuff(source, owner, skill, buffID, args);
            case BuffTypeEnum.HouyiMixedMultiScatter:
                return new HouyiMixedMultiScatterBuff(source, owner, skill, buffID, args);
            case BuffTypeEnum.MoveSpeed_StaticGroup:
                return new MoveSpeedBuff_StaticGroup(source, owner, skill, buffID, args);
            case BuffTypeEnum.Stun_Single_DynamicTime:
                return new StunBuff_DynamicTime(source, owner, skill, buffID, args);
            case BuffTypeEnum.JinxRocketMixed_DynamicGroup:
                return new JinxRocketMixedBuff_DynamicGroup(source, owner, skill, buffID, args);
            case BuffTypeEnum.NSSpeedUpPasv:
                return new NSSpeedPasvBuff(source, owner, skill, buffID, args);
            // TOADD
            case BuffTypeEnum.None:
            default:
                this.Error("Create Buff Failed,BuffID:" + buffID);
                return null;
        }
    }

    private Dictionary<int, BuffCfg> buffCfgDic;
    
    public void LoadBuffs()
    {
        // TODO 可以改反射或者做个迭代器，目前手动
        buffCfgDic = new Dictionary<int, BuffCfg>();
        foreach (var cfg in configTable.TbNormalBuffCfg.DataList)
        {
            buffCfgDic.Add(cfg.buffId, cfg);
        }
        foreach (var cfg in configTable.TbDamageBuffCfg_DynamicGroup.DataList)
        {
            buffCfgDic.Add(cfg.buffId, cfg);
        }
        foreach (var cfg in configTable.TbDamageBuffCfg_StaticGroup.DataList)
        {
            buffCfgDic.Add(cfg.buffId, cfg);
        }
        foreach (var cfg in configTable.TbStunBuffCfg_DynamicTime.DataList)
        {
            buffCfgDic.Add(cfg.buffId, cfg);
        }
        foreach (var cfg in configTable.TbArthurMarkBuffCfg.DataList)
        {
            buffCfgDic.Add(cfg.buffId, cfg);
        }
        foreach (var cfg in configTable.TbExecuteDamageBuffCfg.DataList)
        {
            buffCfgDic.Add(cfg.buffId, cfg);
        }
        foreach (var cfg in configTable.TbMoveSpeedBuffCfg.DataList)
        {
            buffCfgDic.Add(cfg.buffId, cfg);
        }
        foreach (var cfg in configTable.TbHPCureBuffCfg.DataList)
        {
            buffCfgDic.Add(cfg.buffId, cfg);
        }
        foreach (var cfg in configTable.TbCommonModifySkillBuffCfg.DataList)
        {
            buffCfgDic.Add(cfg.buffId, cfg);
        }
        foreach (var cfg in configTable.TbHouyiMultipleArrowBuffCfg.DataList)
        {
            buffCfgDic.Add(cfg.buffId, cfg);
        }
        foreach (var cfg in configTable.TbHouyiScatterArrowBuffCfg.DataList)
        {
            buffCfgDic.Add(cfg.buffId, cfg);
        }
        foreach (var cfg in configTable.TbTargetFlashMoveBuffCfg.DataList)
        {
            buffCfgDic.Add(cfg.buffId, cfg);
        }
        foreach (var cfg in configTable.TbHouyiMixedMultiScatterBuffCfg.DataList)
        {
            buffCfgDic.Add(cfg.buffId, cfg);
        }
        foreach (var cfg in configTable.TbHouyiMultipleSkillModifyBuffCfg.DataList)
        {
            buffCfgDic.Add(cfg.buffId, cfg);
        }
        foreach (var cfg in configTable.TbHouyiPasvAttackSpeedBuffCfg.DataList)
        {
            buffCfgDic.Add(cfg.buffId, cfg);
        }
        foreach (var cfg in configTable.TbHouyiScatterSkillModifyBuffCfg.DataList)
        {
            buffCfgDic.Add(cfg.buffId, cfg);
        }
        foreach (var cfg in configTable.TbJinxRocketMixedBuffCfg_DynamicGroup.DataList)
        {
            buffCfgDic.Add(cfg.buffId, cfg);
        }
        foreach (var cfg in configTable.TbNSSpeedBuffCfg.DataList)
        {
            buffCfgDic.Add(cfg.buffId, cfg);
        }
    }
    
    public BuffCfg GetBuffConfigById(int id) {
        
        if (buffCfgDic.TryGetValue(id,out var cfg))
        {
            return cfg;
        }
        
        // TODO Test buff
     //    return Test(id);
     //    BuffCfg Test(int test_id)
     //    {
     //        BuffCfg buff_10300 = new HouyiPasvAttackSpeedBuffCfg {
     //      //通用buff属性
     //      buffId = 10300,
     //      buffName = "被动攻速加成叠加",
     //      buffType = BuffTypeEnum.HouyiPasvAttackSpeed,
     //
     //      attacher = AttachTypeEnum.Caster,
     //      impacter = null,
     //
     //      buffDelay = 0,
     //      buffInterval = 66,
     //      buffDuration = -1,
     //
     //      //专有属性
     //      overCount = 3,
     //      speedAddtion = 40,
     //      resetTime = 2500,
     //  };
     //        BuffCfg buff_10301 = new NSSpeedBuffCfg {
     //    buffId = 10301,
     //      buffName = "被动攻速移速加成、不可叠加",
     //      buffType = BuffTypeEnum.NSSpeedUpPasv,
     //
     //      attacher = AttachTypeEnum.Caster,
     //      impacter = null,
     //
     //      buffDelay = 0,
     //      buffInterval = 66,
     //      buffDuration = -1,
     //
     //      //专有属性
     //      buffAudio = "jinx_speedup",
     //      moveSpeedPer = 100,
     //      atkSpeedPer = 100,
     //      resetTime = 3000,
     //  };
     //
     //        // skill 1
     //        BuffCfg buff_10310 = new CommonModifySkillBuffCfg {
     //            buffId = 10310,
     //            buffName = "替换普攻到炮形态",
     //            buffType = BuffTypeEnum.ModifySkill,
     //
     //            attacher = AttachTypeEnum.Caster,
     //            impacter = null,
     //            
     //            buffDuration = -1,
     //
     //            //专有属性
     //            originalID = 1030,
     //            replaceID = 1034,
     //            times = -1,
     //            replaceIconId = 0,
     //        };
     //        
     //        BuffCfg buff_10311 = new CommonModifySkillBuffCfg {
     //            buffId = 10311,
     //            buffName = "替换Sk1到炮状态",
     //            buffType = BuffTypeEnum.ModifySkill,
     //
     //            attacher = AttachTypeEnum.Caster,
     //            impacter = null,
     //            
     //            buffDuration = -1,
     //
     //            //专有属性
     //            originalID = 1031,
     //            replaceID = 1035,
     //            times = -1,
     //            replaceIconId = 1,
     //        };
     //        
     //        BuffCfg buff_10312 = new CommonModifySkillBuffCfg {
     //            buffId = 10312,
     //            buffName = "替换普攻到枪形态",
     //            buffType = BuffTypeEnum.ModifySkill,
     //
     //            attacher = AttachTypeEnum.Caster,
     //            impacter = null,
     //            
     //            buffDuration = -1,
     //
     //            //专有属性
     //            originalID = 1030,
     //            replaceID = 1030,
     //            times = -1,
     //            replaceIconId = 0,
     //        };
     //        
     //        BuffCfg buff_10313 = new CommonModifySkillBuffCfg {
     //            buffId = 10313,
     //            buffName = "替换Sk1到枪状态",
     //            buffType = BuffTypeEnum.ModifySkill,
     //
     //            attacher = AttachTypeEnum.Caster,
     //            impacter = null,
     //            
     //            buffDuration = -1,
     //
     //            //专有属性
     //            originalID = 1031,
     //            replaceID = 1031,
     //            times = -1,
     //            replaceIconId = 1,
     //        };
     //        
     //        BuffCfg buff_10314 = new DamageBuffCfg_StaticGroup {
     //            buffId = 10314, 
     //            buffName = "范围伤害",
     //
     //     attacher = AttachTypeEnum.Bullet,
     //     impacter = new TargetCfg {
     //         targetTeam = TargetTeamEnum.Friend,
     //         selectRule = SelectRuleEnum.PositionClosestMulti,
     //         targetTypeArr = new UnitTypeEnum[] {
     //             UnitTypeEnum.Hero,
     //             UnitTypeEnum.Soldier,
     //         },
     //         selectRange = 2f
     //     },
     //
     //     buffDelay = 0,
     //     buffInterval = 0,
     //     buffDuration = 0,
     //     buffType = BuffTypeEnum.Damage_StaticGroup,
     //     staticPosType = StaticPosTypeEnum.BulletHitTargetPos,
     //
     //     //damageAudio = "com_hit1",
     //     //effect
     //     effectDestoryExtend = 1f,
     //     buffEffect = "jinx_bullets/Bullet_Boom",
     //
     //     damage = 200,
     // };
     //        
     //        // skill2
     //        
     //        BuffCfg buff_10320 = new MoveSpeedBuffCfg {
     //            buffId = 10320,
     //     buffName = "减速",
     //     buffType = BuffTypeEnum.MoveSpeed_Single,
     //
     //     attacher = AttachTypeEnum.Bullet,
     //     impacter = null,
     //
     //     buffDelay = 0,
     //     buffInterval = 0,
     //     buffDuration = 2000,
     //     effectDestoryExtend = 2.5f,
     //     buffEffect = "jinx_bullets/Beam_Boom",
     //     amount = -50,
     // };
     //        BuffCfg buff_10330 = new JixRocketMixedBuffCfg_DynamicGroup {
     //            buffId = 10330, 
     //            buffName = "Jinx大招混合范围伤害",
     //
     //            attacher = AttachTypeEnum.Bullet,
     //            impacter = new TargetCfg {
     //                targetTeam = TargetTeamEnum.Friend,
     //                selectRule = SelectRuleEnum.PositionClosestMulti,
     //                targetTypeArr = new UnitTypeEnum[] {
     //                    UnitTypeEnum.Hero,
     //                    UnitTypeEnum.Soldier,
     //                },
     //                selectRange = 3.5f
     //            },
     //
     //            buffDelay = 0,
     //            buffInterval = 0,
     //            buffDuration = 0,
     //            buffType = BuffTypeEnum.JixRocketMixed_DynamicGroup,
     //            staticPosType = StaticPosTypeEnum.BulletHitTargetPos,
     //
     //            //damageAudio = "com_hit1",
     //            //effect
     //            //buffEffect = "Effect_sk3",
     //
     //            minTime = 100,
     //            maxTime = 1000,
     //            minDamage = 50,
     //            maxDamage = 500,
     //            perDamagedHit = 35,
     //            perSplash = 80,
     //            
     //            effectDestoryExtend = 2.5f,
     //            buffEffect = "jinx_bullets/Rocket_Boom",
     //        };
     //        
     //        switch (test_id)
     //        {
     //            case 10300: return buff_10300;
     //            case 10301: return buff_10301;
     //            case 10310: return buff_10310;
     //            case 10311: return buff_10311;
     //            case 10312: return buff_10312;
     //            case 10313: return buff_10313;
     //            case 10314: return buff_10314;
     //            case 10320: return buff_10320;
     //            case 10330: return buff_10330;
     //        }
     //        return null;
     //    }
        
        
        this.Error("Get Buff Config Failed,buffId:" + id);
        return null;

        // TODO 1
        // switch(buffId) {
        //     case 10100:
        //         return ResBuffConfigs.buff_10100;
        //     //Arthur1技能
        //     case 10110://移速加速
        //         return ResBuffConfigs.buff_10110;
        //     case 10111:
        //         return ResBuffConfigs.buff_10111;
        //     case 10140:
        //         return ResBuffConfigs.buff_10140;
        //     case 10141:
        //         return ResBuffConfigs.buff_10141;
        //     case 10142:
        //         return ResBuffConfigs.buff_10142;
        //     //Arthur2技能
        //     case 10120:
        //         return ResBuffConfigs.buff_10120;
        //     //Arthur3技能
        //     case 10130:
        //         return ResBuffConfigs.buff_10130;
        //     case 10131:
        //         return ResBuffConfigs.buff_10131;
        //     case 10132:
        //         return ResBuffConfigs.buff_10132;
        //     case 10133:
        //         return ResBuffConfigs.buff_10133;
        //     // //Houyi被动技能
        //     case 10200:
        //         return ResBuffConfigs.buff_10200;
        //     case 10201:
        //         return ResBuffConfigs.buff_10201;
        //     case 10250:
        //         return ResBuffConfigs.buff_10250;
        //     //Houyi1技能
        //     case 10210://技能替换
        //         return ResBuffConfigs.buff_10210;
        //     case 10240://scatter
        //         return ResBuffConfigs.buff_10240;
        //     case 10260://mixed
        //         return ResBuffConfigs.buff_10260;
        //     // //Houyi2技能
        //     case 10220:
        //         return ResBuffConfigs.buff_10220;
        //     case 10221:
        //         return ResBuffConfigs.buff_10221;
        //     case 10222:
        //         return ResBuffConfigs.buff_10222;
        //     case 10223:
        //         return ResBuffConfigs.buff_10223;
        //     // //Houyi3技能
        //     case 10230:
        //         return ResBuffConfigs.buff_10230;
        //     case 10231:
        //         return ResBuffConfigs.buff_10231;
        //     //通用
        //     case 90000:
        //         return ResBuffConfigs.buff_90000;
        //     default:
        //         break;
        // }
        //this.Error("Get Buff Config Failed,buffId:" + buffId);
        // return null;
    }

    #endregion

    #region Bullet信息

    public Bullet CreateBullet(MainLogicUnit source, MainLogicUnit target, Skill skill) {
        switch(skill.skillCfg.bulletCfg.bulletType) {
            case BulletTypeEnum.SkillTarget:
                return new TargetBullet(source, target, skill);
            case BulletTypeEnum.UIDirection:
                return new DirectionBullet(source, skill);
            case BulletTypeEnum.UIPosition: // TODO 
            case BulletTypeEnum.BuffSearch: // TODO 
            default:
                this.Error("Create Bullet Error.");
                return null;
        }
    }

    #endregion
    
    #region 地图信息
    
    public MapCfg GetMapConfigById(int mapId)
    {
        // TODO 简写了,可以改成读取配置表
        switch (mapId)
        {
           case 101:
                return new MapCfg {
                    mapId = 101,
                    //blueBorn = new PEVector3(-27, 0, 0),
                    //redBorn = new PEVector3(27, 0, 0),
                    blueBorn = new PEVector3(-5, 0, -3),
                    redBorn = new PEVector3(5, 0, -3),
                    towerIDArr = new int[] { 1001, 1002, 2001, 2002 },
                    towerPosArr = new PEVector3[] {
                        new PEVector3(-(PEInt)12.6f, 0, -(PEInt)0.2f),
                        new PEVector3(-(PEInt)24.1f, 0, -(PEInt)0.2f),
                        new PEVector3((PEInt)12.6f, 0, -(PEInt)0.2f),
                        new PEVector3((PEInt)24.1f, 0, -(PEInt)0.2f)
                    },
                    bornDelay = 15000,
                    bornInterval = 2000,
                    waveInterval = 50000,
                    blueSoldierIDArr = new int[] { 1003, 1003, 1004, 1004 },
                    blueSoldierPosArr = new PEVector3[] {
                        new PEVector3(-22,0,-(PEInt)1.7f),
                        new PEVector3(-22,0,(PEInt)1.7f),
                        new PEVector3(-22,0,-(PEInt)1.7f),
                        new PEVector3(-22,0,(PEInt)1.7f),
                    },
                    redSoldierIDArr = new int[] { 2003, 2003, 2004, 2004 },
                    redSoldierPosArr = new PEVector3[] {
                        new PEVector3(22,0,-(PEInt)1.7f),
                        new PEVector3(22,0,(PEInt)1.7f),
                        new PEVector3(22,0,-(PEInt)1.7f),
                        new PEVector3(22,0,(PEInt)1.7f),
                    },

                };
            case 102:
                return new MapCfg {
                    mapId = 102,
                    blueBorn = new PEVector3(-5, 0, -3),
                    redBorn = new PEVector3(5, 0, -3),
                    towerIDArr = new int[] { 1001, 1002, 2001, 2002 },
                    towerPosArr = new PEVector3[] {
                        new PEVector3(-(PEInt)12.6f, 0, -(PEInt)0.2f),
                        new PEVector3(-(PEInt)24.1f, 0, -(PEInt)0.2f),
                        new PEVector3((PEInt)12.6f, 0, -(PEInt)0.2f),
                        new PEVector3((PEInt)24.1f, 0, -(PEInt)0.2f)
                    },
                    bornDelay = 15000,
                    bornInterval = 2000,
                    waveInterval = 50000,
                    blueSoldierIDArr = new int[] { 1003, 1003, 1004, 1004 },
                    blueSoldierPosArr = new PEVector3[] {
                        new PEVector3(-22,0,-(PEInt)1.7f),
                        new PEVector3(-22,0,(PEInt)1.7f),
                        new PEVector3(-22,0,-(PEInt)1.7f),
                        new PEVector3(-22,0,(PEInt)1.7f),
                    },
                    redSoldierIDArr = new int[] { 2003, 2003, 2004, 2004 },
                    redSoldierPosArr = new PEVector3[] {
                        new PEVector3(22,0,-(PEInt)1.7f),
                        new PEVector3(22,0,(PEInt)1.7f),
                        new PEVector3(22,0,-(PEInt)1.7f),
                        new PEVector3(22,0,(PEInt)1.7f),
                    },
                };
        }

        return null;
    }

    #endregion
    
    #region 地图加载

    private Action sceneBPMethod = null;
    public void AsyncLoadScene(string sceneName,Action<int> updateProgress,Action afterAll)
    {
        StartCoroutine(StartLoading(sceneName,updateProgress,afterAll));
        
        // GameRootResources.Instance().loadingWindow.SetWindowState();
        //
        // AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(sceneName);
        // //sceneAsync.allowSceneActivation = true;
        // prgCB = () => {
        //     float val = sceneAsync.progress;
        //     GameRootResources.Instance().loadingWindow.SetProgress(val);
        //     if (val >= 0.9) {
        //         if (afterAll != null) {
        //             afterAll();
        //         }
        //         prgCB = null;
        //         sceneAsync = null;
        //         GameRootResources.Instance().loadingWindow.SetWindowState(false);
        //     }
        // };
    }

    private void Update()
    {
        if (sceneBPMethod != null)
        {
            sceneBPMethod();
            sceneBPMethod = null;
        }
    }
    
    /// <summary>
    /// 优化进度读取：协程刷新进度。updateProgress是更新进度函数
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    private IEnumerator StartLoading(string sceneName,Action<int> updateProgress,Action afterAll)
    {
        int displayProgress = 0;
        int toProgress = 0;
        // 卸载当前场景
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName); 
        
        // 不让场景自动跳转，progress也最多只能到90%
        op.allowSceneActivation = false;
        
        while (op.progress < 0.9f)
        {
            toProgress = (int)(op.progress * 100);
            // Debug.Log("below90: " + displayProgress + " , " + op.progress + " , " + toProgress);
            while (displayProgress < toProgress)
            {
                // 减少请求次数,一次涨15%进度
                var tmpProgress = displayProgress + 15;
                displayProgress = tmpProgress > toProgress ? tmpProgress : toProgress;
                //GameRootResources.Instance().loadingWindow.SetProgress(displayProgress);
                updateProgress?.Invoke(displayProgress);
                yield return new WaitForEndOfFrame();
            }
        }

        toProgress = 100;
       
        while (displayProgress < toProgress)
        {
            // 减少请求次数,一次涨25%进度;90%后,最后10%为了效果,一次涨1%进度.加载速度可能过快,直接跳过上面的0.9f判断,所以改为25%
            var tmpProgress = displayProgress + 20;
            if (tmpProgress <= 90)
            {
                // 不到70的每次都叠加20%
                displayProgress = tmpProgress;
            }
            else if (tmpProgress > 90 && tmpProgress < 110)
            {
                // 70-89.99的直接跳到90%
                displayProgress = 90;
            }
            else
            {
                // >=90的叠加1%
                ++displayProgress;
            }
            
            //GameRootResources.Instance().loadingWindow.SetProgress(displayProgress);
            updateProgress?.Invoke(displayProgress);
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;
        
        //loadingWindow.SetWindowState(false);
        // 赋值回调函数
        sceneBPMethod = afterAll;
        
    }

    #endregion

 
}
