using System;
using Works;
using Xunit;

namespace UnitTest_Works
{
    public class UnitTest_SortedList
    {
        public SortedList<string> ArrayFactory()
        {
            SortedList<string> arr = new SortedList<string>();
            string[] input = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            int size = input.Length;

            if (size > 0)
            {
                for (int i = 0; i < size; i++)
                    arr.Add(input[i]);
            }
            return (arr);
        }

        [Fact]
        public void ShouldBe_True_AddElement()
        {
            SortedList<string> arr = ArrayFactory();
            arr.Add("11");
            Assert.True(arr[1] == "11");
        }

        [Fact]        
        public void ShouldBe_True_InsertElement_ExceptionThrown()
        {
            SortedList<string> arr = ArrayFactory();            
            var exception = Assert.Throws<Exception>(() => arr.Insert(5, "16"));
            Assert.True(exception.Message == "Inserting element would result in an unsorted list.");
        }


        [Fact]
        public void ShouldBe_True_InsertElement()
        {
            SortedList<string> arr = ArrayFactory();
            arr.Insert(1, "16");
            Assert.True(arr[1] == "16");
            Assert.True(arr[2] == "2");
        }

        [Fact]
        public void ShouldBe_True_InsertNoElement_InsideTheArray()
        {
            SortedList<string> arr = ArrayFactory();
            var exception = Assert.Throws<Exception>(() => arr.Insert(5, ""));
            Assert.True(exception.Message == "Element is null or empty.");
        }

        [Fact]
        public void ShouldBe_True_InsertNoElement_AtTheBeginning()
        {
            SortedList<string> arr = ArrayFactory();
            var exception = Assert.Throws<Exception>(() => arr.Insert(0, ""));
            Assert.True(exception.Message == "Element is null or empty.");
        }

        [Fact]
        public void ShouldBe_True_InsertNoElement_AtTheEnd()
        {
            SortedList<string> arr = ArrayFactory();
            var exception = Assert.Throws<Exception>(() => arr.Insert(8, ""));
            Assert.True(exception.Message == "Element is null or empty.");
        }
    }
}
