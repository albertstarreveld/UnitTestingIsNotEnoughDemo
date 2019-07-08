using System;
using Testing.TheCodez;
using Testing.TheCodez.Calculators;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            var forecastService = new ForecastService(
                new ForecastCalculator(new EfficiencyCalculator(), new EfficiencyBugsExcludedCalculator()),
                new SprintRepository());

            var forecast = forecastService.PredictTheFuture();
            
            Console.WriteLine($"We think we can do {forecast.Optimistic} story points. \n" +
                              $"But we expect bugs this sprint, so we're going for: {forecast.Pessimistic}. \n" +
                              $"Makes sense cuz {forecast.Pessimistic} <  {forecast.Optimistic}, right?");

            Console.ReadLine();
        }
    }
}
