using System.Linq;
using Testing.TheCodez;
using Testing.TheCodez.Calculators;
using Testing.TheCodez.Model;

namespace Testing
{
    public class ForecastService
    {
        private readonly IForecastCalculator _forecastCalculator;
        private readonly ISprintRepository _sprintRepository;

        public ForecastService(IForecastCalculator forecastCalculator, 
            ISprintRepository sprintRepository)
        {
            _forecastCalculator = forecastCalculator;
            _sprintRepository = sprintRepository;
        }

        public Forecast PredictTheFuture()
        {
            var previousSprints = _sprintRepository
                .GetPreviousSprints()
                .ToArray();
            
            var optimisticForecast = _forecastCalculator
                .CreateForecastForNextSprint(previousSprints);

            var pessimisticForecast = _forecastCalculator
                .CreatePessimisticForecast(previousSprints);

            return new Forecast
            {
                Optimistic = optimisticForecast,
                Pessimistic = pessimisticForecast
            };
        }
    }
}