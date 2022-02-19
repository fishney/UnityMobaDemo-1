using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using proto.HOKProtocol;
using MySql.Data.MySqlClient;

namespace Server
{
	public class DBMgr : Singleton<DBMgr>
	{
		private DBMgr() { }

		/// <summary>
		/// 连接服务
		/// </summary>
		private MySqlConnection conn;

		public void Init()
		{
			conn = new MySqlConnection("server=localhost;User Id = root;password=;Database=hok;Charset = utf8");
			conn.Open();
			//QueryPlayerData("test1","test");
			this.Log("DBMgr Init Done.");
		}

		#region 账号相关

		public PlayerData QueryPlayerData(string acct, string pass)
		{
			PlayerData playerData = null;

			MySqlCommand cmd = new MySqlCommand("select * from account where acct = @acct", conn);
			cmd.Parameters.AddWithValue("acct", acct);


			ReqLogin acctAndPass = cmd.Query<ReqLogin>()?.FirstOrDefault();



			if (acctAndPass != null && pass.Equals(acctAndPass.pass))
			{
				// 帐号密码正确，查询填充具体数据
				playerData = cmd.QueryPlayerData()?.FirstOrDefault();

			}
			else if (acctAndPass != null && !pass.Equals(acctAndPass.pass))
			{
				// 帐号存在、密码不正确           
			}
			else
			{
				// 帐号不存在，新建默认账号数据
				playerData = new PlayerData
				{
					id = 0,
					name = "test",// TODO name
					level = 1,
					exp = 0,
                    coin = 5000,
					diamond = 500,
                    ticket = 10,
					heroSelectData = new List<HeroSelectData>
                    {
						new HeroSelectData
                        {
							heroID = 101,
                        },
                        new HeroSelectData
                        {
                            heroID = 102,
                        },
					},
					bagData = GetNewBag(),
				};
				playerData.id = InsertNewAcctData(acct, pass, playerData);
			}

			return playerData;
		}

		private int InsertNewAcctData(string acct, string pass,PlayerData pd)
		{
			// TODO add column
			MySqlCommand cmd = new MySqlCommand(
				"insert into account set acct = @acct,pass = @pass,name = @name,level = @level,exp = @exp,coin = @coin,diamond = @diamond,ticket = @ticket,heroSelectData = @heroSelectData,bag = @bag", conn);
			cmd.Parameters.AddWithValue("acct", acct);
			cmd.Parameters.AddWithValue("pass", pass);
			cmd.Parameters.AddWithValue("name", pd.name);
			cmd.Parameters.AddWithValue("level", pd.level);
			cmd.Parameters.AddWithValue("exp", pd.exp);
            cmd.Parameters.AddWithValue("coin", pd.coin);
			cmd.Parameters.AddWithValue("diamond", pd.diamond);
			cmd.Parameters.AddWithValue("ticket", pd.ticket);
			cmd.Parameters.AddWithValue("heroSelectData", pd.heroSelectData.ToStringArr());
			cmd.Parameters.AddWithValue("bag", pd.bagData.ToStringArr());

            cmd.ExecuteNonQuery();
			return (int)cmd.LastInsertedId;
		}

		//public PlayerData QueryPlayerDataByName(string name)
		//{
		//	PlayerData playerData = null;

		//	MySqlCommand cmd = new MySqlCommand("select * from account where name = @name", conn);
		//	cmd.Parameters.AddWithValue("name", name);

		//	return cmd.QueryPlayerData()?.FirstOrDefault();
		//}

		public void UpdatePlayerData(PlayerData pd)
        {
            MySqlCommand cmd = new MySqlCommand(
				"update account set name = @name,level = @level,exp = @exp,coin = @coin,diamond = @diamond,ticket = @ticket,heroSelectData = @heroSelectData,bag = @bag where id = @id", conn);
			cmd.SetPlayerDataParas(pd);
			cmd.ExecuteNonQuery();
		}

		#endregion

        public List<BagItemData> GetNewBag()
        {
            var list = new List<BagItemData>();

			// TODO 测试用
            for (int i = 0; i < 80; i++)
            {
                list.Add(new BagItemData
                {
					itemId=i,
					itemNum= 6 - i/20,
                });
            }

            return list;
        }

	}
}
