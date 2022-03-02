using System;
using cfg.Datas;

public class ItemInfo
{
    // from db
    public int id;
    private int _num;
    public int num
    {
        get
        {
            return _num;
        }
        set
        {
            _num = value;
            NumChanged?.Invoke(value);
        }
        
    }
    public Action<int> NumChanged;

    // from cfg
    public ItemCfg cfg;
    
    // for view
    public bool isSelected;
}