
namespace Nancy.Bandit
{
    public class Name : ConstrainedString
    {
        public Name(string name) : base(name, 1, 25)
        {

        }

        public static explicit operator Name(string value)
        {
            return new Name(value);
        }
    }
}