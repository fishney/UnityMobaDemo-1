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
using PEMath;
using PEPhysx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResSvc : GameRootMonoSingleton<ResSvc>
{
    public void InitSvc()
    {
        
        
        Debug.Log("ResSvc Init Completed.");
    }

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
    
    
    
    public UnitCfg GetUnitConfigById(int unitId)
    {
        // TODO 简写了,可以改成读取配置表
        switch (unitId)
        {
            case 101:
                return new UnitCfg()
                {
                    unitId = 101,
                    unitName = "亚瑟",
                    resName = "arthur",
                    hitHeight = (PEInt)1.5F,
                    
                    hp = 6500,
                    def = 0,
                    moveSpeed = 5,
                    
                    colliCfg = new ColliderConfig
                    {
                        mType = ColliderType.Cylinder, //所有角色都是圆柱体
                        mRadius = (PEInt)0.5f,
                    },
                    pasvBuff = new []{10100},
                    skillArr = new []{1010,1011,1012,1013},
                };
            case 102:
                return new UnitCfg()
                {
                    unitId = 102,
                    unitName = "后羿",
                    resName = "houyi",
                    hitHeight = (PEInt)1.5F,
                    
                    hp = 3500,
                    def = 10,
                    moveSpeed = 5,
                    
                    colliCfg = new ColliderConfig
                    {
                        mType = ColliderType.Cylinder, //所有角色都是圆柱体
                        mRadius = (PEInt)0.5f,
                    },
                    pasvBuff = new int[] { 10200, 10201 },
                    skillArr = new []{1020,1021,1022,1023},
                };
            
            case 1001:
                return new UnitCfg {
                    unitId = 1001,
                    unitName = "蓝方一塔",
                    resName = "blueTower",
                    hitHeight = (PEInt)1.5f,
                    hp = 400,
                    def = 0,
                    colliCfg = new ColliderConfig {
                        mType = ColliderType.Cylinder,
                        mRadius = (PEInt)0.25f,
                    },
                    pasvBuff = null,
                    skillArr = new int[] { 10010 }
                };
            case 1002:
                return new UnitCfg {
                    unitId = 1002,
                    unitName = "蓝方水晶",
                    resName = "blueCrystal",
                    hitHeight = (PEInt)1f,
                    hp = 800,
                    def = 0,
                    colliCfg = new ColliderConfig {
                        mType = ColliderType.Cylinder,
                        mRadius = (PEInt)1f,
                    },
                    pasvBuff = null,
                    skillArr = new int[] { 10020 }
                };
            case 1003:
                return new UnitCfg {
                    unitId = 1003,
                    unitName = "蓝方近战小兵",
                    resName = "xb_blue_jz",
                    hitHeight = (PEInt)0.6f,
                    hp = 500,
                    def = 0,
                    moveSpeed = 2,
                    colliCfg = new ColliderConfig {
                        mType = ColliderType.Cylinder,
                        mRadius = (PEInt)0.25f,
                    },
                    pasvBuff = null,
                    skillArr = new int[] { 10030 }
                };
            case 1004:
                return new UnitCfg {
                    unitId = 1004,
                    unitName = "蓝方远程小兵",
                    resName = "xb_blue_yc",
                    hitHeight = (PEInt)0.6f,
                    hp = 300,
                    def = 0,
                    moveSpeed = 2,
                    colliCfg = new ColliderConfig {
                        mType = ColliderType.Cylinder,
                        mRadius = (PEInt)0.25f,
                    },
                    pasvBuff = null,
                    skillArr = new int[] { 10040 }
                };
            case 2001:
                return new UnitCfg {
                    unitId = 2001,
                    unitName = "红方一塔",
                    resName = "redTower",
                    hitHeight = (PEInt)1.5f,
                    hp = 400,
                    def = 0,
                    colliCfg = new ColliderConfig {
                        mType = ColliderType.Cylinder,
                        mRadius = (PEInt)0.25f,
                    },
                    pasvBuff = null,
                    skillArr = new int[] { 20010 }
                };
            case 2002:
                return new UnitCfg {
                    unitId = 2002,
                    unitName = "红方水晶",
                    resName = "redCrystal",
                    hitHeight = (PEInt)1f,
                    hp = 800,
                    def = 0,
                    colliCfg = new ColliderConfig {
                        mType = ColliderType.Cylinder,
                        mRadius = (PEInt)1f,
                    },
                    pasvBuff = null,
                    skillArr = new int[] { 20020 }
                };
            case 2003:
                return new UnitCfg {
                    unitId = 2003,
                    unitName = "红方近战小兵",
                    resName = "xb_red_jz",
                    hitHeight = (PEInt)0.6f,
                    hp = 500,
                    def = 0,
                    moveSpeed = 2,
                    colliCfg = new ColliderConfig {
                        mType = ColliderType.Cylinder,
                        mRadius = (PEInt)0.25f,
                    },
                    pasvBuff = null,
                    skillArr = new int[] { 20030 }
                };
            case 2004:
                return new UnitCfg {
                    unitId = 2004,
                    unitName = "红方远程小兵",
                    resName = "xb_red_yc",
                    hitHeight = (PEInt)0.6f,
                    hp = 300,
                    def = 0,
                    moveSpeed = 2,
                    colliCfg = new ColliderConfig {
                        mType = ColliderType.Cylinder,
                        mRadius = (PEInt)0.25f,
                    },
                    pasvBuff = null,
                    skillArr = new int[] { 20040 }
                };
        }

        return null;
    }

    #endregion

    #region 技能信息
    
    public SkillCfg GetSkillConfigById(int skillId)
    {
        switch (skillId)
        {
            case 1010:
                return ResSkillConfigs.sk_1010;
            case 1011:
                return ResSkillConfigs.sk_1011;
            case 1012:
                return ResSkillConfigs.sk_1012;
            case 1013:
                return ResSkillConfigs.sk_1013;
            case 1014:
                return ResSkillConfigs.sk_1014;
            case 1020:
                return ResSkillConfigs.sk_1020;
            case 1021:
                return ResSkillConfigs.sk_1021;
            case 1022:
                return ResSkillConfigs.sk_1022;
            case 1023:
                return ResSkillConfigs.sk_1023;
            case 1024:
                return ResSkillConfigs.sk_1024;
            case 1025:
                return ResSkillConfigs.sk_1025;
            case 1026:
                return ResSkillConfigs.sk_1026;
            
            //防御塔与水晶的技能
            case 10010:
                return ResSkillConfigs.sk_10010;
            case 10020:
                return ResSkillConfigs.sk_10020;
            case 10030:
                return ResSkillConfigs.sk_10030;
            case 10040:
                return ResSkillConfigs.sk_10040;
            case 20010:
                return ResSkillConfigs.sk_20010;
            case 20020:
                return ResSkillConfigs.sk_20020;
            case 20030:
                return ResSkillConfigs.sk_20030;
            case 20040:
                return ResSkillConfigs.sk_20040;
            default:
                this.Error("Get SkillCfg Failed,Id: " + skillId);
                return null;
        }
    }

    #endregion
    
    #region Buff信息

    public Buff CreateBuff(MainLogicUnit source, MainLogicUnit owner, Skill skill, int buffID, object[] args) {
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
            case BuffTypeEnum.Silense:
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
            // TOADD
            case BuffTypeEnum.None:
            default:
                this.Error("Create Buff Failed,BuffID:" + buffID);
                return null;
        }
    }
    
    public BuffCfg GetBuffConfigById(int buffId) {
        switch(buffId) {
            case 10100:
                return ResBuffConfigs.buff_10100;
            //Arthur1技能
            case 10110://移速加速
                return ResBuffConfigs.buff_10110;
            case 10111:
                return ResBuffConfigs.buff_10111;
            case 10140:
                return ResBuffConfigs.buff_10140;
            case 10141:
                return ResBuffConfigs.buff_10141;
            case 10142:
                return ResBuffConfigs.buff_10142;
            //Arthur2技能
            case 10120:
                return ResBuffConfigs.buff_10120;
            //Arthur3技能
            case 10130:
                return ResBuffConfigs.buff_10130;
            case 10131:
                return ResBuffConfigs.buff_10131;
            case 10132:
                return ResBuffConfigs.buff_10132;
            case 10133:
                return ResBuffConfigs.buff_10133;
            // //Houyi被动技能
            case 10200:
                return ResBuffConfigs.buff_10200;
            case 10201:
                return ResBuffConfigs.buff_10201;
            case 10250:
                return ResBuffConfigs.buff_10250;
            //Houyi1技能
            case 10210://技能替换
                return ResBuffConfigs.buff_10210;
            case 10240://scatter
                return ResBuffConfigs.buff_10240;
            case 10260://mixed
                return ResBuffConfigs.buff_10260;
            // //Houyi2技能
            case 10220:
                return ResBuffConfigs.buff_10220;
            case 10221:
                return ResBuffConfigs.buff_10221;
            case 10222:
                return ResBuffConfigs.buff_10222;
            case 10223:
                return ResBuffConfigs.buff_10223;
            // //Houyi3技能
            case 10230:
                return ResBuffConfigs.buff_10230;
            case 10231:
                return ResBuffConfigs.buff_10231;
            //通用
            case 90000:
                return ResBuffConfigs.buff_90000;
            default:
                break;
        }
        this.Error("Get Buff Config Failed,buffId:" + buffId);
        return null;
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
