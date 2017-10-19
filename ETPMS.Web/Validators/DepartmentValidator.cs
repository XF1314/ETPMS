using ETPMS.Application.DTOs;
using FluentValidation;

namespace ETPMS.Web.Validators
{
    public sealed class DepartmentValidator : AbstractValidator<DepartmentDto>
    {
        public DepartmentValidator()
        {
            RuleFor(x => x.FATHER_DEPARTMENT_ID).GreaterThanOrEqualTo(0).WithMessage("请明确上级部门~");
            RuleFor(x => x.DEPARTMENT_LEADER_ID).GreaterThan(0).WithMessage("请明确部门领导~");

            RuleFor(x => x.DEPARTMENT_CODE)
                .NotNull()
                .NotEmpty().WithMessage("部门编码不应为空~")
                .Length(1, 32).WithMessage("部门编码长度应介于1-32字符之间~");

            RuleFor(x => x.DEPARTMENT_NAME)
                .NotNull()
                .NotEmpty().WithMessage("部门名称不应为空~")
                .Length(1, 128).WithMessage("部门名称长度应介于1-128字符之间~");

            RuleFor(x => x.DEAPRTMENT_SHORT_NAME)
                .Length(1, 32).WithMessage("部门简称长度应介于1-32字符之间~");

            RuleFor(x => x.DEPARTEMNT_DESCRIPTION)
                .Length(1, 256).WithMessage("描述信息长度应介于1-256字符之间~");     
        }
    }
}