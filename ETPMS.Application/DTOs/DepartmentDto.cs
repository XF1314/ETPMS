using System;

namespace ETPMS.Application.DTOs
{
    public sealed class DepartmentDto
    {
        public int? ID { get; set; }

        /// <summary>
        /// 上级部门ID
        /// </summary>
        public int FATHER_DEPARTMENT_ID { get; set; }


        /// <summary>
        /// 部门负责人ID
        /// </summary>
        public int DEPARTMENT_LEADER_ID { get; set; }

        /// <summary>
        /// 部门编码
        /// </summary>
        public string DEPARTMENT_CODE { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DEPARTMENT_NAME { get; set; }

        /// <summary>
        /// 部门简称
        /// </summary>
        public string DEAPRTMENT_SHORT_NAME { get; set; }

        /// <summary>
        /// 部门描述
        /// </summary>
        public string DEPARTEMNT_DESCRIPTION { get; set; }

        /// <summary>
        /// 部门索引
        /// </summary>
        public int DEPARTMENT_INDEX { get; set; }

        /// <summary>
        /// 最后操作人ID
        /// </summary>
        public int? OPERATOR_ID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CREATE_TIME { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime? UPDATE_TIME { get; set; }

    }
}
