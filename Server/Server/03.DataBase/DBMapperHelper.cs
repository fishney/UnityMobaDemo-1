using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using proto.HOKProtocol;
using MySql.Data.MySqlClient;

namespace Server
{
	/// <summary>
	/// Mapper、Reflection相关共通方法抽出
	/// </summary>
	public static class DBMapperHelper
	{
		/// <summary>
		/// Query操作共通抽出
		/// </summary>
		public static List<T> Query<T>(this MySqlCommand cmd) where T : new()
		{
			MySqlDataReader reader = cmd.ExecuteReader();

			var backupList = new List<T>();
			// 检测是否有数据
			while (reader.Read())
			{
				var dto = new T();

				foreach (var propertyInfo in dto.GetType().GetProperties())
				{
					// 数据Mapper
					if (propertyInfo.PropertyType == typeof(int))
					{
						propertyInfo.SetValue(dto, reader.GetInt32(propertyInfo.Name.ToLower()));
					}
					else if (propertyInfo.PropertyType == typeof(string))
					{
						propertyInfo.SetValue(dto, reader.GetString(propertyInfo.Name.ToLower()));

					}
					else if (propertyInfo.PropertyType == typeof(float))
					{
						propertyInfo.SetValue(dto, reader.GetFloat(propertyInfo.Name.ToLower()));
					}
					else if (propertyInfo.PropertyType == typeof(decimal))
					{
						propertyInfo.SetValue(dto, reader.GetDecimal(propertyInfo.Name.ToLower()));
					}
					else if (propertyInfo.PropertyType == typeof(long))
					{
						propertyInfo.SetValue(dto, reader.GetInt64(propertyInfo.Name.ToLower()));

					}

				}

				backupList.Add(dto);
			}

			// 最后要关闭！！！
			reader.Close();
			return backupList;
		}

		/// <summary>
		/// Query账号数据共通抽出
		/// </summary>
		public static List<PlayerData> QueryPlayerData(this MySqlCommand cmd)
		{
			MySqlDataReader reader = cmd.ExecuteReader();

			var backupList = new List<PlayerData>();
			// 检测是否有数据
			while (reader.Read())
			{
				var dto = new PlayerData();

				foreach (var propertyInfo in dto.GetType().GetProperties())
				{
					// 数据Mapper
					if (propertyInfo.PropertyType == typeof(int))
					{
						propertyInfo.SetValue(dto, reader.GetInt32(propertyInfo.Name.ToLower()));
					}
					else if (propertyInfo.PropertyType == typeof(string))
					{
						propertyInfo.SetValue(dto, reader.GetString(propertyInfo.Name.ToLower()));

					}
					else if (propertyInfo.PropertyType == typeof(float))
					{
						propertyInfo.SetValue(dto, reader.GetFloat(propertyInfo.Name.ToLower()));
					}
					else if (propertyInfo.PropertyType == typeof(decimal))
					{
						propertyInfo.SetValue(dto, reader.GetDecimal(propertyInfo.Name.ToLower()));
					}
					else if (propertyInfo.PropertyType == typeof(long))
					{
						propertyInfo.SetValue(dto, reader.GetInt64(propertyInfo.Name.ToLower()));

					}
				}

				// PlayerData特殊

				dto.heroSelectData = reader.GetString("heroSelectData").ToHeroSelectDataArr();
				dto.bagData = reader.GetString("bag").ToBagItemDataArr();
				//dto.taskArr = reader.GetString("taskArr").ToStringArr();

                backupList.Add(dto);
			}

			// 最后要关闭！！！
			reader.Close();
			return backupList;
		}


		/// <summary>
		/// 将数据全部Mapper到cmd的Parameter中
		/// </summary>
		public static void SetAllParameters<T>(this MySqlCommand cmd, T dto)
		{
			foreach (var propertyInfo in dto.GetType().GetProperties())
			{
				// 数据Mapper
				if (propertyInfo.GetValue(dto) != null)
				{
					cmd.Parameters.AddWithValue(propertyInfo.Name.ToLower(), propertyInfo.GetValue(dto));
				}
			}

		}

