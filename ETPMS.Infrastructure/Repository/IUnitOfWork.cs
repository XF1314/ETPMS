using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETPMS.Infrastructure.Repository
{
    public interface IUnitOfWork:IDisposable
    {
        /// <summary>
        /// 持久化数据(相当于Savechanges操作)
        /// </summary>
        void Commite();

        /// <summary>
        /// 设置Changetracker中所有实体为UnChanged状态
        /// </summary>
        void RollBackChanges();
    }
}
