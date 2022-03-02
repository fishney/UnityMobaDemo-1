using cfg;

public class BuffView : ViewUnit
{
    private Buff buff;
    public override void Init(LogicUnit buff)
    {
        base.Init(buff);
        this.buff = (Buff)buff;

        if (this.buff.cfg.staticPosType != StaticPosTypeEnum.None)
        {
            //固定位置buff
            transform.position = buff.LogicPos.ConvertViewVector3();
            transform.rotation = CalcRotation(buff.LogicDir.ConvertViewVector3());
        }
    }

    //用一个空函数覆盖位置与方向的更新，因为不需要同步、平滑插值等复杂的表现手法，直接跟随hero就完了
    protected override void Update() { }

    public void DestroyBuff() {
        Destroy(gameObject, 0.1f);
    }
}