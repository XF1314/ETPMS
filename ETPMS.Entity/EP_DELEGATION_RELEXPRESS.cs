//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ETPMS.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class EP_DELEGATION_RELEXPRESS
    {
        public int ID { get; set; }
        public int DELEGATION_ID { get; set; }
        public byte MATERIAL_TYPE_ID { get; set; }
        public byte EXPRESS_COMPANY_ID { get; set; }
        public string EXPRESS_ORDER_CODE { get; set; }
        public System.DateTime DELIVERY_TIME { get; set; }
        public bool IS_DELETED { get; set; }
        public int OPERATOR_ID { get; set; }
        public System.DateTime CREATE_TIME { get; set; }
        public System.DateTime UPDATE_TIME { get; set; }
    
        public virtual EP_DELEGATION EP_DELEGATION { get; set; }
    }
}
