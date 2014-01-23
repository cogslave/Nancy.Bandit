
namespace Nancy.Bandit.Selection
{
    using System.Collections.Generic;

    public interface ISelectionAlgorithm
    {
        Experiment Choose(IList<Experiment> experiments);
    }
}