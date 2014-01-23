
namespace Nancy.Bandit.Session
{
    public interface ISession
    {
        string GetActiveExperiment(string hypothesisName);
        void SetActiveExperiment(string hypothesisName, string experimentName);
    }
}