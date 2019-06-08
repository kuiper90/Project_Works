using System.Linq;
using System.Collections.Generic;

namespace Works
{
    public class Product
    {
        public string Name { get; set; }

        public ICollection<Feature> Features { get; set; }

        public static List<Product> GetProductsWithAtLeastOneGivenFeature(List<Product> productList, List<Feature> featureList)
        {
            List<Product> listOfProductsWithAtLeastOneGivenFeature =
               productList
                            .Where(prod => prod.Features.Any(
                                feat => featureList.Select(
                                    f => f.Id).Contains(feat.Id))).ToList();
            return listOfProductsWithAtLeastOneGivenFeature;
        }

        public static List<string> GetProductsStringWithAtLeastOneGivenFeature(List<Product> productList, List<Feature> featureList)
        {
            List<string> listOfProductsWithAtLeastOneGivenFeature =
                productList
                            .Where(prod => prod.Features.Any(
                                feat => featureList
                                .Select(f => f.Id)
                                    .Contains(feat.Id)))
                                    .Select(prod => {
                                        List<Feature> featList = (List<Feature>)prod.Features;
                                        string rez = prod.Name;
                                        foreach (Feature f in (List<Feature>)featList)
                                            rez = rez + " " + f.Id;
                                        return rez;
                                    }).ToList();
        return listOfProductsWithAtLeastOneGivenFeature;
    }

        public static List<Product> GetProductsWithAllGivenFeatures(List<Product> productList, List<Feature> featureList)
        {
            List<Product> listOfProductsWithAllGivenFeatures =
                productList
                            .Where(prod => featureList.All(feat => prod.Features
                                .Select(f => f.Id).Contains(feat.Id))).ToList();
            return listOfProductsWithAllGivenFeatures;
        }

        public static List<Product> GetProductsWithNoGivenFeature(List<Product> productList, List<Feature> featureList)
        {
            List<Product> listOfProductsWithNoGivenFeature =
                productList
                            .Where(prod => featureList.All(
                                feat => !prod.Features.Select(
                                    f => f.Id).Contains(feat.Id))).ToList();
            return listOfProductsWithNoGivenFeature;
        }
    }
}
