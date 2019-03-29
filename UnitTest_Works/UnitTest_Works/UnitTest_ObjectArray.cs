using Works;
using Xunit;

namespace UnitTest_Works
{
    public class UnitTest_ObjectArray
    {
        public ObjectArray ArrayFactory()
        {
            ObjectArray intArr = new ObjectArray();
            string[] input = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            int size = input.Length;

            if (size > 0)
            {
                for (int i = 0; i < size; i++)
                {
                    if (int.TryParse(input[i], out int elem))
                        intArr.Add(elem);
                }
            }
            return (intArr);
        }

        [Fact]
        public void ShouldBe_True_CountOfActualElements()
        {
            ObjectArray arr = ArrayFactory();
            Assert.True(arr.Count == 9);
        }

        [Fact]
        public void ShouldBe_False_CountOfActualElements()
        {
            ObjectArray arr = ArrayFactory();
            Assert.False(arr.Count == 8);
        }

        [Fact]
        public void ShouldBe_False_CountOfActualElements_ArrayNotEmpty()
        {
            ObjectArray arr = ArrayFactory();
            Assert.False(arr.Count == 0);
        }
    }
}
