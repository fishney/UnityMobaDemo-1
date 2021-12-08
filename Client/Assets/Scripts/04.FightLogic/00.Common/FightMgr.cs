/****************************************************
    文件：FightMgr.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：战斗管理器
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using HOKProtocol;
using PEMath;
using PEPhysx;
using UnityEngine;

public class FightMgr : MonoBehaviour
{
    private MapRoot mapRoot;
    private EnvColliders logicEnv;

    private List<Hero> heroList;

    private Transform transFollow;
    
    public void Init(List<BattleHeroData> battleHeroList,MapCfg mapCfg)
    {
        heroList = new List<Hero>();
        
        // 初始化碰撞环境
        InitEnv();
        // 防御塔

        // 英雄
        InitHero(battleHeroList,mapCfg);
        
        // 小兵

        // delay后出生小兵,按波次出生


    }
    
    /// <summary>
    /// 由服务器数据帧进行驱动
    /// </summary>
    public void Tick()
    {
        for (int i = 0; i < heroList.Count; i++)
        {
            heroList[i].LogicTick();
        }
    }
    
    public void UnInit()
    {
        heroList.Clear();
    }
    
    /// <summary>
    /// 由画面驱动
    /// </summary>
    private void Update()
    {
        if (transFollow != null)
        {
            mapRoot.transCameraRoot.position = transFollow.position;
        }
    }
    
    void InitEnv()
    {
        Transform transMapRoot = GameObject.FindGameObjectWithTag("MapRoot").transform;
        mapRoot = transMapRoot.GetComponent<MapRoot>();
        List<ColliderConfig> envColliderCfgList = GenerateEnvColliderCfgs(mapRoot.transEnvCollider);
        logicEnv = new EnvColliders()
        {
            envColliCfgLst = envColliderCfgList,
        };
        logicEnv.Init();
        
    }

    void InitHero(List<BattleHeroData> battleHeroList,MapCfg mapCfg)
    {
        int sep = battleHeroList.Count / 2;
        var blueTeamHero = new Hero[sep];
        var redTeamHero = new Hero[sep];
        for (int i = 0; i < battleHeroList.Count; i++)
        {
            HeroData hd = new HeroData()
            {
                heroId = battleHeroList[i].heroId,
                posIndex = i,
                userName = battleHeroList[i].userName,
                unitCfg = ResSvc.Instance().GetUnitConfigById(battleHeroList[i].heroId),
            };

            Hero hero;
            if (i < sep)
            {
                hd.teamEnum = TeamEnum.Blue;
                hd.bornPos = mapCfg.blueBorn;
                hero = new Hero(hd);
                blueTeamHero[i] = hero;
            }
            else
            {
                hd.teamEnum = TeamEnum.Red;
                hd.bornPos = mapCfg.redBorn;
                hero = new Hero(hd);
                redTeamHero[i-sep] = hero;
            }
            
            hero.LogicInit();
            heroList.Add(hero);
        }

        CalcRule.blueTeamHero = blueTeamHero;
        CalcRule.redTeamHero = redTeamHero;
    }

  

    public void InitCamFollowTrans(int posIndex)
    {
        transFollow = heroList[posIndex].mainViewUnit.transform;
    }
    
    

    // TODO 以后要学，这里是录入位置、大小、朝向、半径等信息，且转换浮点型Vector3为定点型PEVector3
    List<ColliderConfig> GenerateEnvColliderCfgs(Transform transEnvRoot)
    {
        List<ColliderConfig> envColliderCfgList = new List<ColliderConfig>();
        BoxCollider[] boxArr = transEnvRoot.GetComponentsInChildren<BoxCollider>();
        
        // Box碰撞
        for (int i = 0; i < boxArr.Length; i++)
        {
            Transform trans = boxArr[i].transform;
            var cfg = new ColliderConfig()
            {
                // PEVector3是定点运算!!!
                mPos = new PEVector3(trans.position)
            };
            cfg.mSize = new PEVector3(trans.localScale / 2);
            cfg.mType = ColliderType.Box;
            cfg.mAxis = new PEVector3[3];
            cfg.mAxis[0] = new PEVector3(trans.right);
            cfg.mAxis[1] = new PEVector3(trans.up);
            cfg.mAxis[2] = new PEVector3(trans.forward);
            
            envColliderCfgList.Add(cfg);
        }

        // Cylinder碰撞
        CapsuleCollider[] cylinderArr = transEnvRoot.GetComponentsInChildren<CapsuleCollider>();
        for (int i = 0; i < cylinderArr.Length; i++)
        {
            Transform trans = cylinderArr[i].transform;
            var cfg = new ColliderConfig()
            {
                // PEVector3是定点运算!!!
                mPos = new PEVector3(trans.position)
            };
            cfg.mType = ColliderType.Cylinder;
            cfg.mRadius = (PEInt) (trans.localScale.x / 2); // 半径
            
            envColliderCfgList.Add(cfg);
        }
        
        return envColliderCfgList;
    }

    public List<PEColliderBase> GetEnvColliders()
    {
        return logicEnv.GetEnvColliders();
    }

    public void InputKey(List<OpKey> keyList)
    {
        for (int i = 0; i < keyList.Count; i++)
        {
            OpKey key = keyList[i];
            MainLogicUnit hero = heroList[key.opIndex];
            hero.InputKey(key);
        }
    }

    public MainLogicUnit GetSelfHero(int posIndex)
    {
        return heroList[posIndex];
    }
}
