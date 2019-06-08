using System.Collections.Generic;
using Works;
using Xunit;

namespace UnitTest_Works
{
    public class UnitTest_Product
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
        public void GetProductsWithAtLeastOneGivenFeature_ShouldReturn_ListOf_Length()
        {
            List<Product> listOfProductsWithAtLeastOneGivenFeature = Product.GetProductsWithAtLeastOneGivenFeature(productList, featureList);
            Assert.True(listOfProductsWithAtLeastOneGivenFeature.Count == 3);
        }

        [Fact]
        public void GetProductsWithAtLeastOneGivenFeature_ShouldReturn_List_Elements()
        {
            List<Product> listOfProductsWithAtLeastOneGivenFeature = Product.GetProductsWithAtLeastOneGivenFeature(productList, featureList);
            Assert.True(listOfProductsWithAtLeastOneGivenFeature[0].Name == "prodOne");
            List<Feature> fl0 = (List<Feature>)listOfProductsWithAtLeastOneGivenFeature[0].Features;
            Assert.True(fl0[0].Id == 0);
            Assert.True(fl0[1].Id == 1);
            Assert.True(fl0[2].Id == 2);
            Assert.True(fl0[3].Id == 10);
            Assert.True(fl0[4].Id == 11);
            Assert.True(fl0[5].Id == 12);


            Assert.True(listOfProductsWithAtLeastOneGivenFeature[1].Name == "prodTwo");
            List<Feature> fl1 = (List<Feature>)listOfProductsWithAtLeastOneGivenFeature[1].Features;
            Assert.True(fl1[0].Id == 0);
            Assert.True(fl1[1].Id == 1);
            Assert.True(fl1[2].Id == 2);
            Assert.True(fl1[3].Id == 10);
            Assert.True(fl1[4].Id == 11);
            Assert.True(fl1[5].Id == 12);
            Assert.True(fl1[6].Id == 6);

            Assert.True(listOfProductsWithAtLeastOneGivenFeature[2].Name == "prodThree");
            List<Feature> fl2 = (List<Feature>)listOfProductsWithAtLeastOneGivenFeature[2].Features;
            Assert.True(fl2[0].Id == 10);
            Assert.True(fl2[1].Id == 2);
        }

        [Fact]
        public void GetProductsWithAllGivenFeatures_ShouldReturn_ListOf_Length()
        {
            List<Product> listOfProductsWithAllGivenFeatures = Product.GetProductsWithAllGivenFeatures(productList, featureList);
            Assert.True(listOfProductsWithAllGivenFeatures.Count == 2);
        }

        [Fact]
        public void GetProductsWithAllGivenFeatures_ShouldReturn_List_Elements()
        {
            List<Product> listOfProductsWithAllGivenFeatures = Product.GetProductsWithAllGivenFeatures(productList, featureList);
            Assert.True(listOfProductsWithAllGivenFeatures[0].Name == "prodOne");
            List<Feature> fl0 = (List<Feature>)listOfProductsWithAllGivenFeatures[0].Features;
            Assert.True(fl0[0].Id == 0);
            Assert.True(fl0[1].Id == 1);
            Assert.True(fl0[2].Id == 2);
            Assert.True(fl0[3].Id == 10);
            Assert.True(fl0[4].Id == 11);
            Assert.True(fl0[5].Id == 12);
            Assert.True(listOfProductsWithAllGivenFeatures[1].Name == "prodTwo");
            List<Feature> fl1 = (List<Feature>)listOfProductsWithAllGivenFeatures[1].Features;
            Assert.True(fl1[0].Id == 0);
            Assert.True(fl1[1].Id == 1);
            Assert.True(fl1[2].Id == 2);
            Assert.True(fl1[3].Id == 10);
            Assert.True(fl1[4].Id == 11);
            Assert.True(fl1[5].Id == 12);
            Assert.True(fl1[6].Id == 6);           
        }

        [Fact]
        public void GetProductsWithNoGivenFeature_ShouldReturn_ListOf_Length()
        {
            List<Product> listOfProductsWithNoGivenFeature = Product.GetProductsWithNoGivenFeature(productList, featureList);
            Assert.True(listOfProductsWithNoGivenFeature.Count == 1);
        }

        public void GetProductsWithNoGivenFeature_ShouldReturn_List_Elements()
        {
            List<Product> listOfProductsWithNoGivenFeature = Product.GetProductsWithNoGivenFeature(productList, featureList);
            Assert.True(listOfProductsWithNoGivenFeature[0].Name == "prodFour");
            List<Feature> fl0 = (List<Feature>)listOfProductsWithNoGivenFeature[0].Features;
            Assert.True(fl0[0].Id == 5);
            Assert.True(fl0[1].Id == 4);
        }
    }
}
