using PEMath;

public class Skill
{
    public int skillId;
    public SkillCfg skillCfg;
    public PEVector3 skillArgs;
    public MainLogicUnit lockTarget;
    public SKillState skillState = SKillState.None;

    public PEInt spellTime;// 施法时间
    public PEInt skillTime;// 技能总时间
    
    public MainLogicUnit owner;

    public Skill(int skillId,MainLogicUnit owner)
    {
        this.skillId = skillId;
        this.owner = owner;
        skillCfg = ResSvc.Instance().GetSkillConfigById(this.skillId);
        spellTime = skillCfg.spellTime;
        skillTime = skillCfg.skillTime;
        
        
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