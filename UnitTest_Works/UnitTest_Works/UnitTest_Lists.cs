using Works;
using Xunit;

namespace UnitTest_Works
{
    public class UnitTest_Lists
    {
        public Lists<string> ArrayFactory()
        {
            Lists<string> arr = new Lists<string>();
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
        public void ShouldBe_True_CountOfActualElements()
        {
            Lists<string> arr = ArrayFactory();
            Assert.True(arr.Count == 9);
        }

        [Fact]
        public void ShouldBe_False_CountOfActualElements_ArrayNotEmpty()
        {
            Lists<string> arr = ArrayFactory();
            Assert.False(arr.Count == 0);
        }

        [Fact]
        public void ShouldBe_False_CountOfActualElements()
        {
            Lists<string> arr = ArrayFactory();
            Assert.False(arr.Count == 8);
        }

        [Fact]
        public void ShouldBe_True_ArrayContainsNumber()
        {
            Lists<string> arr = ArrayFactory();
            Assert.True(arr.Contains("1") == true);
        }

        [Fact]
        public void ShouldBe_False_ArrayEmpty()
        {
            Lists<string> arr = ArrayFactory();
            Assert.False(arr.Contains(""));
        }

        [Fact]
        public void ShouldBe_True_IndexOfElement()
        {
            Lists<string> arr = ArrayFactory();
            Assert.True(arr.IndexOf("1") == 0);
        }
    }
}
