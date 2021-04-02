using Xunit;
using Works;

namespace UnitTest_Works
{
    public class UnitTest_SortedIntArray
    {
        [Fact]
        public void AddValueSucceeds()
        {
            SortedIntArray sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(8);
            sortedIntArray.Add(4);
            sortedIntArray.Add(5);
            sortedIntArray.Add(3);
            sortedIntArray.Add(10);
            Assert.True(sortedIntArray[0] == 3);
        }

        [Fact]
        public void AddValueEqualToPreviousSucceeds()
        {
            SortedIntArray sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(8);
            sortedIntArray.Add(4);
            sortedIntArray.Add(5);
            sortedIntArray.Add(3);
            sortedIntArray.Add(10);
            sortedIntArray.Add(10);
            Assert.True(sortedIntArray[4] == 10);
            Assert.True(sortedIntArray[5] == 10);
        }

        [Fact]
        public void AddValueGreaterThanPreviousSucceeds()
        {
            SortedIntArray sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(8);
            sortedIntArray.Add(4);
            sortedIntArray.Add(5);
            sortedIntArray.Add(3);
            sortedIntArray.Add(10);
            sortedIntArray.Add(11);
            Assert.True(sortedIntArray[5] == 11);
        }

        [Fact]
        public void SetValueEqualToExistingValueSucceeds()
        {
            SortedIntArray sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(8);
            sortedIntArray.Add(4);
            sortedIntArray.Add(5);
            sortedIntArray.Add(3);
            sortedIntArray.Add(10);
            sortedIntArray[0] = 3;
            Assert.True(sortedIntArray[0] == 3);
            Assert.True(sortedIntArray[1] == 4);
        }

        [Fact]
        public void SetOnFirstPositionValueSmallerThanNextSucceeds()
        {
            SortedIntArray sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(8);
            sortedIntArray.Add(4);
            sortedIntArray.Add(5);
            sortedIntArray.Add(3);
            sortedIntArray.Add(10);
            sortedIntArray[0] = -1;
            Assert.True(sortedIntArray[0] == -1);
            Assert.True(sortedIntArray[1] == 4);
        }

        [Fact]
        public void SetValueEqualToPreviousFails()
        {
            SortedIntArray sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(8);
            sortedIntArray.Add(4);
            sortedIntArray.Add(5);
            sortedIntArray.Add(3);
            sortedIntArray.Add(10);
            sortedIntArray[5] = 10;
            Assert.True(sortedIntArray[4] == 10);
            Assert.False(sortedIntArray[5] == 10);
        }

        [Fact]
        public void SetValueSmallerThanExistingValueSucceeds()
        {
            SortedIntArray sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(8);
            sortedIntArray.Add(4);
            sortedIntArray.Add(5);
            sortedIntArray.Add(3);
            sortedIntArray.Add(10);
            sortedIntArray[0] = 1;
            Assert.True(sortedIntArray[0] == 1);
            Assert.True(sortedIntArray[1] == 4);
        }

        [Fact]
        public void SetValueSmallerThanPreviousValueFails()
        {
            SortedIntArray sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(8);
            sortedIntArray.Add(4);
            sortedIntArray.Add(5);
            sortedIntArray.Add(3);
            sortedIntArray.Add(10);
            sortedIntArray[1] = 1;
            Assert.True(sortedIntArray[0] == 3);
            Assert.True(sortedIntArray[1] == 4);
        }

        [Fact]
        public void SetValueGreaterThanPreviousFails()
        {
            SortedIntArray sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(8);
            sortedIntArray.Add(4);
            sortedIntArray.Add(5);
            sortedIntArray.Add(3);
            sortedIntArray.Add(10);
            sortedIntArray[5] = 11;
            Assert.True(sortedIntArray[4] == 10);
            Assert.False(sortedIntArray[5] == 11);
        }

