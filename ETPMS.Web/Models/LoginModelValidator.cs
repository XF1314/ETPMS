using FluentValidation;
using FluentValidation.Results;

namespace ETPMS.Web.Models
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.PassWord).NotEmpty().WithMessage("密码不可为空");
            //RuleFor(x => x.PassWord).Must(IsPasswordLenthAvailable).WithMessage("密码长度不应小于6位");
            //Custom(x => x.PassWord.Length <= 6 ? new ValidationFailure("PassWord", "密码长度不应小于6位") : null);
            RuleFor(x => x.UserCode).NotEmpty().WithMessage("用户名或密码不可为空~").Length(1, 10).WithMessage("用户名长度超出限制~");
            RuleFor(x => x.PassWord).NotEmpty().WithLocalizedName(() => "密码").WithLocalizedMessage(() => "{PropertyName}不可为空~");
        }

        private bool IsPasswordLenthAvailable(string password)
        {
            if (password.Trim().Length <= 6)
            {
                return false;
            }

            return true;
        }
    }
}