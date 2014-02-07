
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
            Dictionary<string, Experiment> experiments = new Dictionary<string, Experiment>();
            experiments.Add("Green", new Experiment(new Name("Green"), new DescriptiveText("GreenExperiment")));
            experiments.Add("Blue", new Experiment(new Name("Blue"), new DescriptiveText("BlueExperiment")));
            experiments.Add("Red", new Experiment(new Name("Red"), new DescriptiveText("RedExperiment")));

            Hypothesis colorHypothesis = new Hypothesis(
                    new Name("ColorTest"),
                    new DescriptiveText("One of theses colors is the best"),
                    selector,
                    experiments);
            
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
