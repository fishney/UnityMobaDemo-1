/****************************************************
    文件：AudioSvc.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSvc : GameRootMonoSingleton<AudioSvc>
{
    public void InitSvc()
    {
        Debug.Log("AudioSvc Init Completed.");
    }
    
    public AudioSource bgAudio;
    public AudioSource uiAudio;
    
    public void PlayBGMusic(string name, bool isLoop = true)
    {
        AudioClip audioClip = ResSvc.Instance().LoadAudio("ResAudio/" + name);
        // 检测是否和播放的一样
        if (bgAudio.clip == null || bgAudio.clip.name != audioClip.name)
        {
            bgAudio.clip = audioClip;
            bgAudio.loop = true;
        }

        if (!bgAudio.isPlaying)
        {
            bgAudio.Play();
        }
        
    }
    
    public void PlayUIAudio(string name, bool isLoop = true)
    {
        AudioClip uiClip = ResSvc.Instance().LoadAudio("ResAudio/" + name);
        uiAudio.clip = uiClip;
        uiAudio.Play();
    }
    
    public void PlayCustomClip(string name, AudioSource audioSource)
    {
        AudioClip playerClip = ResSvc.Instance().LoadAudio("ResAudio/" + name);
        audioSource.clip = playerClip;
        audioSource.Play();
    }

    public void StopBGM()
    {
        if (bgAudio != null)
        {
            bgAudio.Stop();
        }
    }
}
