/****************************************************
    文件：ResSvc.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                };
            case 102:
                return new UnitCfg()
                {
                    unitId = 102,
                    unitName = "后羿",
                    resName = "houyi",
                };
        }

        return null;
    }

    #endregion
    
    
}
