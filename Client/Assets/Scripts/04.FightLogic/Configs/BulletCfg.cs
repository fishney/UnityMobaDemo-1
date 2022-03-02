using System;

namespace Editor.xNode_Editor
{
    [Serializable]
    public class BulletCfg : ValStateView
    {
        public cfg.BulletTypeEnum bulletType;
        public string bulletName;
        public string resPath;
        public float bulletSpeed;
        public float bulletSize;
        public float bulletHeight;
        /// <summary>
        /// 发射子弹的时候，要往发射方向偏移一小段而不是从中心
        /// </summary>
        public float bulletOffset;
        public int bulletDelay;//ms
        public bool canBlock;

        public TargetCfg impacter;
        public int bulletDuration;
    }

    // public enum BulletTypeEnum {
    //     UIDirection,//ui指定方向
    //     UIPosition,//ui指定位置
    //     SkillTarget,//当前技能目标
    //     BuffSearch,
    //     //TODO
    // }
}
