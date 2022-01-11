public class BulletView : ViewUnit {
    public override void Init(LogicUnit logicUnit) {
        base.Init(logicUnit);
    }

    public void DestroyBullet() {
        Destroy(gameObject, 0.1f);
    }
}