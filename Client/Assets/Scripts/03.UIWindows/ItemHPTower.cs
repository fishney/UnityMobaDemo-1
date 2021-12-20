using UnityEngine;

public class ItemHPTower : ItemHP
{
    public override void InitItem(MainLogicUnit unit, Transform root, int hp)
    {
        base.InitItem(unit, root, hp);
        if(isFriend) {
            SetSprite(imgPrg, "ResImages/PlayWnd/selftowerhpfg");
        }
        else {
            SetSprite(imgPrg, "ResImages/PlayWnd/enemytowerhpfg");
        }
    }
}