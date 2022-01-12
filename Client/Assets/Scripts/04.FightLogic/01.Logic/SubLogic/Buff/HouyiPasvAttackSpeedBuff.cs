
using HOKProtocol;
using PEMath;

public class HouyiPasvAttackSpeedBuffCfg : BuffCfg {
    public int overCount;// 叠加层数
    public int speedAddtion;// 加成百分比
    public int resetTime;// 重置周期
}

public class HouyiPasvAttackSpeedBuff : Buff {
    int currOverCount;//叠加层数
    int maxOverCount;//最大叠加层数
    PEInt speedAddtion;
    int resetTime;

    PEInt speedOffset;

    public HouyiPasvAttackSpeedBuff(MainLogicUnit source, MainLogicUnit owner, Skill skill, int buffID, object[] args = null)
        : base(source, owner, skill, buffID, args) {
    }

    public override void LogicInit() {
        base.LogicInit();

        currOverCount = 0;
        HouyiPasvAttackSpeedBuffCfg hpasbc = cfg as HouyiPasvAttackSpeedBuffCfg;
        maxOverCount = hpasbc.overCount;
        resetTime = hpasbc.resetTime;
        speedAddtion = hpasbc.speedAddtion;
        speedOffset = PEInt.zero;

        Skill[] skillArr = source.GetAllSkill();
        for(int i = 0; i < skillArr.Length; i++) {
            skillArr[i].SpellSuccessBp += OnSpellSkillSucc;
        }
    }

    void OnSpellSkillSucc(Skill skillReleased) {
        if(skillReleased.skillCfg.isNormalAttack) {
            timeCount = 0;
            if(currOverCount >= maxOverCount) {
                return;
            }
            else {
                ++currOverCount;
                isCounter = true;
                PEInt addition = owner.AttackSpeedRateBase * (speedAddtion / 100);
                speedOffset += addition;
                owner.ModifyAttackSpeed(addition);
            }
        }
        else {
            if(skillReleased.skillId != 1021) {
                ResetSpeed();
            }
        }
    }

    /// <summary>
    /// 用来计算是否到3秒
    /// </summary>
    int timeCount;
    /// <summary>
    /// 是否在Tick计算是否到3秒
    /// </summary>
    bool isCounter;
    protected override void Tick() {
        base.Tick();
        if(isCounter) {
            timeCount += Configs.ServerLogicFrameIntervelMs;
            if(timeCount >= resetTime) {
                ResetSpeed();
                timeCount = 0;
                isCounter = false;
            }
        }
    }

    void ResetSpeed() {
        owner.ModifyAttackSpeed(-speedOffset);
        speedOffset = PEInt.zero;
        currOverCount = 0;
    }
}
