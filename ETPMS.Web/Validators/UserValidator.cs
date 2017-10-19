using ETPMS.Application.DTOs;
using ETPMS.Web.Extensions;
using ETPMS.Web.Properties;
using FluentValidation;

namespace ETPMS.Web.Validators
{
    public sealed class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.USER_NAME)
                .NotNull()
                .NotEmpty().WithMessage("用户姓名不应为空~")
                .Length(1, 16).WithMessage("用户姓名长度应介于1-16字符之间~");

            RuleFor(x => x.USER_CODE)
                .NotNull()
                .NotEmpty().WithMessage("用户编码不应为空~")
                .EntryName()
                .Length(1, 32).WithMessage("用户编码长度应介于1-32字符之间~");

            RuleFor(x => x.DEPARTMENT_ID)
                .NotEqual(0).WithMessage("请明确用户所属部门~");

            RuleFor(x => x.EMAIL)
                .NotNull()
                .NotEmpty().WithMessage("电子邮箱信息不应为空~")
                .Length(1, 32).WithMessage("电子邮箱长度应介于1-32字符之间~");
               // .EntryName();

            RuleFor(x => x.TELEPHONE)
                .NotNull()
                .NotEmpty().WithMessage("电话号码不应为空~")
                .Length(1, 16).WithMessage("电话号码长度应介于1-16字符之间~");

            RuleFor(x => x.SEX)
                .NotEqual((byte)0).WithMessage("请明确用户性别~");
        }
    }
}
