using ETPMS.Application.Enums;
using ETPMS.Application.Models;
using ETPMS.Infrastructure.Extensions;
using ETPMS.Web.Enums;
using ETPMS.Web.Models;

namespace ETPMS.Web.Extensions
{
    public static class OperationResultExtensions
    {
        /// <summary>
        /// 将业务操作结果映射为返回结果
        /// </summary>
        public static ResponseModel MapToResponseModel<T>(this OperationResult<T> operationResult)
        {
            var content = operationResult.Message ?? operationResult.ResultType.GetDescription();
            var type = operationResult.ResultType.MapToRsponseResultType();
            return new ResponseModel { Message = content, ResultType = type, Data = operationResult.Data };
        }

        /// <summary>
        /// 将业务操作结果映射为返回结果
        /// </summary>
        public static ResponseModel MapToResponseModel(this OperationResult operationResult)
        {
            string content = operationResult.Message ?? operationResult.ResultType.GetDescription();
            var type = operationResult.ResultType.MapToRsponseResultType();
            return new ResponseModel { Message = content, ResultType = type, Data = operationResult.Data };
        }

        /// <summary>
        /// 把业务结果类型<see cref="OperationResultType"/>映射为返回结果类型<see cref="ResponseResultType"/>
        /// </summary>
        public static ResponseResultType MapToRsponseResultType(this OperationResultType resultType)
        {
            switch (resultType)
            {
                case OperationResultType.Succed:
                    return ResponseResultType.Succed;
                case OperationResultType.NoChanged:
                    return ResponseResultType.Info;
                case OperationResultType.Failed:
                    return ResponseResultType.Failed;
                default:
                    return ResponseResultType.Error;
            }
        }

        /// <summary>
        /// 判断业务结果类型是否是Error结果
        /// </summary>
        public static bool IsError(this OperationResultType resultType)
        {
            return resultType == OperationResultType.QueryNull
                || resultType == OperationResultType.ValidError;
        }
    }
}