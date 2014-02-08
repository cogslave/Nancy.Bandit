
namespace Nancy.Bandit.Tests
{
    using Nancy.Bandit.Selection;
    using Nancy.Bandit.Session;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class BandidoTests
    {
        internal class RepeaterSession : ISession
        {
            private string item;

            public string GetActiveExperiment(string hypothesisName)
            {
                return item;
            }

            public void SetActiveExperiment(string hypothesisName, string experimentName)
            {
                item = experimentName;
            }
        }

        internal class GreenSelector : ISelectionAlgorithm
        {
            public Experiment Choose(IList<Experiment> experiments)
            {
                return experiments.First(x => x.Name == "Green");
            }
        }

        internal class MaxSelector : ISelectionAlgorithm
        {
            public Experiment Choose(IList<Experiment> experiments)
            {
                return experiments.Aggregate((last, current) => last.Conversions > current.Conversions ? last : current);
            }
        }

        [Fact]
        public void Bandido_WithInvalidHypothesisChoice_ReturnsEmptyString()
        {
            Bandido bandido = new Bandido(new ColorRepository(new EpsilonGreedy()));
            
            string choice = bandido.Choose("FakeName");

            Assert.Equal(choice, string.Empty);
        }

        [Fact]
        public void Bandido_WithInactiveExperiment_ReturnsSelectedExperiment()
        {
            Bandido bandido = new Bandido(new ColorRepository(new GreenSelector()));
            bandido.Session = new RepeaterSession();

            string choice = bandido.Choose("ColorTest");

            Assert.Equal("GreenExperiment", choice);
        }

        [Fact]
        public void Bandido_WithInactiveExperiment_ReturnsSelectedExperimentOnSecondRun()
        {
            Bandido bandido = new Bandido(new ColorRepository(new GreenSelector()));
            bandido.Session = new RepeaterSession();

            string choice = bandido.Choose("ColorTest");
            choice = bandido.Choose("ColorTest");

            Assert.Equal("GreenExperiment", choice);
        }

        [Fact]
        public void Bandido_WithActiveExperiment_ReturnsExperimentValue()
        {
            Bandido bandido = new Bandido(new ColorRepository(new EpsilonGreedy()));
            bandido.Session = new RepeaterSession();
            bandido.Session.SetActiveExperiment("ColorTest", "Red");

            string choice = bandido.Choose("ColorTest");

            Assert.Equal("RedExperiment", choice);
        }

        [Fact]
        public void Bandido_WithWeightedConversion_UpdatesExperiment()
        {
            Bandido bandido = new Bandido(new ColorRepository(new MaxSelector()));
            bandido.Session = new RepeaterSession();

            bandido.Session.SetActiveExperiment("ColorTest", "Blue");
            bandido.Convert("ColorTest", 500);
            string choice = bandido.Choose("ColorTest");

            Assert.Equal("BlueExperiment", choice);
        }

        [Fact]
        public void Bandido_WithConversion_UpdatesExperiment()
        {
            Bandido bandido = new Bandido(new ColorRepository(new MaxSelector()));
            bandido.Session = new RepeaterSession();

            bandido.Session.SetActiveExperiment("ColorTest", "Blue");
            bandido.Convert("ColorTest");
            string choice = bandido.Choose("ColorTest");

            Assert.Equal("BlueExperiment", choice);
        }

    }
}
