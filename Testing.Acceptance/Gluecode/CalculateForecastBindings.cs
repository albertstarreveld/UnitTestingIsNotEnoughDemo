using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Testing.Acceptance.Builders;
using Testing.TheCodez;
using Testing.TheCodez.Calculators;
using Testing.TheCodez.Model;

namespace Testing.Acceptance.Gluecode
{
    [Binding]
    [Scope(Feature = "CalculatingForecast")]
    public class CalculateForecastBindings
    {
        private Fixture _fixture = new Fixture();
        private IList<Sprint> _sprints = new List<Sprint>();
        private Forecast _forecast;

        [Given("a history of sprints")]
        public void CreateSprint()
        {
            _sprints.Add(_fixture.Create<Sprint>());
        }

        [Given("production issues have occured in the past")]
        public void SprintsContainBugs()
        {
            foreach (var sprint in _sprints)
            {
                sprint.Bugs = _fixture.Create<int>();
            }
        }

        [When("we make a sprint forecast")]
        public void When()
        {
            var serviceProvider = new ServiceProviderBuilder()
                .Build();
             
            var sut = (ForecastService)serviceProvider.GetService(typeof(ForecastService));

            _forecast = sut.PredictTheFuture();
        }

        [Then("commit less then we normally would to account for disaster to strike")]
        public void Then()
        {
            Assert.IsTrue(_forecast.Optimistic > _forecast.Pessimistic);
        }
    }
}
