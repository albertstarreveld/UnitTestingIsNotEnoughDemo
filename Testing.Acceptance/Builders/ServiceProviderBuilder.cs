using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Testing.TheCodez;
using Testing.TheCodez.Calculators;

namespace Testing.Acceptance.Builders
{
    public class ServiceProviderBuilder
    {
        private readonly ServiceCollection _serviceCollection = new ServiceCollection();

        public ServiceProviderBuilder()
        {
            _serviceCollection.AddTransient<ISprintRepository, SprintRepository>();
            _serviceCollection.AddTransient<IForecastCalculator, ForecastCalculator>();
            _serviceCollection.AddTransient<IEfficiencyCalculator, EfficiencyCalculator>();
            _serviceCollection.AddTransient<IEfficiencyBugsExcludedCalculator, EfficiencyBugsExcludedCalculator>();
            _serviceCollection.AddTransient<ForecastService>();
        }
        
        public IServiceProvider Build()
        {
            return _serviceCollection.BuildServiceProvider();
        }
        
    }
}
