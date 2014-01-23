
namespace Nancy.Bandit.Repository
{  
    public interface IRepository
    {
        Hypothesis SelectHypothesis(string hypothesis);
        void Synchronize();
    }
}