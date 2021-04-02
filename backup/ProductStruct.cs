using System;
using System.Collections.Generic;
using System.Linq;

namespace Works
{
    public class ProductStruct
    {
        public struct Prod
        {
            public string Name;
            public int Quantity;
        }

        public static List<Prod> GetTotalPerProduct(List<Prod> listOne, List<Prod> listTwo)
        {
            List<Prod> listTotalPerProduct =
                listOne.Concat(listTwo)
                       .GroupBy(p => p.Name)
                       .Select(g => new Prod { Name = g.Key, Quantity = g.Sum(p => p.Quantity) })
                       .ToList();

            return listTotalPerProduct;
        }
    }
}
