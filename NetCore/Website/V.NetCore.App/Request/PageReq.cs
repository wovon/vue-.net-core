
namespace V.NetCore.App.Request
{
    public class PageReq
    {
        public string Id { get; set; }

        /// <summary>
        /// 排序标识  如 ID，CreateTime
        /// </summary>
        public string PrimaryKey { get; set; }

        /// <summary>
        /// 排序方式  多排序可传：asc,update Time desc
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string KeyWorld { get; set; }

        public PageReq()
        {
            PrimaryKey = "CreateTime";
            Order = "desc";
            PageIndex = 1;
            PageSize = 10;
        }
    }
}
