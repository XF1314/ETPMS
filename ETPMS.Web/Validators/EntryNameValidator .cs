using ETPMS.Web.Properties;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace ETPMS.Web.Validators
{
    public sealed class EntryNameValidator : PropertyValidator, IRegularExpressionValidator
    {
        private readonly Regex _regex;
        public string Expression { get; } = @"^[a-zA-Z][\w-_]{1,19}$";

        //"'{PropertyName}'格式不正确，只能包含英文字母，数字，减号，下划线，并且以英文字母开头，且小长度为2，最大长度为20字符~";
        public static string EntryNameError { get; } = FluentValidationResources.G_Msg_SholdBeEntry;

        public EntryNameValidator()
            : base(() => EntryNameError)
        {
            _regex = new Regex(Expression, RegexOptions.IgnoreCase);
        }


        protected override bool IsValid(PropertyValidatorContext context)
        {
            return context.PropertyValue == null || _regex.IsMatch((string)context.PropertyValue);
        }
    }
}