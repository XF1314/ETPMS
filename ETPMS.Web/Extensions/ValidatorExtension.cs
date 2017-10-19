using ETPMS.Web.Validators;
using FluentValidation;

namespace ETPMS.Web.Extensions
{
    public static class ValidatorExtension
    {
        public static IRuleBuilderOptions<T, string> EntryName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new EntryNameValidator());
        }
    }
}