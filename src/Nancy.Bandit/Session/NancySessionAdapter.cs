
namespace Nancy.Bandit.Session
{

    public class NancySessionAdapter : ISession
    {
        private Nancy.Session.ISession session;
        //Todo: Think about URL args
        //Todo: THink about sticky vs none sticky sessions?

        public NancySessionAdapter(Nancy.Session.ISession session)
        {
            this.session = session;
        }

        public string GetActiveExperiment(string hypothesis)
        {
            var value = this.session[string.Format("bandit_{0}", hypothesis)];
            if (value != null)
            {
                return value.ToString();
            }

            return string.Empty;
        }

        public void SetActiveExperiment(string hypothesis, string experiment)
        {
            this.session[string.Format("bandit_{0}", hypothesis)] = experiment;
        }
    }
}
