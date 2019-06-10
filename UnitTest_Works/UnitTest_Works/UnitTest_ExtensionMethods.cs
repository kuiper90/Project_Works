using System;
using System.Collections.Generic;
using System.Linq;
using Works;
using Xunit;

namespace UnitTest_Works
{
    public class UnitTest_ExtensionMethods
    {

        static List<Product> productList = new List<Product>
            {
                new Product { Name = "prodOne",
                    Features = new List<Feature>
                    { new Feature {Id = 0}, new Feature {Id = 1},
                      new Feature {Id = 2}, new Feature {Id = 10},
                      new Feature {Id = 11}, new Feature {Id = 12} } },
                new Product { Name = "prodTwo",
                    Features = new List<Feature>
                    { new Feature {Id = 0}, new Feature {Id = 1},
                      new Feature {Id = 2}, new Feature {Id = 10},
                      new Feature {Id = 11}, new Feature {Id = 12},
                      new Feature {Id = 6} } },
                new Product { Name = "prodThree",
                    Features = new List<Feature>
                    { new Feature {Id = 10}, new Feature {Id = 2} } },
                new Product { Name = "prodFour",
                    Features = new List<Feature>
                    { new Feature {Id = 5}, new Feature {Id = 4} } }
            };

        static List<Feature> featureList = new List<Feature>
            {
                new Feature {Id = 0},
                new Feature {Id = 1},
                new Feature {Id = 2},
                new Feature {Id = 10},
                new Feature {Id = 11},
                new Feature {Id = 12}
            };

        [Fact]
        public void AllQ_ShouldReturn_True()
        {
            List<int> intList = new List<int> { 5, 6, 7, 8, 9, 10 };
            Assert.True(intList.AllQ(x => x > 3));
        }

        [Fact]
        public void AllQ_ShouldReturn_False()
        {
            List<int> intList = new List<int> { 5, 6, 7, 8, 9, 10 };
            Assert.False(intList.AllQ(x => x > 6));
        }

        [Fact]
        public void AnyQ_ShouldReturn_True()
        {
            List<int> intList = new List<int> { 5, 6, 7, 8, 9, 10 };
            Assert.True(intList.AnyQ(x => x % 3 == 0));
        }

        [Fact]
        public void AnyQ_ShouldReturn_False()
        {
            List<int> intList = new List<int> { 5, 6, 7, 8, 9, 10 };
            Assert.False(intList.AnyQ(x => x % 11 == 0));
        }

        [Fact]
        public void FirstQ_ShouldReturn_True()
        {
            List<int> intList = new List<int> { 5, 6, 7, 8, 9, 10 };
            Assert.True(intList.FirstQ(x => x % 3 == 0) == 6);
        }

        [Fact]
        public void FirstQ_ShouldReturn_False()
        {
            List<int> intList = new List<int> { 5, 6, 7, 8, 9, 10 };
            Assert.False(intList.FirstQ(x => x % 11 == 0) == 6);
        }

        [Fact]
        public void SelectQ_UsingBoolPredicate_ShouldReturn_ListOfBool()
        {
            List<int> intArray = new List<int> { 5, 6, 7, 8, 9, 10 };
            List<bool> rez =
                intArray
                .SelectQ(x => x % 3 == 0)
                .ToList();
            Assert.True(rez[0] == false);
            Assert.True(rez[1] == true);
            Assert.True(rez[2] == false);
            Assert.True(rez[3] == false);
            Assert.True(rez[4] == true);
            Assert.True(rez[5] == false);
        }

        [Fact]
        public void SelectQ_UsingIncrementPredicate_ShouldReturn_ArrayOfInt()
        {
            List<int> intList = new List<int> { 5, 6, 7, 8, 9, 10 };
            int[] rez = new int[6];
            rez = 
                intList
                    .SelectQ(x => x + 2)
                    .ToArray();
            Assert.True(rez[0] == 7);
            Assert.True(rez[1] == 8);
            Assert.True(rez[2] == 9);
            Assert.True(rez[3] == 10);
            Assert.True(rez[4] == 11);
            Assert.True(rez[5] == 12);
        }

        [Fact]
        public void SelectManyQ_UsingArrayOfArray_ShouldReturn_Array()
        {
            int[][] intArray = new int[][] { new int[] {  5,  6,  7 },
                                             new int[] { 15, 16, 17 },
                                             new int[] { 25, 26, 27 } };
            int[] rez =
                intArray
                .SelectManyQ(x => x)
                .ToArray();
            Assert.True(rez[0] == 5);
            Assert.True(rez[1] == 6);
            Assert.True(rez[2] == 7);
            Assert.True(rez[3] == 15);
            Assert.True(rez[4] == 16);
            Assert.True(rez[5] == 17);
            Assert.True(rez[6] == 25);
            Assert.True(rez[7] == 26);
            Assert.True(rez[8] == 27);
        }

