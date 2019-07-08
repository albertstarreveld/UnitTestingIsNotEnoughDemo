using Moq;
using Testing.TheCodez;
using Testing.TheCodez.Calculators;
using Testing.TheCodez.Model;

namespace Testing.Tests.Builders
{
    internal class ForecastCalculatorBuilder
    {
        private readonly Mock<IEfficiencyCalculator> _efficiencyCalculator;
        private readonly Mock<IEfficiencyBugsExcludedCalculator> _efficiencyBugsExcludedCalculator;

        public ForecastCalculatorBuilder()
        {
            _efficiencyCalculator = new Mock<IEfficiencyCalculator>();
            _efficiencyBugsExcludedCalculator = new Mock<IEfficiencyBugsExcludedCalculator>();
        }

        public ForecastCalculatorBuilder WithTeamMemberEfficiency(int efficiency)
        {
            _efficiencyCalculator
                .Setup(x => x.CalculateEfficiencyPerTeamMember(It.IsAny<Sprint>()))
                .Returns(efficiency);

            return this;
        }


        public ForecastCalculatorBuilder WithTeamMemberEfficiencyExcludingBugs(int efficiency)
        {
            _efficiencyBugsExcludedCalculator
                .Setup(x => x.CalculateEfficiencyPerTeamMember(It.IsAny<Sprint>()))
                .Returns(efficiency);

            return this;
        }


        public ForecastCalculatorBuilder WithPessimisticTeamMemberEfficiency(int efficiency)
        {
            _efficiencyBugsExcludedCalculator
                .Setup(x => x.CalculateEfficiencyPerTeamMember(It.IsAny<Sprint>()))
                .Returns(efficiency);

            return this;
        }

        public ForecastCalculator Build()
        {
            return new ForecastCalculator(_efficiencyCalculator.Object, _efficiencyBugsExcludedCalculator.Object);
        }
    }
}