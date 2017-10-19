using ETPMS.Application.Enums;
using ETPMS.Infrastructure.Extensions;

namespace ETPMS.Application.Models
{
    public sealed class OperationResult : OperationResult<object>
    {
        public OperationResult() : base()
        {
        }
    }

    public class OperationResult<T>
    {
        public OperationResult()
        {
            this.ResultType = OperationResultType.Failed;
            //this.Message = this.ResultType.GetDescription();
            this.Data = default(T);
        }
        /// <summary>
        /// 操作结果
        /// </summary>
        public OperationResultType ResultType { get; set; }

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
