public class BulletCfg {
    public BulletTypeEnum bulletType;
    public string bulletName;
    public string resPath;
    public float bulletSpeed;
    public float bulletSize;
    public float bulletHeight;
    public float bulletOffset;
    public int bulletDelay;//ms
    public bool canBlock;

    public TargetCfg impacter;
    public int bulletDuration;
}

public enum BulletTypeEnum {
    UIDirection,//ui指定方向
    UIPosition,//ui指定位置
    SkillTarget,//当前技能目标
    BuffSearch,
    //TODO
}