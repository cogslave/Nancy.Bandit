
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
            Dictionary<string, Experiment> experiments = new Dictionary<string, Experiment>();
            experiments.Add("Small", new Experiment(new Name("Small"), new DescriptiveText("10")));
            experiments.Add("Medium", new Experiment(new Name("Medium"), new DescriptiveText("25")));
            experiments.Add("Large", new Experiment(new Name("Large"), new DescriptiveText("150")));

            Hypothesis moneyHypothesis = new Hypothesis(
                new Name("OptimalBegging"),
                new DescriptiveText("There is an optimal amount of money to ask for."),
                new EpsilonGreedy(),
                experiments
            );         

            this.hypotheses = new Dictionary<string, Hypothesis>();
            this.hypotheses.Add(moneyHypothesis.Name, moneyHypothesis);
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