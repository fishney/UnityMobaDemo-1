using CodingK.UI;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 格子类对象
/// UI显示道具
/// </summary>
public class BagItem : MonoBehaviour, IPoolObj<ItemCfg>
{
    public Text txtNum;
    public string imgPath;
    public ItemCfg cfg;

    public void Load(ItemCfg t)
    {
        gameObject.SetActive(true);
        cfg = t;
        imgPath = t.path;
        txtNum.text = t.num.ToString();
        
    }

    public void UnLoad()
    {
        cfg = null;
        txtNum.text = string.Empty;
        imgPath = "";
        gameObject.SetActive(false);
    }
}