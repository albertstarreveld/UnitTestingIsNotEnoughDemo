using Testing.TheCodez.Model;

namespace Testing.TheCodez.Calculators
{
    public interface IEfficiencyCalculator
    {
        double CalculateEfficiencyPerTeamMember(Sprint sprint);
    }

    public class EfficiencyCalculator : IEfficiencyCalculator
    {
        public double CalculateEfficiencyPerTeamMember(Sprint sprint)
        {
            if (sprint == null)
            {
                return 0;
            }

            var efficiency = (double)sprint.StoryPointsCompleted / (double)sprint.TeamMembers;

            //const int maxEfficiency = 5;
            //if (efficiency > maxEfficiency)
            //{
            //    return maxEfficiency;
            //}

            return efficiency;
        }
    }
}
