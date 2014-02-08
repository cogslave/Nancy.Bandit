
namespace Nancy.Bandit
{
    public class ConstrainedString
    {
        private readonly string value;
        
        public ConstrainedString(string value, int min, int max)
        {
            Guard.NotNullOrEmpty(() => value, value);
            Guard.IsValid(() => min, min, x => min < max, "Min must be less than max.");
            Guard.IsValid(() => value, value, x => x.Length >= min && x.Length <= max, string.Format("Invalid length, must be between {0} and {1} characters long", min, max));

            this.value = value;
        }

        public override string ToString()
        {
            return value;
        }

        public static implicit operator string(ConstrainedString constrainedString)
        {
            return constrainedString.ToString();
        }
    }
}