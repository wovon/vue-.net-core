using System;
using System.Collections.Generic;
using System.Text;

namespace V.NetCore.App.Response
{
    /// <summary>
    /// table的返回数据
    /// </summary>
    public class TableData
    {
        /// <summary>
        /// 数据内容
        /// </summary>
        public dynamic data;
        /// <summary>
        /// 状态码
        /// </summary>
        public int status;

        public string message { get; set; }

        /// <summary>
        /// 操作状态码，200为正常
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 状态文本
        /// </summary>
        public string statusText;
        public dynamic items;
        public int total;
        public bool browser { get; set; }

        public TableData()
        {
            code = 200;
            statusText = "加载成功";
        }
    }
}
