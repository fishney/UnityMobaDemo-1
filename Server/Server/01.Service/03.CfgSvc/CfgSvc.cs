using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Bright.Serialization;
using cfg.Datas;

namespace Server
{
    public class CfgSvc : ServiceBase<CfgSvc>
    {
        private string cfgDataDirPath = string.Empty;
        private CfgSvc() { }

        public override void Init()
        {
            base.Init();
            cfgDataDirPath = FindPath("CfgDatas");
            LoadItemInfo();
        }

        #region ItemCfg

        private Dictionary<int, ItemCfg> itemCfgDic;

        public void LoadItemInfo()
        {
            itemCfgDic = new Dictionary<int, ItemCfg>();
            var tables = new cfg.Tables(LoadByteBuf);
            foreach (var cfg in tables.TbItemCfg.DataList)
            {
                itemCfgDic.Add(cfg.id, cfg);
            }
        }

        private ByteBuf LoadByteBuf(string file)
        {
            return new ByteBuf(File.ReadAllBytes($"{cfgDataDirPath}/ResCfg/{file}.bytes"));
        }
        public ItemCfg GetItemCfgById(int id)
        {
            if (itemCfgDic.TryGetValue(id, out var itemCfg))
            {
                return itemCfg;
            }

            return null;
        }

        #endregion



        private string FindPath(string dirName)
        {
            var tryPath = @"\" + dirName;
            var path = System.AppDomain.CurrentDomain.BaseDirectory + dirName;
            while (!Directory.Exists(path))
            {
                path = path.Substring(0,path.LastIndexOf(@"\"));
                path = path.Substring(0, path.LastIndexOf(@"\"));
                path = path + tryPath;
            }

            return path;
        }
    }
}