        [Fact]
        public void SetInArrayValueGreaterThanPreviousFails()
        {
            SortedIntArray sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(8);
            sortedIntArray.Add(9);
            sortedIntArray.Add(5);
            sortedIntArray.Add(12);
            sortedIntArray.Add(10);
            sortedIntArray[2] = 7;
            Assert.False(sortedIntArray[2] == 7);
        }

        [Fact]
        public void SetLastValueGreaterThanPreviousSucceeds()
        {
            SortedIntArray sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(8);
            sortedIntArray.Add(4);
            sortedIntArray.Add(5);
            sortedIntArray[2] = 7;
            Assert.True(sortedIntArray[2] == 7);
        }

        [Fact]
        public void InsertValueEqualToPreviousSucceeds()
        {
            SortedIntArray sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(8);
            sortedIntArray.Add(4);
            sortedIntArray.Add(5);
            sortedIntArray.Add(3);
            sortedIntArray.Add(10);
            sortedIntArray.Insert(5, 10);
            Assert.True(sortedIntArray[4] == 10);
            Assert.True(sortedIntArray[5] == 10);
        }

        [Fact]
        public void InsertValueEqualToNextSucceeds()
        {
            SortedIntArray sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(8);
            sortedIntArray.Add(4);
            sortedIntArray.Add(5);
            sortedIntArray.Add(3);
            sortedIntArray.Add(10);
            sortedIntArray.Insert(0, 3);
            Assert.True(sortedIntArray[0] == 3);
        }

        [Fact]
        public void InsertInArrayValueEqualToNextSucceeds()
        {
            SortedIntArray sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(8);
            sortedIntArray.Add(4);
            sortedIntArray.Add(5);
            sortedIntArray.Add(3);
            sortedIntArray.Add(10);
            sortedIntArray[2] = 8;
            Assert.True(sortedIntArray[2] == 8);
        }

        [Fact]
        public void InsertValueSmallerThanPreviousFails()
        {
            SortedIntArray sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(8);
            sortedIntArray.Add(4);
            sortedIntArray.Add(5);
            sortedIntArray.Add(3);
            sortedIntArray.Add(10);
            sortedIntArray.Insert(5, 6);
            Assert.True(sortedIntArray[4] == 10);
        }

        [Fact]
        public void InsertInArrayValueSmallerThanNextSucceeds()
        {
            SortedIntArray sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(8);
            sortedIntArray.Add(4);
            sortedIntArray.Add(5);
            sortedIntArray.Add(0);
            sortedIntArray.Add(10);
            sortedIntArray[2] = 6;
            Assert.True(sortedIntArray[2] == 6);
        }

        [Fact]
        public void InsertValueSmallerThanNextSucceeds()
        {
            SortedIntArray sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(8);
            sortedIntArray.Add(4);
            sortedIntArray.Add(5);
            sortedIntArray.Add(3);
            sortedIntArray.Add(10);
            sortedIntArray.Insert(0, 1);
            Assert.True(sortedIntArray[0] == 1);
        }

        [Fact]
        public void InsertValueGreaterThanPreviousFails()
        {
            SortedIntArray sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(8);
            sortedIntArray.Add(4);
            sortedIntArray.Add(5);
            sortedIntArray.Add(3);
            sortedIntArray.Add(10);
            sortedIntArray.Insert(0, 7);
            Assert.False(sortedIntArray[0] == 7);
        }

        [Fact]
        public void InsertValueGreaterThanPreviousSucceeds()
        {
            SortedIntArray sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(8);
            sortedIntArray.Add(4);
            sortedIntArray.Add(5);
            sortedIntArray.Add(3);
            sortedIntArray.Add(10);
            sortedIntArray.Insert(5, 11);
            Assert.True(sortedIntArray[5] == 11);
        }

        [Fact]
        public void InsertValueOnIncorrectPositionFails()
        {
            SortedIntArray sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(8);
            sortedIntArray.Add(4);
            sortedIntArray.Add(5);
            sortedIntArray.Add(0);
            sortedIntArray.Add(10);
            sortedIntArray[6] = 13;
            Assert.False(sortedIntArray[6] == 13);
        }
    }
}
