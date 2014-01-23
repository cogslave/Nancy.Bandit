
namespace Nancy.Bandit.IntegrationTest.Infrastructure
{
    using Nancy.Bandit.Repository;
    using Nancy.Bandit.Selection;
    using System.Collections.Generic;

    public class MemoryRepository : IRepository
    {
        private IDictionary<string, Hypothesis> hypotheses;

        public MemoryRepository()
        {
            Hypothesis colorHypothesis = new Hypothesis()
            {
                Name = "OptimalBegging",
                Description = "There is an optimal amount of money to ask for.",
                Experiments = new Dictionary<string, Experiment>(),
                Selector = new EpsilonGreedy()
            };

            Experiment redExperiment = new Experiment()
            {
                Name = "Small",
                Value = "10"
            };

            Experiment greenExperiment = new Experiment()
            {
                Name = "Medium",
                Value = "25"
            };

            Experiment blueExperiment = new Experiment()
            {
                Name = "Large",
                Value = "150"
            };

            colorHypothesis.Experiments.Add(greenExperiment.Name, greenExperiment);
            colorHypothesis.Experiments.Add(blueExperiment.Name, blueExperiment);
            colorHypothesis.Experiments.Add(redExperiment.Name, redExperiment);

            this.hypotheses = new Dictionary<string, Hypothesis>();
            this.hypotheses.Add(colorHypothesis.Name, colorHypothesis);
        }

        public Hypothesis SelectHypothesis(string key)
        {
            Hypothesis hypothesis;

            if (this.hypotheses.TryGetValue(key, out hypothesis))
            {
                return hypothesis;
            }

            return null;
        }

        public void Synchronize()
        {
            
        }
    }
}