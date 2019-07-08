using System.Collections.Generic;
using Testing.TheCodez.Model;

namespace Testing.TheCodez
{
    public interface ISprintRepository
    {
        IEnumerable<Sprint> GetPreviousSprints();
    }

    public class SprintRepository : ISprintRepository
    {
        public IEnumerable<Sprint> GetPreviousSprints()
        {
            return new[]
            {
                new Sprint
                {
                    Bugs = 2,
                    StoryPointsCompleted = 30,
                    TeamMembers = 4,
                    UserStories = 15
                },
                new Sprint
                {
                    Bugs = 3,
                    StoryPointsCompleted = 27,
                    TeamMembers = 3,
                    UserStories = 12
                },
                new Sprint
                {
                    Bugs = 0,
                    StoryPointsCompleted = 32,
                    TeamMembers = 4,
                    UserStories = 5
                },
                new Sprint
                {
                    Bugs = 15,
                    StoryPointsCompleted = 12,
                    TeamMembers = 4,
                    UserStories = 2
                },
            };
        }
    }
}