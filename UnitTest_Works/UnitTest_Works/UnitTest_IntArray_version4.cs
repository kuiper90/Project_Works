﻿using Works;
using Xunit;

namespace UnitTest_Works
{
    public class UnitTest_IntArray_version4
    {
        public IntArray_version4 ArrayFactory()
        {
            IntArray_version4 intArr = new IntArray_version4();
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
            IntArray_version4 arr = ArrayFactory();
            Assert.True(arr.Count == 9);
        }

        [Fact]
        public void ShouldBe_False_CountOfActualElements()
        {
            IntArray_version4 arr = ArrayFactory();
            Assert.False(arr.Count == 8);
        }

        [Fact]
        public void ShouldBe_False_CountOfActualElements_ArrayNotEmpty()
        {
            IntArray_version4 arr = ArrayFactory();
            Assert.False(arr.Count == 0);
        }

        [Fact]
        public void ShouldBe_True_ArrayContainsNumber()
        {
            IntArray_version4 arr = ArrayFactory();
            Assert.True(arr.Contains(1) == true);
        }

        [Fact]
        public void ShouldBe_False_ArrayEmpty()
        {
            IntArray_version4 arr = ArrayFactory();
            Assert.False(arr.Contains(default(int)));
        }

        [Fact]
        public void ShouldBe_True_IndexOfElement()
        {
            IntArray_version4 arr = ArrayFactory();
            Assert.True(arr.IndexOf(1) == 0);
        }
    }
}
