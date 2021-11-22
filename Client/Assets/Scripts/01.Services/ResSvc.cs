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
                    
                    hp = 6500,
                    def = 0,
                    moveSpeed = 5,
                    
                    colliCfg = new ColliderConfig
                    {
                        mType = ColliderType.Cylinder, //所有角色都是圆柱体
                        mRadius = (PEInt)0.5f,
                    }
                };
            case 102:
                return new UnitCfg()
                {
                    unitId = 102,
                    unitName = "后羿",
                    resName = "houyi",
                    
                    hp = 3500,
                    def = 10,
                    moveSpeed = 5,
                    
                    colliCfg = new ColliderConfig
                    {
                        mType = ColliderType.Cylinder, //所有角色都是圆柱体
                        mRadius = (PEInt)0.5f,
                    }
                };
        }

        return null;
    }

    #endregion

    #region 地图信息
    
    public MapCfg GetMapConfigById(int mapId)
    {
        // TODO 简写了,可以改成读取配置表
        switch (mapId)
        {
            case 101:
                return new MapCfg()
                {
                    mapId = 101,
                    bornDelay = 15000,
                    bornInterval = 2000,
                    waveInterval = 50000,
                };
            case 102:
                return new MapCfg()
                {
                    mapId = 102,
                    bornDelay = 15000,
                    bornInterval = 2000,
                    waveInterval = 50000,
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
                updateProgress.Invoke(displayProgress);
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
            updateProgress.Invoke(displayProgress);
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;
        
        //loadingWindow.SetWindowState(false);
        
        // 赋值回调函数
        sceneBPMethod = afterAll;
        
    }

    #endregion
}
