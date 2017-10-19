using ETPMS.Web.Enums;

namespace ETPMS.Web.Models
{
    public sealed class ResponseModel:ResponseModel<object>
    {
        public ResponseModel() :base()
        {
        }
    }

    public class ResponseModel<T>
    {
        public ResponseModel()
        {
            this.ResultType = ResponseResultType.Failed;
            this.Message = string.Empty;
            this.Data = default(T);
        }
        /// <summary>
        /// 请求结果类型
        /// </summary>
        public ResponseResultType ResultType { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }
    }


}