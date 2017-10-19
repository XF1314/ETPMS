
namespace ETPMS.Infrastructure.Extensions
{
    public sealed class ValueTextPair<TValue, TText>
    {
        public ValueTextPair(TValue value, TText text)
        {
            this.Value = value;
            this.Text = text;
        }
        public TValue Value { get; set; }

        public TText Text { get; set; }
    }
}
