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
    
    public partial class OD_DIEVIDER_GRANULARITY
    {
        public int ID { get; set; }
        public int OD_ID { get; set; }
        public string GRANULARITY_CODE { get; set; }
        public byte GRANULARITY_SAMPLE_TYPE_ID { get; set; }
        public decimal SAMPLE_WEIGHT { get; set; }
        public decimal GRANULARITY_WEIGHT_RANGE1 { get; set; }
        public decimal GRANULARITY_WEIGHT_RANGE2 { get; set; }
        public decimal GRANULARITY_RATION_RANGE1 { get; set; }
        public decimal GRANULARITY_RATION_RANGE2 { get; set; }
    
        public virtual OD_BASIC OD_BASIC { get; set; }
    }
}
