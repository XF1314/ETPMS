using ETPMS.Application.DTOs;
using ETPMS.Web.Extensions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETPMS.Web.Validators
{
    public sealed class PasswordChangeValidator:AbstractValidator<PasswordChangeDto>
    {
        public PasswordChangeValidator()
        {
            RuleFor(x => x.OriginalPassword)
                .NotNull()
                .NotEmpty().WithMessage("原密码不能为空~");

            RuleFor(x => x.NewPassword)
                .NotNull()
                .NotEmpty().WithMessage("新密码不能为空~")
                .Length(6, int.MaxValue).WithMessage("新密码的长度不应小于6个符~");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword)
                .WithMessage("确认密码与新密码不一致~");

        }
    }
}