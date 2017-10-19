using ETPMS.Application.DTOs;
using FluentValidation;

namespace ETPMS.Web.Validators
{
    public sealed class RoleValidator:AbstractValidator<RoleDto>
    {
        public RoleValidator()
        {
            RuleFor(x => x.ROLE_CODE)
                .NotNull()
                .NotEmpty().WithMessage("角色编码不应为空~")
                .Length(1, 16).WithMessage("角色编码长度应介于1-16字符之间~");

            RuleFor(x => x.ROLE_NAME)
                .NotNull()
                .NotEmpty().WithMessage("角色名称不应为空~")
                .Length(1, 32).WithMessage("角色名称长度应介于1-32字符之间~");

            RuleFor(x => x.ROLE_DESCRIPTION)
                .Length(1, 256).WithMessage("角色描述长度应介于1-256字符之间~");
        }
    }
}