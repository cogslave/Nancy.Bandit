
namespace Nancy.Bandit.Selection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EpsilonGreedy : ISelectionAlgorithm
    {
        private Random random = new Random();
        private double epsilon;

        public EpsilonGreedy()
        {
            this.epsilon = 0.1;
        }

        public EpsilonGreedy(double epsilon)
        {
            this.epsilon = epsilon;
        }

        public Experiment Choose(IList<Experiment> experiments)
        {
            Guard.NotNull(() => experiments, experiments);
            Guard.IsValid(() => experiments, experiments, x => x.Count > 0, "Experiments must contain at least one experiment.");

            return random.NextDouble() < epsilon ? Exploration(experiments) : Exploitation(experiments);
        }

        private Experiment Exploration(IList<Experiment> experiments)
        {
            return experiments.ElementAt(random.Next(0, experiments.Count));
        }

        private Experiment Exploitation(IList<Experiment> experiments)
        {
            return experiments.Aggregate((last, current) => Expectation(last) > Expectation(current) ? last : current);
        }

        private decimal Expectation(Experiment experiment)
        {
            return experiment.Conversions == 0 || experiment.Trials == 0 ? 0 : experiment.Conversions / experiment.Trials;
        }
    }
}