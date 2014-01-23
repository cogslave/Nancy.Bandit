
namespace Nancy.Bandit
{
    using Nancy.Bandit.Repository;
    using Nancy.Bandit.Session;
    using System;
    using System.Collections.Generic;

    public class Bandido : IBandit
    {
        private int syncCounter;
        private int syncThreshold;
        private IRepository repository;
        public ISession Session { get; set; }

        public Bandido(IRepository repository)
        {
            Random random = new Random();
            this.syncCounter = 0;
            this.syncThreshold = 25 + random.Next(0, 25);
            this.repository = repository;
        }

        private void Synchronize()
        {
            this.syncCounter++;
            if(this.syncCounter > this.syncThreshold)
            {
                this.syncCounter = 0;
                this.repository.Synchronize();
            }
        }

        public string Choose(string key)
        {
            Hypothesis hypothesis = this.repository.SelectHypothesis(key);
            if (hypothesis != null)
            {
                KeyValuePair<string, string> experiment = new KeyValuePair<string, string>();
                string activeExperiment = this.Session.GetActiveExperiment(key);
                if (!string.IsNullOrEmpty(activeExperiment))
                {
                    experiment = hypothesis.Choose(activeExperiment);
                }
                else
                {
                    experiment = hypothesis.Choose();
                }

                this.Session.SetActiveExperiment(key, experiment.Key);
                return experiment.Value;
            }

            return string.Empty;
        }

        public void Convert(string key, decimal weight)
        {
            string experiment = this.Session.GetActiveExperiment(key);
            if (!string.IsNullOrEmpty(experiment))
            {
                Hypothesis hypothesis = repository.SelectHypothesis(key);
                if (hypothesis != null)
                {
                    hypothesis.Convert(experiment, weight);
                }
            }
        }

        public void Convert(string key)
        {
            Convert(key, 1);
        }
    }
}