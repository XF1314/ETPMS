using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETPMS.Application.Models
{
    public sealed class SimplifiedDepartmentInfo
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 上级部门Id
        /// </summary>
        public int FatherDepartmentId { get; set; }

        /// <summary>
        /// 部门编码
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 部门简称
        /// </summary>
        public string DepartmentShortName { get; set; }

    }
}
