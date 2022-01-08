/****************************************************
    文件：HeroView.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：英雄表现控制
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能释放显示、旁白
/// </summary>
public class HeroView : MainViewUnit
{
    public Transform sk1;
    public Transform sk2;
    public Transform sk3;
    
    private Hero hero;

    public override void Init(LogicUnit logicUnit)
    {
        base.Init(logicUnit);
        
        hero = logicUnit as Hero;
        
        skillRange.gameObject.SetActive(false);
        if (sk1 != null) sk1.gameObject.SetActive(false);
        if (sk2 != null) sk2.gameObject.SetActive(false);
        if (sk3 != null) sk3.gameObject.SetActive(false);
        
        
    }
    
    protected override Vector3 GetUnitViewDir()
    {
        if(hero.IsSkillSpelling()) {
            return viewTargetDir;
        }
        // 玩家朝向使用UI输入位置朝向，不使用物理引擎运算修正方向
        // 从而防止特殊情况，比如撞上墙会被物理引擎修正朝向
        return hero.InputDir.ConvertViewVector3();
    }

    public void DisableSkillGuide(int skillIndex)
    {
        switch (skillIndex)
        {
            case 1:
                if(sk1 != null) sk1.gameObject.SetActive(false);
                break;
            case 2:
                if(sk2 != null) sk2.gameObject.SetActive(false);
                break;
            case 3:
                if(sk3 != null) sk3.gameObject.SetActive(false);
                break;
        }
    }
    
    
    public void SetSkillGuide(int skillIndex,bool state,ReleaseModeEnum mode,Vector3 vector)
    {
        switch (skillIndex)
        {
            case 1:
                sk1.gameObject.SetActive(state);
                if (state)
                {
                    UpdateSkillGuide(sk1,mode,vector);
                }
                break;
            case 2:
                sk2.gameObject.SetActive(state);
                if (state)
                {
                    UpdateSkillGuide(sk2,mode,vector);
                }
                break;
            case 3:
                sk3.gameObject.SetActive(state);
                if (state)
                {
                    UpdateSkillGuide(sk3,mode,vector);
                }
                break;
        }
    }

    void UpdateSkillGuide(Transform sk,ReleaseModeEnum mode,Vector3 vector)
    {
        if (mode == ReleaseModeEnum.Position)
        {
            sk.localPosition = vector;
        }
        else if (mode == ReleaseModeEnum.Direction)
        {
            // 计算指向与正向向量0,1的夹角
            float angle = Vector2.SignedAngle(new Vector2(vector.x,vector.z),new Vector2(0,1));
            sk.localEulerAngles = new Vector3(0, angle,0);
        }
    }
}
