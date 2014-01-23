
namespace Nancy.Bandit.Tests
{
    using Nancy.Bandit.Selection;
    using System.Collections.Generic;
    using Xunit;

    public class EpsilonGreedyTests
    {
        [Fact]
        public void EpsilonGreedy_OnAverage_ReturnsExploitedExperiment()
        {
            EpsilonGreedy eg = new EpsilonGreedy(0.1);

            List<Experiment> experiments = new List<Experiment>();

            experiments.Add(new Experiment()
            {
                Name = "Red",
                Value = "RedExperiment",
                Trials = 1,
                Conversions = 1
            });

            experiments.Add(new Experiment()
            {
                Name = "Green",
                Value = "GreenExperiment",
                Trials = 1,
                Conversions = 500
            });

            experiments.Add(new Experiment()
            {
                Name = "Blue",
                Value = "BlueExperiment",
                Trials = 1,
                Conversions = 1
            });

            Dictionary<string, int> counter = new Dictionary<string,int>();
            counter.Add("Red", 0);
            counter.Add("Green", 0);
            counter.Add("Blue", 0);

            for (int i = 0; i < 1000; i++)
            {
                counter[eg.Choose(experiments).Name]++;
            }

            Assert.True(counter["Green"] > 850, string.Format("{0}, {1}, {2}", counter["Red"], counter["Green"], counter["Blue"]));
        }
    }
}
