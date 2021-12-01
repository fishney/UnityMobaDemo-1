/****************************************************
    文件：MainViewUnit.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：主要表现控制
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻速、移速的表现速度
/// 技能动画
/// 血条信息
/// 小地图显示
/// </summary>
public abstract class MainViewUnit : ViewUnit
{
    public Transform skillRange;
    
    public float fade;

    public Animation ani;

    private float aniMoveSpeedBase;

    private MainLogicUnit mainLogicUnit = null;
    public override void Init(LogicUnit logicUnit)
    {
        base.Init(logicUnit);
        mainLogicUnit = logicUnit as MainLogicUnit;
        
        // 移动速度
        aniMoveSpeedBase = mainLogicUnit.LogicMoveSpeed.RawFloat;
    }

    protected override void Update()
    {
        if (mainLogicUnit.isDirChanged)
        {
            // 朝向变更
            if (mainLogicUnit.LogicDir.ConvertViewVector3().Equals(Vector3.zero))
            {
                PlayAni("free");
            }
            else
            {
                PlayAni("walk");
            }
        }
        
        base.Update();
    }

    public override void PlayAni(string aniName)
    {
        if (aniName.Contains("walk"))
        {
            // 如果有buff让速度提升，就按速率播放：
            float moveRate = mainLogicUnit.LogicMoveSpeed.RawFloat / aniMoveSpeedBase;
            ani[aniName].speed = moveRate;
            ani.CrossFade(aniName,fade / moveRate);
        }
        else
        {
            if (ani == null)
            {
                this.Log("ani is null");
            }
            ani.CrossFade(aniName,fade);
        }
    }

    public void SetAtkSkillRange(bool state, float range = 2.5f)
    {
        if (skillRange != null)
        {
            // 加上角色本身的半径长度
            range += mainLogicUnit.ud.unitCfg.colliCfg.mRadius.RawFloat;
            // 调整技能提示框的缩放。为什么/2.5f？因为技能提示框的素材大概占用2.5格单位。
            skillRange.localScale = new Vector3(range / 2.5f, range / 2.5f, 1);
            skillRange.gameObject.SetActive(state);
        }
    }
}
