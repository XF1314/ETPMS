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
    
    public partial class EP_DOCUMENT
    {
        public int ID { get; set; }
        public string DOCUMENT_NAME { get; set; }
        public byte DOCUMENT_TYPE_ID { get; set; }
        public byte EQUIPMENT_TYPE_ID { get; set; }
        public string URL { get; set; }
        public string REMARK { get; set; }
        public bool IS_DELETED { get; set; }
        public int OPERATOR_ID { get; set; }
        public System.DateTime CREATE_TIME { get; set; }
        public System.DateTime UPDATE_TIME { get; set; }
    }
}