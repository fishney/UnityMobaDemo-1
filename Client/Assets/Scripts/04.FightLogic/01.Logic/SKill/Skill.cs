using PEMath;

public class Skill
{
    public int skillId;
    public SkillCfg skillCfg;
    public PEVector3 skillArgs;
    public MainLogicUnit lockTarget;
    public SKillState skillState = SKillState.None;
    
    /// 施法时间
    public PEInt spellTime;
    /// 技能总时间
    public PEInt skillTime;
    
    public MainLogicUnit owner;

    public Skill(int skillId,MainLogicUnit owner)
    {
        this.skillId = skillId;
        skillCfg = ResSvc.Instance().GetSkillConfigById(this.skillId);
        spellTime = skillCfg.spellTime;
        skillTime = skillCfg.skillTime;
        
        if (skillCfg.isNormalAttack)
        {
            owner.InitAttackSpeedRate(1000/skillTime);
        }
        
        this.owner = owner;
    }

    public void ReleaseSkill(PEVector3 skillArgs)
    {
        
    }
}

public enum SKillState
{
    None,
    SpellStart,// 前摇
    SpellAfter,// 后摇
}