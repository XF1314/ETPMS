﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ETPMSDbContext : DbContext
    {
        public ETPMSDbContext()
            : base("name=ETPMSDbContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BD_CONFIG> BD_CONFIG { get; set; }
        public virtual DbSet<EP_CONTRACT> EP_CONTRACT { get; set; }
        public virtual DbSet<EP_CONTRACT_RELEQUIPMENT> EP_CONTRACT_RELEQUIPMENT { get; set; }
        public virtual DbSet<EP_DELEGATION> EP_DELEGATION { get; set; }
        public virtual DbSet<EP_DELEGATION_RELEXPRESS> EP_DELEGATION_RELEXPRESS { get; set; }
        public virtual DbSet<EP_DELEGATION_RELITEM> EP_DELEGATION_RELITEM { get; set; }
        public virtual DbSet<EP_DOCUMENT> EP_DOCUMENT { get; set; }
        public virtual DbSet<EP_EQUIPMENT_MENUFACTURER> EP_EQUIPMENT_MENUFACTURER { get; set; }
        public virtual DbSet<EP_EQUIPMENT_TASK> EP_EQUIPMENT_TASK { get; set; }
        public virtual DbSet<EP_EXPERIMENT_DOCUMENT> EP_EXPERIMENT_DOCUMENT { get; set; }
        public virtual DbSet<EP_POWER_PLANT> EP_POWER_PLANT { get; set; }
        public virtual DbSet<OD_BASIC> OD_BASIC { get; set; }
        public virtual DbSet<OD_BIAS> OD_BIAS { get; set; }
        public virtual DbSet<OD_CRUSHER_GRANULARITY> OD_CRUSHER_GRANULARITY { get; set; }
        public virtual DbSet<OD_CRUSHING_DIVIDING_MACHINE> OD_CRUSHING_DIVIDING_MACHINE { get; set; }
        public virtual DbSet<OD_CRUSHING_MACHINE> OD_CRUSHING_MACHINE { get; set; }
        public virtual DbSet<OD_DIEVIDER_GRANULARITY> OD_DIEVIDER_GRANULARITY { get; set; }
        public virtual DbSet<OD_ELECTRIC_DIVIDING_MACHINE> OD_ELECTRIC_DIVIDING_MACHINE { get; set; }
        public virtual DbSet<OD_INTEGRATED_PREPARATION_MACHINE> OD_INTEGRATED_PREPARATION_MACHINE { get; set; }
        public virtual DbSet<OD_MILLER_GRANULARITY> OD_MILLER_GRANULARITY { get; set; }
        public virtual DbSet<OD_MILLING_MACHINE> OD_MILLING_MACHINE { get; set; }
        public virtual DbSet<OD_MT_LOSSES> OD_MT_LOSSES { get; set; }
        public virtual DbSet<OD_PRECISION> OD_PRECISION { get; set; }
        public virtual DbSet<OD_RIFFLE_DEVIDING_MACHINE> OD_RIFFLE_DEVIDING_MACHINE { get; set; }
        public virtual DbSet<OD_SAMPLING_MACHINE> OD_SAMPLING_MACHINE { get; set; }
        public virtual DbSet<OD_SPLITTING_RATION> OD_SPLITTING_RATION { get; set; }
        public virtual DbSet<OD_TEST_INSTRUMENT> OD_TEST_INSTRUMENT { get; set; }
        public virtual DbSet<OD_UNIFIED_PREPARATION_MACHINE> OD_UNIFIED_PREPARATION_MACHINE { get; set; }
        public virtual DbSet<OD_WEIGHT_LOSSES> OD_WEIGHT_LOSSES { get; set; }
        public virtual DbSet<UM_DEPARTMENT> UM_DEPARTMENT { get; set; }
        public virtual DbSet<UM_MENU> UM_MENU { get; set; }
        public virtual DbSet<UM_ROLE> UM_ROLE { get; set; }
        public virtual DbSet<UM_ROLE_RELMENU> UM_ROLE_RELMENU { get; set; }
        public virtual DbSet<UM_USER_RELROLE> UM_USER_RELROLE { get; set; }
        public virtual DbSet<UM_USERINFO> UM_USERINFO { get; set; }
    }
}
