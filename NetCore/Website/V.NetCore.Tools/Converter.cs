using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace V.NetCore.Tools
{
    public static  class Converter
    {

        /// <summary>
        /// object to Int(字符串 转换成 有符号、数值、整数)
        /// </summary>
        /// <param name="s">object</param>
        /// <param name="iDef">Default</param>
        /// <param name="isSNumber">是否去掉字符串中的字符</param>
        /// <returns>int</returns>
        public static int ToInt(this object s, int iDef = -1, bool isSNumber = false)
        {

            if (s != null)
            {
                if (s.GetType().Name == "Int32" || s.GetType().Name == "Int16")
                {
                    return (int)s;
                }
            }

            if (s == null)
                return iDef;
            int iResult = 0;
            //尝试转换
            var isTryParse = int.TryParse(s.ToString(), out iResult);
            if (isTryParse)
            {
                return iResult;
            }
            else
            {
                //去掉字符串中的字符
                if (isSNumber)
                {
                    string str = string.Empty;
                    //正则表达式
                    Regex reg = new Regex(@"[\d]+");
                    MatchCollection mstr = reg.Matches(s.ToString());
                    foreach (Match mc in mstr)
                    {
                        str += mc.Value;
                    }
                    //默认值修改成
                    return str.ToInt(iDef);
                }
                else
                {
                    return iDef;
                }
            }
        }

        /// <summary>
        /// object to DataTime(字符串 转换成 有符号、数值、整数)
        /// </summary>
        /// <param name="s">object</param>
        /// <param name="iDef">Default</param>
        /// <returns>string</returns>
        public static string ToDate(this object s, string format = "yyyy-MM-dd", string sDef = null)
        {
            if (sDef == null) { sDef = DateTime.Now.ToString(format); }
            if (s == null) return sDef;
            var dt = DateTime.Now;
            return DateTime.TryParse(s.ToString(), out dt) ? Convert.ToDateTime(s).ToString(format) : sDef;
        }

        /// <summary>
        /// object to DataTime(字符串 转换成 有符号、数值、整数)
        /// </summary>
        /// <param name="s">String</param>
        /// <param name="iDef">Default</param>
        /// <returns>Byte</returns>
        public static DateTime ToTime(this object s, string sDef = "")
        {
            var dt = DateTime.Now;
            return DateTime.TryParse(s.ToString(), out dt) ? Convert.ToDateTime(s) : DateTime.Now;
        }

        /// <summary>
        /// object to Double(字符串 转换成 有符号、数值、整数)
        /// </summary>
        /// <param name="s">object</param>
        /// <returns>double</returns>
        public static double ToDouble(this object s, double dDef = 0.00)
        {
            var _s = Convert.ToString(s);
            var d = 0.00;
            return double.TryParse(_s, out d) ? d : dDef;
        }

        /// <summary>
        /// object to Bool (1 为 true 也可字符串“true”)
        /// </summary>
        /// <param name="s">object</param>
        /// <param name="bDef"></param>
        /// <returns>bool</returns>
        public static bool ToBool(this object s, bool bDef = false)
        {
            if (s == null)
                return bDef;
            var _s = Convert.ToString(s);
            var b = false;
            var iResult = -1;
            if (int.TryParse(_s, out iResult))
                _s = iResult == 1 ? "true" : "false";
            return bool.TryParse(_s, out b) ? Convert.ToBoolean(_s) : bDef;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        //public static dynamic ToNewDynamic(this object o)
        //{
        //    if (o == null)
        //    {
        //        return null;
        //    }
        //    dynamic nd = new V.Frame.Tools.Extensions.NewDynamic();
        //    var od = (IDictionary<string, object>)o;
        //    foreach (var k in od.Keys)
        //    {
        //        nd.set(k, od[k]);
        //    }
        //    return nd;
        //}
        public static string ToJson(this Dictionary<string, string> array)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(array, Newtonsoft.Json.Formatting.Indented);
        }

        //public static List<T> JSONStringToList<T>(this string JsonStr)
        //{
        //    JavaScriptSerializer Serializer = new JavaScriptSerializer();
        //    List<T> objs = Serializer.Deserialize<List<T>>(JsonStr);
        //    return objs;
        //}

        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }

        ///// <summary>
        ///// dynamic获取动态属性
        ///// </summary>
        ///// <param name="target">dynamic</param>
        ///// <param name="name">属性名</param>
        ///// <returns></returns>
        //public static object GetProperty(this object target, string name)
        //{
        //    var site = System.Runtime.CompilerServices
        //        .CallSite<Func<System.Runtime.CompilerServices.CallSite, object, object>>
        //        .Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(0, name, target.GetType()
        //                , new[] { Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo.Create(0, null) }));
        //    return site.Target(site, target);
        //}
        /// <summary>
        /// dynamic获取动态属性
        /// </summary>
        /// <param name="target">dynamic</param>
        /// <param name="name">属性名</param>
        /// <returns></returns>
        public static object GetProperty(object target, string name)
        {
            var site = System.Runtime.CompilerServices
                .CallSite<Func<System.Runtime.CompilerServices.CallSite, object, object>>
                .Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(0, name, target.GetType()
                , new[] { Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo.Create(0, null) }));
            return site.Target(site, target);
        }

        /// <summary>   
        /// 对象集合转换Json   
        /// </summary>   
        /// <param name="array">集合对象</param>   
        /// <returns>Json字符串</returns>   
        public static string ToJson(this IEnumerable array, string sDef)
        {
            string jsonString = "[";
            foreach (object item in array)
            {
                jsonString += JsonHelper.ToJson(item) + ",";
            }

            if (jsonString.Length > 1)
            {
                //jsonString.Remove(jsonString.Length - 1, 1);
                jsonString.Remove(jsonString.Length - 1, jsonString.Length);
            }
            return jsonString + "]";
        }

        public static string ToJson(this IDictionary<string, object> dictionary)
        {
            return JsonConvert.SerializeObject(dictionary);
        }

        public static Dictionary<string, string> FromJsonToDictionary(this string json)
        {
            string[] keyValueArray = json.Replace("{", string.Empty).Replace("}", string.Empty).Replace("\"", string.Empty).Split(',');
            return keyValueArray.ToDictionary(item => item.Split(':')[0], item => item.Split(':')[1]);
        }

    }
}
