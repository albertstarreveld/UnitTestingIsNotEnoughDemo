using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Testing.Tests.Builders;
using Testing.TheCodez;
using Testing.TheCodez.Calculators;
using Testing.TheCodez.Model;

namespace Testing.Tests
{
    [TestClass]
    public class WhenCalculatingPessimisticForecast
    {
        private Fixture _fixture = new Fixture();

        [TestMethod]
        public void GivenWorkloadAndSprints_ThenAverageWorkloadMultipliedByAverageAmountOfTeammembers()
        {
            // Arrange
            var sprints = _fixture.CreateMany<Sprint>();
            var averageWorkloadPerTeamMember = _fixture.Create<int>();

            // Act
            var sut = new ForecastCalculatorBuilder()
                .WithTeamMemberEfficiencyExcludingBugs(averageWorkloadPerTeamMember)
                .Build();

            var actual = sut.CreatePessimisticForecast(sprints);

            // Assert
            var expected = (double)(averageWorkloadPerTeamMember * sprints.Average(x => x.TeamMembers));
            Assert.AreEqual(expected, actual);
        }
    }

    [TestClass]
    public class WhenPredictingTheFuture
    {
        private Fixture _fixture = new Fixture();

        [TestMethod]
        public void GivenEfficiency_ThenEfficiencyIsMappedToOptimisticField()
        {
            // Arrange
            var optimisticEfficiency = _fixture.Create<int>();
            
            // Act
            var sut = new ForecastServiceBuilder()
                .WithOptimisticEfficiency(optimisticEfficiency)
                .Build();

            var actual = sut.PredictTheFuture();

            // Assert
            Assert.AreEqual(optimisticEfficiency, actual.Optimistic);
        }

        [TestMethod]
        public void GivenEfficiencyBugsExcluded_ThenEfficiencyBugsExcludedIsMappedToPessimisticField()
        {
            // Arrange
            var pessimisticEfficiency = _fixture.Create<int>();

            // Act
            var sut = new ForecastServiceBuilder()
                .WithPessimisticEfficiency(pessimisticEfficiency)
                .Build();

            var actual = sut.PredictTheFuture();

            // Assert
            Assert.AreEqual(pessimisticEfficiency, actual.Pessimistic);
        }
    }

    public class ForecastServiceBuilder
    {
        private Fixture _fixture = new Fixture();

        private int _optimisticEfficiency;
        private int _pessimisticEfficiency;

        public ForecastServiceBuilder()
        {
            _optimisticEfficiency = _fixture.Create<int>();
            _pessimisticEfficiency = _fixture.Create<int>();
        }

        public ForecastServiceBuilder WithOptimisticEfficiency(int efficiency)
        {
            _optimisticEfficiency = efficiency;
            return this;
        }

        public ForecastServiceBuilder WithPessimisticEfficiency(int efficiency)
        {
            _pessimisticEfficiency = efficiency;
            return this;
        }

        public ForecastService Build()
        {
            var repositoryMock = new Mock<ISprintRepository>();
            repositoryMock.Setup(x => x.GetPreviousSprints()).Returns(_fixture.CreateMany<Sprint>());

            var forecastCalculatorMock = new Mock<IForecastCalculator>();
            forecastCalculatorMock
                .Setup(x => x.CreatePessimisticForecast(It.IsAny<IEnumerable<Sprint>>()))
                .Returns(_pessimisticEfficiency);

            forecastCalculatorMock
                .Setup(x => x.CreateForecastForNextSprint(It.IsAny<IEnumerable<Sprint>>()))
                .Returns(_optimisticEfficiency);

            return new ForecastService(forecastCalculatorMock.Object, repositoryMock.Object);
        }
    }
}