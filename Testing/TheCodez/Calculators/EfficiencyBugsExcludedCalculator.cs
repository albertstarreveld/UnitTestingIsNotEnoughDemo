using Testing.TheCodez.Model;

namespace Testing.TheCodez.Calculators
{
    public interface IEfficiencyBugsExcludedCalculator
    {
        double CalculateEfficiencyPerTeamMember(Sprint sprint);
    }

    public class EfficiencyBugsExcludedCalculator : IEfficiencyBugsExcludedCalculator
    {
        public double CalculateEfficiencyPerTeamMember(Sprint sprint)
        {
            if (sprint == null)
            {
                return 0;
            }

            var totalWorkItems = sprint.Bugs + sprint.UserStories;
            var percentageUserStories = (double) sprint.UserStories / (double) totalWorkItems;
            
            return ((double)sprint.StoryPointsCompleted / (double)sprint.TeamMembers) * percentageUserStories;
        }
    }
}