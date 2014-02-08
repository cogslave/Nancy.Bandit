
namespace Nancy.Bandit
{
    public class DescriptiveText : ConstrainedString
    {
        protected readonly static int min = 1;
        protected readonly static int max = 256;

        public DescriptiveText(string name)
            : base(name, min, max)
        {

        }

        public static explicit operator DescriptiveText(string value)
        {
            return new DescriptiveText(value.Length <= max ? value : value.Substring(0, max));
        }
    }
}
