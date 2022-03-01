using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Commen.Helper
{
    public class JsonHelper
    {
        public static T Deserialize<T>(string content) where T : class, new()
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        public static string Serialize<T>(T obj) where T : class, new()
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static void Serialize<T, S>(T obj, S stream) where S : Stream where T : class, new()
        {
            using (stream)
            {
                byte[] content = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
                stream.Write(content, 0, content.Length);
            }
        }

        /// <summary>
        /// 从字典中根据Key获取值
        /// </summary>
        /// <param name="param"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetParam(Dictionary<string, object> param, string key)
        {
            string result = "";
            if (param != null && param.ContainsKey(key))
            {
                if (param[key] != null && !param[key].ToString().Equals("null"))
                {
                    return param[key];
                }
                else
                {
                    return result;
                }
            }
            return result;
        }

        public static List<T> ToDataList<T>(DataTable dt) where T : new()
        {
            List<T> ts = new List<T>();

            // 获得此模型的类型
            Type type = typeof(T);
            //定义一个临时变量
            string tempName = string.Empty;
            //遍历DataTable中所有的数据行
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性
                PropertyInfo[] propertys = t.GetType().GetProperties();
                //遍历该对象的所有属性
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;//将属性名称赋值给临时变量
                                       //检查DataTable是否包含此列（列名==对象的属性名）
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter
                        if (!pi.CanWrite) continue;//该属性不可写，直接跳出
                                                   //取值
                        object value = dr[tempName];
                        //如果非空，则赋给对象的属性
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                //对象添加到泛型集合中
                ts.Add(t);
            }
            return ts;
        }
    }
}
