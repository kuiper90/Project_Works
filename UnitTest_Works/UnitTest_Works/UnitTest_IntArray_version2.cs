using Works;
using Xunit;

namespace UnitTest_Works
{
    public class UnitTest_IntArray_version2
    {
        public IntArray_version2 ArrayFactory()
        {
            IntArray_version2 intArr = new IntArray_version2();
            int[] input = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int size = input.Length;

            if (size > 0)
            {
                for (int i = 0; i < size; i++)
                    intArr.Add(input[i]);
            }
            return (intArr);
        }

        [Fact]
        public void ShouldBe_True_CountOfActualElements()
        {
            IntArray_version2 arr = ArrayFactory();
            Assert.True(arr.Count == 9);
        }

        [Fact]
        public void ShouldBe_False_CountOfActualElements()
        {
            IntArray_version2 arr = ArrayFactory();
            Assert.False(arr.Count == 8);
        }



        [Fact]
        public void ShouldBe_True_ArrayContainsNumber()
        {
            IntArray_version2 arr = ArrayFactory();
            Assert.True(arr.Contains(1) == true);
        }

        [Fact]
        public void ShouldBe_True_IndexOfElement()
        {
            IntArray_version2 arr = ArrayFactory();
            Assert.True(arr.IndexOf(1) == 0);
        }
    }
}
