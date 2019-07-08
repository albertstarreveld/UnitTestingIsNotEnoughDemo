using System.Collections.Generic;
using System.Linq;
using Testing.TheCodez.Model;

namespace Testing.TheCodez.Calculators
{
    public interface IForecastCalculator
    {
        double CreateForecastForNextSprint(IEnumerable<Sprint> previousSprints);
        double CreatePessimisticForecast(IEnumerable<Sprint> previousSprints);
    }

    public class ForecastCalculator : IForecastCalculator
    {
        private readonly IEfficiencyCalculator _efficiencyCalculator;
        private readonly IEfficiencyBugsExcludedCalculator _efficiencyBugsExcludedCalculator;

        public ForecastCalculator(IEfficiencyCalculator efficiencyCalculator, 
                                  IEfficiencyBugsExcludedCalculator efficiencyBugsExcludedCalculator)
        {
            _efficiencyCalculator = efficiencyCalculator;
            _efficiencyBugsExcludedCalculator = efficiencyBugsExcludedCalculator;
        }

        public double CreateForecastForNextSprint(IEnumerable<Sprint> previousSprints)
        {
            var averageEfficiencyPerPerson = previousSprints
                .Select(_efficiencyCalculator.CalculateEfficiencyPerTeamMember)
                .Average();
            
            var averageCapacity = previousSprints
                .Average(x => x.TeamMembers);

            return averageEfficiencyPerPerson * averageCapacity;
        }

        public double CreatePessimisticForecast(IEnumerable<Sprint> previousSprints)
        {
            var averageCapacity = CalculateAverageCapacity(previousSprints);

            var averageEfficiencyPerPersonBugsExcluded = previousSprints
                .Select(_efficiencyBugsExcludedCalculator.CalculateEfficiencyPerTeamMember)
                .Average();

            return averageEfficiencyPerPersonBugsExcluded * averageCapacity;
        }

        private double CalculateAverageCapacity(IEnumerable<Sprint> previousSprints)
        {
            return previousSprints
                .Average(x => x.TeamMembers);
        }
    }
}