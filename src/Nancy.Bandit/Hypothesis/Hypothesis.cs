
namespace Nancy.Bandit
{
    using Nancy.Bandit.Selection;
    using System.Collections.Generic;
    using System.Linq;

    public class Hypothesis
    {
        public Name Name { get; protected set; }
        public DescriptiveText Description { get; set; }
        private ISelectionAlgorithm selector;
        private IDictionary<string, Experiment> experiments;

        public Hypothesis(Name name, DescriptiveText description, ISelectionAlgorithm selector, IDictionary<string, Experiment> experiments)
        {
            Guard.NotNull(() => name, name);
            Guard.NotNull(() => description, description);
            Guard.NotNull(() => selector, selector);
            Guard.NotNull(() => experiments, experiments);

            this.Name = name;
            this.Description = description;
            this.selector = selector;
            this.experiments = experiments;
        }

        public KeyValuePair<string, string> Choose(string key)
        {
            Guard.NotNullOrEmpty(() => key, key);

            Experiment experiment;
            if(experiments.TryGetValue(key, out experiment))
            {
                return new KeyValuePair<string, string>(experiment.Name, experiment.Value);
            }

            return new KeyValuePair<string, string>(string.Empty, string.Empty);
        }

        public KeyValuePair<string, string> Choose()
        {
            Experiment experiment = selector.Choose(experiments.Values.ToList());
            experiment.Trial();

            return new KeyValuePair<string, string>(experiment.Name, experiment.Value);
        }

        public void Convert(string key, decimal weight)
        {
            Guard.NotNullOrEmpty(() => key, key);

            Experiment experiment;
            if (experiments.TryGetValue(key, out experiment))
            {
                experiment.Convert(weight);
            }
        }
    }
}