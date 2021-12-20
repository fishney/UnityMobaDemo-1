using UnityEngine;
using UnityEngine.UI;

public class ItemHPHero : ItemHPSoldier
{
    public Image imgState;// 状态文字图标
    public Text txtName;
    public Transform hpMarkRoot;// 血格底图
    public int markCount;// 每个血格的血量单位（每1000血画一个格）

    public override void InitItem(MainLogicUnit unit, Transform root, int hp)
    {
        base.InitItem(unit, root, hp);
        
        SetActive(imgState,false);
        txtName.text = unit.unitName;
        SetActive(txtName);

        SetHPMark(hp);
    }

    /// <summary>
    /// 血格计算显示
    /// </summary>
    void SetHPMark(int hp)
    {
        int count = hp / markCount;
        int childCount = hpMarkRoot.childCount;
        for (int i = 0; i < childCount; i++)
        {
            if (i < count)
            {
                SetActive(hpMarkRoot.GetChild(i));
            }
            else
            {
                SetActive(hpMarkRoot.GetChild(i),false);
            }
        }
    }

    /// <summary>
    /// 设置状态栏的文字(玩家名or状态文字)
    /// </summary>
    /// <param name="state"></param>
    /// <param name="show">是否需要额外状态显示（如果需要就只显示状态文字，如果不需要就只显示名字）</param>
    public override void SetStateInfo(StateEnum state, bool show)
    {
        base.SetStateInfo(state, show);

        if (!show)
        {
            SetActive(txtName);
            SetActive(imgState,false);
        }
        else
        {
            switch(state) {
                case StateEnum.Silenced:
                    SetSprite(imgState, "ResImages/PlayWnd/silenceState");
                    break;
                case StateEnum.Knockup:
                    SetSprite(imgState, "ResImages/PlayWnd/knockState");
                    break;
                case StateEnum.Stunned:
                    SetSprite(imgState, "ResImages/PlayWnd/stunState");
                    break;
                //TODO
                case StateEnum.Invincible:
                case StateEnum.Restricted:
                case StateEnum.None:
                default:
                    break;
            }

            SetActive(txtName, false);
            SetActive(imgState);
            imgState.SetNativeSize();
        }
    }
}