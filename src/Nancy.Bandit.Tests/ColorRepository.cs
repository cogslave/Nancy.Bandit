
namespace Nancy.Bandit.Tests
{
    using Nancy.Bandit.Repository;
    using Nancy.Bandit.Selection;
    using System.Collections.Generic;

    class ColorRepository : IRepository
    {
        private IDictionary<string, Hypothesis> hypotheses;

        public ColorRepository(ISelectionAlgorithm selector)
        {
            Hypothesis colorHypothesis = new Hypothesis()
            {
                Name = "ColorTest",
                Description = "One of theses colors is the best",
                Experiments = new Dictionary<string, Experiment>(),
                Selector = selector
            };

            Experiment redExperiment = new Experiment()
            {
                Name = "Red",
                Value = "RedExperiment",
                Conversions = 0,
                Trials = 0
            };

            Experiment greenExperiment = new Experiment()
            {
                Name = "Green",
                Value = "GreenExperiment",
                Conversions = 0,
                Trials = 0
            };

            Experiment blueExperiment = new Experiment()
            {
                Name = "Blue",
                Value = "BlueExperiment",
                Conversions = 0,
                Trials = 0
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
            
            if(this.hypotheses.TryGetValue(key, out hypothesis))
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
