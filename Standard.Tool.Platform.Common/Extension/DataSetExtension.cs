using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Common.Extension
{
    public static class DataSetExtension
    {
        public static IEnumerable<T> DataSetToList<T>(this DataSet ds) where T : new()
        {
            if (ds != null)
            {
                var dt = ds.Tables[0];
                var mappingTable = ds.Tables[1];
                PropertyInfo[] propertys = new T().GetType().GetProperties();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    T entity = new T();
                    foreach (DataRow map_dr in mappingTable.Rows)
                    {
                        string desNameCol = map_dr["导入列名称"].ToString();
                        string mapNameCol = map_dr["映射字段"].ToString();
                        if (string.IsNullOrEmpty(mapNameCol))
                        {
                            continue;
                        }

                        if (!dt.Columns.Contains(desNameCol))
                        {
                            continue;
                        }
                        if (mapNameCol.Trim() == "CODEART")
                        {
                            if (string.IsNullOrEmpty(dt.Rows[i][desNameCol].ToString()))
                            {
                                continue;
                            }
                        }
                        if (dt.Rows[i][desNameCol] != null && !string.IsNullOrEmpty(dt.Rows[i][desNameCol].ToString()))
                        {
                            foreach (PropertyInfo pi in propertys)
                            {
                                if (pi.Name.Equals("No"))
                                {
                                    pi.SetValue(entity, i + 1, null);
                                }
                                if (pi.Name.Equals(mapNameCol))
                                {
                                    var type = pi.PropertyType;
                                    object value = dt.Rows[i][desNameCol];
                                    pi.SetValue(entity, ChanageType(value, pi.PropertyType), null);
                                }
                            }
                        }
                    }
                    yield return entity;
                }
            }
        }

        private static object ChanageType(object value, Type convertsionType)
        {
            //判断convertsionType类型是否为泛型，因为nullable是泛型类,
            if (convertsionType.IsGenericType &&
                //判断convertsionType是否为nullable泛型类
                convertsionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null || value.ToString().Length == 0)
                {
                    return null;
                }

                //如果convertsionType为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
                NullableConverter nullableConverter = new NullableConverter(convertsionType);
                //将convertsionType转换为nullable对的基础基元类型
                convertsionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, convertsionType);
        }
    }
}
