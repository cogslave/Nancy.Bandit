
namespace Nancy.Bandit
{
    using Nancy.Bandit.Selection;
    using System.Collections.Generic;
    using System.Linq;

    public class Hypothesis
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ISelectionAlgorithm Selector { get; set; }
        public IDictionary<string, Experiment> Experiments { get; set; }

        public KeyValuePair<string, string> Choose(string key)
        {
            Experiment experiment;
            if(Experiments.TryGetValue(key, out experiment))
            {
                return new KeyValuePair<string, string>(experiment.Name, experiment.Value);
            }

            return new KeyValuePair<string, string>(string.Empty, string.Empty);
        }

        public KeyValuePair<string, string> Choose()
        {
            Experiment experiment = Selector.Choose(Experiments.Values.ToList());
            experiment.Trials++;

            return new KeyValuePair<string, string>(experiment.Name, experiment.Value);
        }

        public void Convert(string key, decimal weight)
        {
            Experiment experiment;
            if (Experiments.TryGetValue(key, out experiment))
            {
                experiment.Conversions += weight;
            }
        }

    }
}