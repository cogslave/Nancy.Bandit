
namespace Nancy.Bandit
{
    public interface IBandit
    {
        string Choose(string hypothesisName);
        void Convert(string hypothesisName);
        void Convert(string hypothesisName, decimal weight);
    }
}