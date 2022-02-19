//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;

namespace cfg.Datas
{
   
public partial class TbItemEffectCfg
{
    private readonly Dictionary<int, Datas.ItemEffectCfg> _dataMap;
    private readonly List<Datas.ItemEffectCfg> _dataList;
    
    public TbItemEffectCfg(ByteBuf _buf)
    {
        _dataMap = new Dictionary<int, Datas.ItemEffectCfg>();
        _dataList = new List<Datas.ItemEffectCfg>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            Datas.ItemEffectCfg _v;
            _v = Datas.ItemEffectCfg.DeserializeItemEffectCfg(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.id, _v);
        }
        PostInit();
    }

    public Dictionary<int, Datas.ItemEffectCfg> DataMap => _dataMap;
    public List<Datas.ItemEffectCfg> DataList => _dataList;

    public Datas.ItemEffectCfg GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public Datas.ItemEffectCfg Get(int key) => _dataMap[key];
    public Datas.ItemEffectCfg this[int key] => _dataMap[key];

    public void Resolve(Dictionary<string, object> _tables)
    {
        foreach(var v in _dataList)
        {
            v.Resolve(_tables);
        }
        PostResolve();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var v in _dataList)
        {
            v.TranslateText(translator);
        }
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}