/****************************************************
    文件：FightMgr.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：战斗管理器
*****************************************************/

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
    
    public void Init(List<BattleHeroData> battleHeroList,MapCfg mapCfg)
    {
        // 初始化碰撞环境
        InitEnv();
        // 防御塔

        // 英雄

        // 小兵

        // delay后出生小兵,按波次出生


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
    
}
