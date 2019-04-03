using Works;
using Xunit;

namespace UnitTest_Works
{
    public class UnitTest_List
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
        public void ShouldBe_False_CountOfActualElements()
        {
            Lists<string> arr = ArrayFactory();
            Assert.False(arr.Count == 8);
        }

        [Fact]
        public void ShouldBe_False_CountOfActualElements_ArrayNotEmpty()
        {
            Lists<string> arr = ArrayFactory();
            Assert.False(arr.Count == 0);
        }

        [Fact]
        public void ShouldBe_False_DifferentIntegersNotEqual()
        {
            Lists<string> arr = ArrayFactory();
            Assert.False(Lists<string>.Equals("1", "2"));
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

        [Fact]
        public void ShouldBe_True_AddElement()
        {
            Lists<string> arr = ArrayFactory();
            arr.Add("-1");
            Assert.True(arr[arr.Count - 1] == "-1");
        }

        [Fact]
        public void ShouldBe_True_ClearArray()
        {
            Lists<string> arr = ArrayFactory();
            arr.Clear();
            Assert.True(arr.Count == 0);
        }

        [Fact]
        public void ShouldBe_True_InsertElement()
        {
            Lists<string> arr = ArrayFactory();
            arr.Insert(5, "16");
            Assert.True(arr[4] == "5");
            Assert.True(arr[5] == "16");
            Assert.True(arr[6] == "6");
        }

        [Fact]
        public void ShouldBe_True_InsertElement_AtTheBeginning()
        {
            Lists<string> arr = ArrayFactory();
            arr.Insert(0, "16");
            Assert.True(arr[0] == "16");
            Assert.True(arr[1] == "1");
        }

        [Fact]
        public void ShouldBe_True_InsertElement_AtTheEnd()
        {
            Lists<string> arr = ArrayFactory();
            arr.Insert(9, "16");
            Assert.True(arr[8] == "9");
            Assert.True(arr[9] == "16");
        }

        [Fact]
        public void ShouldBe_True_SwapTwoStrings()
        {
            string str1 = "3";
            string str2 = "5";
            Lists<string>.Swap<string>(ref str1, ref str2);
            Assert.True(str1 == "5");
            Assert.True(str2 == "3");
        }

        [Fact]
        public void ShouldBe_True_RemoveElement()
        {
            Lists<string> arr = ArrayFactory();
            arr.Remove("3");
            Assert.True(arr[1] == "2");
            Assert.True(arr[2] == "4");
        }

        [Fact]
        public void ShouldBe_True_RemoveFirstElement()
        {
            Lists<string> arr = ArrayFactory();
            arr.Remove("1");
            Assert.True(arr[0] == "2");
        }

        [Fact]
        public void ShouldBe_True_RemoveLastElement()
        {
            Lists<string> arr = ArrayFactory();
            arr.Remove("9");
            Assert.True(arr[8] == null);
        }

        [Fact]
        public void ShouldBe_True_RemoveNoElement()
        {
            Lists<string> arr = ArrayFactory();
            arr.Remove("");
            Assert.True(arr[0] == "1");
        }

        [Fact]
        public void ShouldBe_True_RemoveElement_AtIndex()
        {
            Lists<string> arr = ArrayFactory();
            arr.RemoveAt(5);
            Assert.True(arr[4] == "5");
            Assert.True(arr[5] == "7");
        }
    }
}
