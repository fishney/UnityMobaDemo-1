/****************************************************
    文件：MainViewUnit.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：主要表现控制
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using PEMath;
using UnityEngine;
using Random = UnityEngine.Random;

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
    private float aniAttackSpeedBase;

    private MainLogicUnit mainLogicUnit = null;

    private HPWindow hpWindow;
    private PlayWindow playWindow;
    //血条定位点
    public Transform hpRoot;
    
    public override void Init(LogicUnit logicUnit)
    {
        base.Init(logicUnit);
        mainLogicUnit = logicUnit as MainLogicUnit;
        
        // 移动速度
        aniMoveSpeedBase = mainLogicUnit.LogicMoveSpeed.RawFloat;
        aniAttackSpeedBase = mainLogicUnit.AttackSpeedRateCurrent.RawFloat;
        
        // 血条显示
        hpWindow = GameRootResources.Instance().hpWindow;
        hpWindow.AddHPItemInfo(mainLogicUnit,hpRoot,mainLogicUnit.Hp.RawInt);
        
        playWindow = GameRootResources.Instance().playWindow;
        playWindow.AddMiniIconItemInfo(mainLogicUnit);
        
        mainLogicUnit.OnHPChange += UpdateHP;
        mainLogicUnit.OnStateChange += UpdateState;
        mainLogicUnit.OnSlowDown += UpdateJui;
    }
    
    private void UpdateHP(int hp, JumpUpdateInfo ji)
    {
        if (ji != null)
        {
            float scaleRate = 1.0f * ClientConfig.ScreenStandardHeight / Screen.height;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1, 0));
            ji.pos = scaleRate * screenPos;
        }
        hpWindow.SetHPVal(mainLogicUnit,hp,ji);
    }
    
    public void UpdateState(StateEnum state, bool show) {
        if(state == StateEnum.Knockup
           || state == StateEnum.Stunned
           || state == StateEnum.Silenced) {
            if(mainLogicUnit.IsPlayerSelf() && show) {
                playWindow.SetAllSkillForbidState();
            }
        }

        hpWindow.SetStateInfo(mainLogicUnit, state, show);
    }
    
    public void UpdateJui(JumpUpdateInfo jui) {
        if(jui != null) {
            float scaleRate = 1.0f * ClientConfig.ScreenStandardHeight / Screen.height;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1, 0));
            jui.pos = screenPos * scaleRate;
        }
        hpWindow.SetJumpUpdateInfo(jui);
    }


    private void OnDestroy()
    {
        mainLogicUnit.OnHPChange -= UpdateHP;
    }

    public virtual void OnDeath(MainLogicUnit unit)
    {
        
    }

    protected override void Update()
    {
        if (mainLogicUnit.isDirChanged && !mainLogicUnit.IsSkillSpelling())
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
        if (aniName == "atk")
        {
            // 随机普攻动画
            aniName += Random.Range(1, 3);
        }
        
        if (aniName.Contains("walk"))
        {
            // 如果有buff让速度提升，就按速率播放：
            float moveRate = mainLogicUnit.LogicMoveSpeed.RawFloat / aniMoveSpeedBase;
            ani[aniName].speed = moveRate;
            ani.CrossFade(aniName,fade / moveRate);
        }
        else if (aniName.Contains("atk"))
        {
            if (ani.IsPlaying(aniName))
            {
                ani.Stop();
            }
            float attackRate = mainLogicUnit.AttackSpeedRateCurrent.RawFloat / aniAttackSpeedBase;
            ani[aniName].speed = attackRate;
            ani.CrossFade(aniName,fade / attackRate);
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

    /// <summary>
    /// 
    /// </summary>
    public void UpdateSKillRotation(PEVector3 skillRotation)
    {
        viewTargetDir = skillRotation.ConvertViewVector3();
    }
    
    public void SetBuffFollower(BuffView buffView) {
        buffView.transform.SetParent(this.transform);
        buffView.transform.localPosition = Vector3.zero;
        buffView.transform.localScale = Vector3.one;
    }
    
    public void SetImgInfo(int cdTime) {
        if(mainLogicUnit.IsPlayerSelf()) {
            playWindow.SetImgInfo(cdTime);
        }
    }
    
    public void RemoveUIItemInfo() {
        hpWindow.RemoveHPItemInfo(mainLogicUnit);
        playWindow.RemoveMapIconItemInfo(mainLogicUnit);
    }
}
