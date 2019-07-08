using System;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Testing.TheCodez;
using Testing.TheCodez.Calculators;
using Testing.TheCodez.Model;

namespace Testing.Tests
{
    [TestClass]
    public class WhenCalculatingEfficiencyPerTeamMember
    {
        private Fixture _fixture = new Fixture();

        [TestMethod]
        public void GivenTeamThatCompletedUserstories_ThenStoryPointsCompletedDevidedByTeammembers()
        {
            // Arrange
            var sprint = _fixture.Create<Sprint>();

            // Act
            var sut = new EfficiencyCalculator();
            var actual = sut.CalculateEfficiencyPerTeamMember(sprint);

            // Assert
            var expected = (double)sprint.StoryPointsCompleted / (double)sprint.TeamMembers;
            Assert.AreEqual(expected, actual);
        }
        
        //[TestMethod]
        //public void GivenWorkloadHigherThenFivePerTeamMember_ThenEfficiencyIsFive()
        //{
        //    // Arrange
        //    var sprint = _fixture.Build<Sprint>();
        //    sprint.StoryPointsCompleted =
        //        sprint.StoryPointsCompleted * sprint.TeamMembers * (5 + _fixture.Build<int>());

        //    // Act
        //    var sut = new EfficiencyCalculator();
        //    var actual = sut.CalculateEfficiencyPerTeamMember(sprint);

        //    // Assert
        //    Assert.AreEqual(5, actual);
        //}

        [TestMethod]
        public void GivenSprintIsNull_ThenEfficientyIsZero()
        {
            // Arrange
            Sprint sprint = null;

            // Act
            var sut = new EfficiencyCalculator();
            var actual = sut.CalculateEfficiencyPerTeamMember(sprint);

            // Assert
            Assert.AreEqual(0, actual);
        }
    }
}
