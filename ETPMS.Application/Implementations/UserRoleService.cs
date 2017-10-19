using ETPMS.Application.Contracts;
using ETPMS.Application.DTOs;
using ETPMS.Application.Enums;
using ETPMS.Application.Models;
using ETPMS.Entity;
using ETPMS.Infrastructure.Components;
using ETPMS.Infrastructure.Extensions;
using ETPMS.Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;

namespace ETPMS.Application.Implementations
{
    [Component(LifeStyle.InstancePerLifetimeScope)]
    public sealed class UserRoleService : ETPMSBaseService<UM_USER_RELROLE>, IUserRoleService
    {
        private readonly IRepository<UM_ROLE> _roleRepository;
        private readonly IRepository<UM_USERINFO> _userRepository;

        public UserRoleService(IRepository<UM_USER_RELROLE> userRoleRepository,
            IRepository<UM_ROLE> roleRepository, IRepository<UM_USERINFO> userRepository) : base(userRoleRepository)
        {
            this._roleRepository = roleRepository;
            this._userRepository = userRepository;
        }

        public List<SimplifiedRoleInfo> GetRolesByUserCode(string userCode)
        {
            var roles = (from k in base.Repository.GetAll()
                         join s in this._userRepository.GetAll() on k.USER_ID equals s.ID
                         join p in this._roleRepository.GetAll() on k.ROLE_ID equals p.ID
                         where s.USER_CODE == userCode && !s.IS_DELETED
                         select new SimplifiedRoleInfo
                         {
                             RoleId = p.ID,
                             RoleName = p.ROLE_NAME,
                             RoleCode = p.ROLE_CODE
                         }).ToList();

            return roles;
        }

        public List<SimplifiedRoleInfo> GetRolesByUserId(int userId)
        {
            var roles = (from s in base.Repository.GetAll()
                         join p in this._roleRepository.GetAll() on s.ROLE_ID equals p.ID
                         where s.USER_ID == userId && !p.IS_DELETED
                         select new SimplifiedRoleInfo
                         {
                             RoleId = p.ID,
                             RoleCode = p.ROLE_CODE,
                             RoleName = p.ROLE_NAME
                         }).ToList();

            return roles;
        }

        public OperationResult UpdateUserRole(int userId, List<UserRoleDto> userRoleDtos)
        {
            //ToDo:先删除后添加
            base.Repository.Delete(s=>s.USER_ID == userId);

            var userRoleEntities = userRoleDtos.MapToList<UserRoleDto, UM_USER_RELROLE>();
            base.Repository.Add(userRoleEntities);

            return new OperationResult
            {
                ResultType = OperationResultType.Succed,
                Message = $"更新用户权限成功~"
            };
        }
    }
}