		/// <summary>
		/// 将数据全部Mapper到cmd的Parameter中
		/// </summary>
		public static void SetPlayerDataParas(this MySqlCommand cmd, PlayerData dto)
		{
            cmd.Parameters.AddWithValue("name", dto.name);
            cmd.Parameters.AddWithValue("level", dto.level);
            cmd.Parameters.AddWithValue("exp", dto.exp);
            cmd.Parameters.AddWithValue("coin", dto.coin);
            cmd.Parameters.AddWithValue("diamond", dto.diamond);
            cmd.Parameters.AddWithValue("ticket", dto.ticket);
            cmd.Parameters.AddWithValue("id", dto.id);


   //          foreach (var propertyInfo in dto.GetType().GetProperties())
			// {
			// 	// 数据Mapper
			// 	if (propertyInfo.GetValue(dto) != null)
			// 	{
			// 		cmd.Parameters.AddWithValue(propertyInfo.Name.ToLower(), propertyInfo.GetValue(dto));
			// 	}
			// }

			// PlayerData特殊
			cmd.Parameters.AddWithValue("heroSelectData", dto.heroSelectData.ToStringArr());
			cmd.Parameters.AddWithValue("bag", dto.bagData.ToStringArr());
        }


		/// <summary>
		/// 把string的数据库用结构转换为int[]，分隔符默认#
		/// </summary>
		public static int[] ToIntArr(this string stringArr, char para = '#')
		{
			var stringList = stringArr.Split(para);
			int[] intList = new int[stringList.Length];
			for (int i = 0; i < stringList.Length; i++)
			{
				intList[i] = Int32.Parse(stringList[i]);
			}
			return intList;
		}

		/// <summary>
		/// 把int[]转换为string的数据库用结构，分隔符默认#
		/// </summary>
		public static string ToStringArr(this int[] intList, char para = '#')
		{
			StringBuilder strongArr = new StringBuilder();
			for (int i = 0; i < intList.Length; i++)
			{
				strongArr.Append(intList[i]);

				if (i < intList.Length - 1)
				{
					strongArr.Append(para);
				}
			}

			return strongArr.ToString();
		}

		/// <summary>
		/// 把List<HeroSelectData>转换为string的数据库用结构，分隔符默认#
		/// </summary>
		public static string ToStringArr(this List<HeroSelectData> list, char para = '#')
        {
            StringBuilder strongArr = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                strongArr.Append(list[i].heroID);

                if (i < list.Count - 1)
                {
                    strongArr.Append(para);
                }
            }

            return strongArr.ToString();
        }

		/// <summary>
		/// 把string的数据库用结构转换为List<HeroSelectData>，分隔符默认#
		/// </summary>
		public static List<HeroSelectData> ToHeroSelectDataArr(this string stringArr, char para = '#')
        {
            var stringList = stringArr.Split(para);
            List<HeroSelectData> list = new List<HeroSelectData>();
            for (int i = 0; i < stringList.Length; i++)
            {
                list.Add(new HeroSelectData
                {
                    heroID = Int32.Parse(stringList[i]),
                });
            }
            return list;
        }

        /// <summary>
        /// 把List<BagItemData>转换为string的数据库用结构，分隔符默认#
        /// </summary>
        public static string ToStringArr(this List<BagItemData> list, char para = '#')
        {
            StringBuilder strongArr = new StringBuilder();
            // 感觉不需要排序
            //if (list.Count > 0)
            //{
            //    list = list.OrderBy(o=>o.itemId).ToList();
            //}

			for (int i = 0; i < list.Count; i++)
            {
                strongArr.Append(list[i].itemId);
                strongArr.Append(":");
                strongArr.Append(list[i].itemNum);

                if (i < list.Count - 1)
                {
                    strongArr.Append(para);
                }
            }

            return strongArr.ToString();
        }

		/// <summary>
		/// 把string的数据库用结构转换为List<BagItemData>，分隔符默认#
		/// </summary>
		public static List<BagItemData> ToBagItemDataArr(this string stringArr, char para = '#')
        {
            var stringList = stringArr.Split(para);
            List<BagItemData> list = new List<BagItemData>();

            foreach (string str in stringList)
            {
                var oneLine = str.Split(':');
                list.Add(new BagItemData
                {
                    itemId = Int32.Parse(oneLine[0]),
                    itemNum = Int32.Parse(oneLine[1]),
				});

			}

            return list;
        }

		/// <summary>
		/// 把string的数据库用结构转换为string[]，分隔符默认#
		/// </summary>
		public static string[] ToStringArr(this string data, char para = '#')
		{
			return data.Split(para);
		}

		/// <summary>
		/// 把string[]转换为string的数据库用结构，分隔符默认#
		/// </summary>
		public static string ToStringDB(this string[] stringList, char para = '#')
		{
			StringBuilder strongArr = new StringBuilder();
			for (int i = 0; i < stringList.Length; i++)
			{
				strongArr.Append(stringList[i]);

				if (i < stringList.Length - 1)
				{
					strongArr.Append(para);
				}
			}

			return strongArr.ToString();
		}
	}
}
