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
    
    public partial class OD_ELECTRIC_DIVIDING_MACHINE
    {
        public int ID { get; set; }
        public int OD_ID { get; set; }
        public string SAMPLE_CODE { get; set; }
        public string COAL_SOURCE { get; set; }
        public int COAL_TYPE_ID { get; set; }
        public decimal ENVIRONMENT_TEMPERATURE { get; set; }
        public decimal ENVIRONMENT_HUMIDITY { get; set; }
        public decimal MAXIMUM_GRANULARITY { get; set; }
        public int PRECISION_SAMPLING_TYPE_ID { get; set; }
        public int BIAS_SAMPLING_TYPE_ID { get; set; }
        public int MT_PREPARATION_TYPE_ID { get; set; }
        public int MT_TEST_TYPE_ID { get; set; }
        public string SPLITTING_RATION { get; set; }
    
        public virtual OD_BASIC OD_BASIC { get; set; }
    }
}
