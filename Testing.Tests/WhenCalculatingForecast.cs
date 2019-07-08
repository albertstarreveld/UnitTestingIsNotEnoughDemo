using System.Linq;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Testing.Tests.Builders;
using Testing.TheCodez.Model;

namespace Testing.Tests
{
    [TestClass]
    public class WhenCalculatingForecast
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
                .WithTeamMemberEfficiency(averageWorkloadPerTeamMember)
                .Build();

            var actual = sut.CreateForecastForNextSprint(sprints);

            // Assert
            var expected = (double)(averageWorkloadPerTeamMember * sprints.Average(x => x.TeamMembers));
            Assert.AreEqual(expected, actual);
        }
    }
}