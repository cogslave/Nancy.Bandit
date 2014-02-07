
namespace Nancy.Bandit
{
    public class DescriptiveText : ConstrainedString
    {
        public DescriptiveText(string name)
            : base(name, 1, 256)
        {

        }

        public static explicit operator DescriptiveText(string value)
        {
            return new DescriptiveText(value);
        }
    }
}
