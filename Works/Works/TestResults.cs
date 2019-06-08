using System.Collections.Generic;
using System.Linq;

namespace Works
{
    public class TestResults
    {
        public string Id { get; set; }
        public string FamilyId { get; set; }
        public int Score { get; set; }

        public static List<TestResults> GetMaxResults(List<TestResults> famList)
        {
            List<TestResults> maxResultsList =
                famList
                       .GroupBy(r => r.FamilyId)
                       .Select(g => {
                           int max = g.Max(h => h.Score);
                           return new TestResults { Id = g.First(f => f.Score == max).Id, FamilyId = g.Key, Score = max };
                       })
                       .ToList();
            return maxResultsList;
        }
    }
}
