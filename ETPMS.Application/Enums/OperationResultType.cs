using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETPMS.Application.Enums
{
    public enum OperationResultType :byte
    {
        /// <summary>
        ///   操作成功
        /// </summary>
        [Description("操作成功。")]
        Succed = 1,

        /// <summary>
        ///   操作失败
        /// </summary>
        [Description("操作失败。")]
        Failed,

        /// <summary>
        ///   输入信息验证失败
        /// </summary>
        [Description("输入信息验证失败。")]
        ValidError,

        /// <summary>
        ///   指定参数的数据不存在
        /// </summary>
        [Description("指定参数的数据不存在。")]
        QueryNull,

        /// <summary>
        ///   操作取消或操作没引发任何变化
        /// </summary>
        [Description("操作没有引发任何变化，提交取消。")]
        NoChanged
    }
}