        [Fact]
        public void SelectManyQ_UsingListOfArrays_ShouldReturn_ArrayOfInt()
        {
            int[][] intArray = new int[][] { new int[] {  5, 15,  5,},
                                             new int[] { 15, 16, 17 },
                                             new int[] { 25, 26, 27 } };
            int[] rez =
                intArray
                .SelectManyQ(x => x.Distinct())
                .ToArray();
            Assert.True(rez[0] == 5);
            Assert.True(rez[1] == 15);
            Assert.True(rez[2] == 15);
            Assert.True(rez[3] == 16);
            Assert.True(rez[4] == 17);
            Assert.True(rez[5] == 25);
            Assert.True(rez[6] == 26);
            Assert.True(rez[7] == 27);
        }

        [Fact]
        public void Select_UsingAggregate_ShouldReturn_ListOfIntSums()
        {
            int[][] intMat = new int[][] {new int[] { 1, 2, 3 },
                                          new int[] { 3, 4, 5 },
                                          new int[] { 5, 6, 71 } };
            int[] rez =
                intMat
                .Select(x => x.Aggregate((acc, e) => acc + e))
                .ToArray();
            Assert.True(rez[0] == 6);
            Assert.True(rez[1] == 12);
            Assert.True(rez[2] == 82);
        }

        [Fact]
        public void SelectMany_UsingAggregate_ShouldReturn_ListOfIntSums()
        {
            int[][] intMat = new int[][] {new int[] { 1, 2, 3 },
                                          new int[] { 3, 4, 5 },
                                          new int[] { 5, 6, 71 } };
            int[] rez =
                intMat
                .SelectMany(x => new int[] { x.Aggregate((acc, e) => acc + e) })
                .ToArray();
            Assert.True(rez[0] == 6);
            Assert.True(rez[1] == 12);
            Assert.True(rez[2] == 82);
        }

        [Fact]
        public void WhereQ_ShouldReturn_True()
        {
            int[] intArray = new int[] { 5, 6, 7, 8, 9, 10 };
            int[] rez =
               intArray
               .Where(x => (x % 2) == 0)
               .Select(x => x)
               .ToArray();
            Assert.True(rez[0] == 6);
        }

        [Fact]
        public void DoubleWhereQ_ShouldReturn_True()
        {
            int[] intArray = new int[] { 5, 6, 7, 8, 9, 10 };
            int[] rez =
                intArray
                .WhereQ(x => x % 5 == 0)
                .WhereQ(x => x % 2 == 0)
                .Select(x => x)
                .ToArray();
            Assert.True(rez[0] == 10);
        }

        [Fact]
        public void DoubleWhereQ_ShouldReturn_False()
        {
            int[] intArray = new int[] { 5, 6, 7, 8, 9, 11 };
            int[] rez =
                intArray
                .WhereQ(x => x % 5 == 0)
                .WhereQ(x => x % 2 == 0)
                .Select(x => x)
                .ToArray();
            Assert.True(rez.Length == 0);
        }

        [Fact]
        public void ZipQ_OnEmptySets_ShouldReturn_True()
        {
            int[] intArrayOne = new int[] {};
            int[] intArrayTwo = new int[] {};
            var rez =
                intArrayOne
                .ZipQ(intArrayTwo, (arrOne, arrTwo) => (arrOne * arrTwo))
                .ToArray();
            Assert.True(rez.Length == 0);
        }

        [Fact]
        public void ZipQ_ShouldReturn_True()
        {
            int[] intArrayOne = new int[] { 5, 6, 7, 8, 9, 11 };
            int[] intArrayTwo = new int[] { 5, 6, 7, 8, 9, 11 };
            var rez =
                intArrayOne
                .ZipQ(intArrayTwo, (arrOne, arrTwo) => arrOne * arrTwo)
                .ToArray();
            Assert.True(rez.Length == 6);
        }

        [Fact]
        public void ToDictionaryQ_ShouldSave_KeyValuePairs_ToADict()
        {
            int[] intArray = new int[] { 5, 6, 7, 8, 9, 11 };
            System.Collections.Generic.Dictionary<int, string> dict = intArray
                .Select(i => new { key = i.GetHashCode(), value = i.ToString() })
                .ToDictionary(x => x.key, x => x.value);
            Assert.True(dict.Count() == 6);
            Assert.True(dict[11] == "11");
        }

        [Fact]
        public void ToDictionaryQ_ShouldReturn_True()
        {
            int[] intArray = { 13, 122, 43, 54, 79 };
            System.Collections.Generic.Dictionary<int, string> dict = intArray
                .ToDictionary(key => key.GetHashCode(), value => (value % 2) == 1 ? "Odd" : "Even");
            Assert.True(dict.Count() == 5);
            Assert.True(dict[43] == "Odd");
        }
    }
}