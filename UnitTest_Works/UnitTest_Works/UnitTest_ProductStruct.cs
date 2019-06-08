using System.Collections.Generic;
using Works;
using Xunit;
using static Works.ProductStruct;

namespace UnitTest_Works
{
    public class UnitTest_ProductStruct
    {
        static List<Prod> listOne = new List<Prod>
            {
                new Prod { Name = "prodOne", Quantity = 3 },
                new Prod { Name = "prodTwo", Quantity = 5 },
                new Prod { Name = "prodThree", Quantity = 7 },
                new Prod { Name = "prodFour", Quantity = 9 },
                new Prod { Name = "prodFour", Quantity = 5 }
            };

        static List<Prod> listTwo = new List<Prod>
            {
                new Prod { Name = "prodOne", Quantity = 2 },
                new Prod { Name = "prodTwo", Quantity = 4 },
                new Prod { Name = "prodThree", Quantity = 6 },
                new Prod { Name = "prodFour", Quantity = 8 }
            };

        [Fact]
        public void Query_ShouldReturn_ListOf_Length()
        {
            List<Prod> listTotalPerProduct = ProductStruct.GetTotalPerProduct(listOne, listTwo);
            Assert.True(listTotalPerProduct.Count == 4);
        }

        [Fact]
        public void Query_ShouldReturn_List_Elements()
        {
            List<Prod> listTotalPerProduct = ProductStruct.GetTotalPerProduct(listOne, listTwo);
            Assert.True(listTotalPerProduct[0].Name == "prodOne");
            Assert.True(listTotalPerProduct[0].Quantity == 5);
            
            Assert.True(listTotalPerProduct[1].Name == "prodTwo");
            Assert.True(listTotalPerProduct[1].Quantity == 9);
            
            Assert.True(listTotalPerProduct[2].Name == "prodThree");
            Assert.True(listTotalPerProduct[2].Quantity == 13);
            
            Assert.True(listTotalPerProduct[3].Name == "prodFour");
            Assert.True(listTotalPerProduct[3].Quantity == 22);
        }
    }
}
