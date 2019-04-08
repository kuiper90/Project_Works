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
            string[]  input = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            int size = input.Length;

            if (size > 0)
            {
                for (int i = 0; i < size; i++)
                    arr.Add(input[i]);
            }
            //arr.IsReadOnly = true;
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
        public void ShouldBe_False_AddElement_IsReadOnly()
        {
            SortedList<string> arr = ArrayFactory();
            arr.IsReadOnly = true;
            Action act = () => arr.Add("11");
            var exception = Assert.Throws<NotSupportedException>(act);
            Assert.True(exception.Message == "Array is readonly.");
        }

        [Fact]
        public void ShouldBe_False_SetElement_IsReadOnly()
        {
            SortedList<string> arr = ArrayFactory();
            arr.IsReadOnly = true;
            Action act = () => arr[1] = "11";
            var exception = Assert.Throws<NotSupportedException>(act);
            Assert.True(exception.Message == "Array is readonly.");
        }

        [Fact]
        public void ShouldBe_False_AddElement_First()
        {
            Lists<string> arr = ArrayFactory();           
            var exception = Assert.Throws<Exception>(() => arr.Insert(1, "90"));
            Assert.False(arr[1] == "90");
            Assert.True(exception.Message == "Inserting element would result in an unsorted list.");
        }

        [Fact]
        public void ShouldBe_True_AddElement_Last()
        {
            SortedList<string> arr = ArrayFactory();
            arr.Insert(9, "90");
            Assert.True(arr[9] == "90");
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
        public void ShouldBe_True_InsertElement_ExceptionThrown()
        {
            SortedList<string> arr = ArrayFactory();            
            var exception = Assert.Throws<Exception>(() => arr.Insert(5, "16"));
            Assert.True(exception.Message == "Inserting element would result in an unsorted list.");
        }

        [Fact]
        public void ShouldBe_True_InsertNoElement_InsideTheArray()
        {
            SortedList<string> arr = ArrayFactory();
            var exception = Assert.Throws<DoesNotComplyWithSortedState>(() => arr.Insert(5, ""));
            Assert.True(exception.Message == "Element is null or empty.");
        }

        [Fact]
        public void ShouldBe_True_InsertNoElement_AtTheBeginning()
        {
            SortedList<string> arr = ArrayFactory();
            var exception = Assert.Throws<DoesNotComplyWithSortedState>(() => arr.Insert(0, ""));
            Assert.True(exception.Message == "Element is null or empty.");
        }

        [Fact]
        public void ShouldBe_True_InsertNoElement_AtTheEnd()
        {
            SortedList<string> arr = ArrayFactory();
            var exception = Assert.Throws<DoesNotComplyWithSortedState>(() => arr.Insert(8, ""));
            Assert.True(exception.Message == "Element is null or empty.");
        }

        [Fact]
        public void ShouldThrow_Exception_Insert_Element_IsReadOnly()
        {
            SortedList<string> arr = ArrayFactory();
            arr.IsReadOnly = true;
            var exception = Assert.Throws<NotSupportedException>(() => arr.Insert(11, "1"));
            Assert.True(exception.Message == "Array is readonly.");
        }

        [Fact]
        public void ShouldThrow_Exception_Insert_NegativeIndex()
        {
            SortedList<string> arr = ArrayFactory();            
            Action act = () => arr.Insert(-1, "-1");
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [Fact]
        public void ShouldThow_Exception_WhenInsertElement_AtNegativeIndex_Message()
        {
            SortedList<string> arr = ArrayFactory();
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => arr.Insert(-1, "-1"));
            Assert.True(exception.Message == "Invalid index.");
        }

        [Fact]
        public void ShouldThrow_Exception_Insert_InvalidIndex()
        {
            SortedList<string> arr = ArrayFactory();
            Action act = () => arr.Insert(13, "13");
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [Fact]
        public void ShouldThow_Exception_WhenInsertElement_AtInvalidIndex_Message()
        {
            SortedList<string> arr = ArrayFactory();
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => arr.Insert(10, "10"));
            Assert.True(exception.Message == "Invalid index.");
        }

        [Fact]
        public void ShouldThrow_Exception_Insert_AtIndexOutOfRange()
        {
            SortedList<string> arr = ArrayFactory();
            Action act = () => arr.Insert(10, "10");
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }
    }
}