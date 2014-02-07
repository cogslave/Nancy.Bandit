
namespace Nancy.Bandit
{

    public class Experiment
    {
        public DescriptiveText Value;
        public Name Name { get; protected set; }
        public decimal Conversions { get; protected set; }
        public int Trials { get; protected set; }

        public Experiment(Name name, DescriptiveText value, decimal conversions, int trials)
        {
            Guard.NotNull(() => name, name);
            Guard.NotNull(() => value, value);

            this.Name = name;
            this.Value = value;
            this.Conversions = conversions;
            this.Trials = trials;
        }

        public Experiment(Name name, DescriptiveText value) : this(name, value, 0, 0)
        {
        }

        public void Convert(decimal value)
        {
            this.Conversions += value;
        }

        public void Trial()
        {
            this.Trials++;
        }
    }
}