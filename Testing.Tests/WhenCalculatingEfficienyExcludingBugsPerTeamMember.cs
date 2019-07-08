using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Testing.TheCodez;
using Testing.TheCodez.Calculators;
using Testing.TheCodez.Model;

namespace Testing.Tests
{
    [TestClass]
    public class WhenCalculatingEfficienyExcludingBugsPerTeamMember
    {
        private Fixture _fixture = new Fixture();

        [TestMethod]
        public void GivenTeamThatCompletedUserstories_ThenStoryPointsCompletedDevidedByTeammembers()
        {
            // Arrange
            var amountOfWorkItems = _fixture.Create<int>();
           
            var sprint = _fixture.Create<Sprint>();
            sprint.Bugs = amountOfWorkItems;
            sprint.UserStories = amountOfWorkItems; 

            // Act
            var sut = new EfficiencyBugsExcludedCalculator();
            var actual = sut.CalculateEfficiencyPerTeamMember(sprint);

            // Assert
            var expected = (double)sprint.StoryPointsCompleted / (double)sprint.TeamMembers / 2d;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenSprintIsNull_ThenEfficientyIsZero()
        {
            // Arrange
            Sprint sprint = null;

            // Act
            var sut = new EfficiencyBugsExcludedCalculator();
            var actual = sut.CalculateEfficiencyPerTeamMember(sprint);

            // Assert
            Assert.AreEqual(0, actual);
        }
    }
}
