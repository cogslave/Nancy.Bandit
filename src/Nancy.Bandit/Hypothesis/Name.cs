
namespace Nancy.Bandit
{
    public class Name : ConstrainedString
    {
        protected readonly static int min = 1;
        protected readonly static int max = 25;

        public Name(string name) : base(name, min, max)
        {

        }

        public static explicit operator Name(string value)
        {
            return new Name(value.Length <= max ? value : value.Substring(0, max));
        }
    }
}