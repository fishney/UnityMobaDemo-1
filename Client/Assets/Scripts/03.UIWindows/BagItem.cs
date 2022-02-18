using CodingK.UI;
using HOK.Expansion;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 格子类对象
/// UI显示道具
/// </summary>
public class BagItem : MonoBehaviour, IPoolObj<ItemInfo>
{
    public Text txtNum;
    public Image imgItem;
    public Button btnSelect;
    public ItemInfo info;
    public Image selectedFrame;

    public void Init()
    {
        gameObject.SetActive(false);
    }

    public void UnInit()
    {
       
    }

    public void Load(ItemInfo t)
    {
        gameObject.SetActive(true);
        info = t;
        //imgItem.SetSprite(t.imgPath);
        txtNum.text = t.num.ToString();
        
        if (info.isSelected) selectedFrame.gameObject.SetActive(true);
        btnSelect.onClick.AddListener(() => ClickItemButton(info));
    }

    public void UnLoad()
    {
        btnSelect.onClick.RemoveAllListeners();
        selectedFrame.gameObject.SetActive(false);
        info = null;
        txtNum.text = string.Empty;
        //imgItem.SetSprite(null);
        gameObject.SetActive(false);
    }

    public void SetSelectedFrame(bool isActive)
    {
        if (selectedFrame.gameObject.activeSelf != isActive)
        {
            selectedFrame.gameObject.SetActive(isActive);
        }
    }
    
    private void ClickItemButton(ItemInfo info)
    {
        GameRootResources.Instance().bagWindow.UpdateSelectedItemPanel(info);
    }
}