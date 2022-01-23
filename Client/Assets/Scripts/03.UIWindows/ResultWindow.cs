using HOKProtocol;
using UnityEngine.UI;

public class ResultWindow : WindowBase
{
    public Image imgResult;
    public Image imgPrg;// 5s倒计时
    public Text txtTime;// 5s倒计时
 
    protected override void InitWindow() {
        base.InitWindow();
    }
    
    public void SetBattleResult(bool isSucc) {
        if(isSucc) {
            audioSvc.PlayUIAudio("victory");
            SetSprite(imgResult, "ResImages/ResultWnd/win");
        }
        else {
            audioSvc.PlayUIAudio("defeat");
            SetSprite(imgResult, "ResImages/ResultWnd/lose");
        }
        imgResult.SetNativeSize();

        CreateMonoTimer(
            (loopCount) => {
                SetText(txtTime, 5 - loopCount);
            },
            1000,
            5,
            (isDelay, loopPrg, allPrg) => {
                imgPrg.fillAmount = allPrg;
            },
            ClickContinueBtn,
            1000);
    }

    public void ClickContinueBtn() {
        if(gameObject.activeSelf) {
            GameMsg msg = new GameMsg {
                cmd = CMD.ReqBattleEnd,
                reqBattleEnd = new ReqBattleEnd {
                    roomId = GameRoot.ActiveRoomId
                }
            };
            netSvc.SendMsg(msg);
        }
    }
}