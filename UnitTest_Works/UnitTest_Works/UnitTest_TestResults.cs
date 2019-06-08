using System;
using System.Collections.Generic;
using Works;
using Xunit;

namespace UnitTest_Works
{
    public class UnitTest_TestResults
    {
        static List<TestResults> famList = new List<TestResults>
            {
                new TestResults { Id = "0", FamilyId = "10", Score = 14 },
                new TestResults { Id = "1", FamilyId = "11", Score = 15 },
                new TestResults { Id = "2", FamilyId = "12", Score = 10 },
                new TestResults { Id = "3", FamilyId = "13", Score = 11 },
                new TestResults { Id = "4", FamilyId = "13", Score = 9 },
            };

        [Fact]
        public void Query_ShouldReturn_ListOf_Length()
        {
            List<TestResults> famListResults = TestResults.GetMaxResults(famList);
            Assert.True(famListResults.Count == 4);
        }

        [Fact]
        public void Query_ShouldReturn_List_Elements()
        {
            List<TestResults> famListResults = TestResults.GetMaxResults(famList);
            Assert.True(famListResults[0].Id == "0");
            Assert.True(famListResults[0].FamilyId == "10");
            Assert.True(famListResults[0].Score == 14);
            Assert.True(famListResults[1].Id == "1");
            Assert.True(famListResults[1].FamilyId == "11");
            Assert.True(famListResults[1].Score == 15);
            Assert.True(famListResults[2].Id == "2");
            Assert.True(famListResults[2].FamilyId == "12");
            Assert.True(famListResults[2].Score == 10);
            Assert.True(famListResults[3].Id == "3");
            Assert.True(famListResults[3].FamilyId == "13");
            Assert.True(famListResults[3].Score == 11);
        }
    }
}
