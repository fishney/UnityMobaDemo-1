using UnityEngine;
using UnityEngine.UI;

public class ItemHPSoldier : ItemHP
{
    public Image iconState;// 状态图标

    public override void InitItem(MainLogicUnit unit, Transform root, int hp)
    {
        base.InitItem(unit, root, hp);
        SetActive(iconState,false);

        if(isFriend) {
            SetSprite(imgPrg, @"ResImages/PlayWnd/selfteamhpfg");
        }
        else {
            SetSprite(imgPrg, @"ResImages/PlayWnd/enemyteamhpfg");
        }
    }

    public override void SetStateInfo(StateEnum state, bool show)
    {
        base.SetStateInfo(state, show);
        if (!show)
        {
            SetActive(iconState,false);
        }
        else
        {
            //血条下方图标显示
            switch(state) {
                case StateEnum.Silenced:
                    SetSprite(iconState, "ResImages/PlayWnd/silenceIcon");
                    break;
                case StateEnum.Knockup:
                    SetSprite(iconState, "ResImages/PlayWnd/stunIcon");
                    break;
                case StateEnum.Stunned:
                    SetSprite(iconState, "ResImages/PlayWnd/stunIcon");
                    break;
                //TODO
                case StateEnum.Invincible:
                case StateEnum.Restricted:
                case StateEnum.None:
                default:
                    break;
            }

            SetActive(iconState);
            iconState.SetNativeSize();
        }
        
    }
}