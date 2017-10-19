using System;
using System.Linq;
using System.Collections.Generic;
using ETPMS.Application.Contracts;
using ETPMS.Application.DTOs;
using ETPMS.Entity;
using ETPMS.Infrastructure.Repository;
using ETPMS.Infrastructure.Components;
using ETPMS.Application.Models;

namespace ETPMS.Application.Implementations
{
    [Component(LifeStyle.InstancePerLifetimeScope)]
    public sealed class MenuService : ETPMSBaseService<UM_MENU>, IMenuService
    {
        public MenuService(IRepository<UM_MENU> repository) : base(repository)
        {
        }


    }
}
