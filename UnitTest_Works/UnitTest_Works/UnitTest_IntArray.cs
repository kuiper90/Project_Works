using Xunit;
using Works;

namespace UnitTest_Works
{
    public class UnitTest_IntArray
    {
        [Fact]
        public void AddJustOneIntToArraySucceeds()
        {
            IntArray intArray = new IntArray();
            intArray.Add(7);
            Assert.True(intArray[0] == 7);
            Assert.True(intArray.Count == 1);
        }

        [Fact]
        public void GetValueFromIncorrectIndexFails()
        {
            IntArray intArray = new IntArray();
            intArray.Add(7);
            intArray.Add(8);
            Assert.True(intArray[-1] == 0);
            Assert.True(intArray[4] == 0);
            Assert.True(intArray.Count == 2);
        }

        [Fact]
        public void GetValueFromUnoccupiedPositionReturnsDefaultZero()
        {
            IntArray intArray = new IntArray();
            intArray.Add(7);
            intArray.Add(8);
            Assert.True(intArray[2] == 0);
            Assert.True(intArray.Count == 2);
        }

        [Fact]
        public void AddOneIntExtraToArraySucceeds()
        {
            IntArray intArray = new IntArray();
            intArray.Add(7);
            intArray.Add(8);
            intArray.Add(9);
            intArray.Add(10);
            intArray.Add(11);
            Assert.True(intArray[4] == 11);
            Assert.True(intArray.Count == 5);
        }

        [Fact]
        public void SetElementToArraySucceeds()
        {
            IntArray intArray = new IntArray();
            intArray.Add(7);
            intArray.Add(8);
            intArray.Add(9);
            intArray.Add(10);
            intArray[1] = 11;
            Assert.True(intArray[1] == 11);
            Assert.True(intArray.Count == 4);
        }

        [Fact]
        public void SetLastElementToArraySucceeds()
        {
            IntArray intArray = new IntArray();
            intArray.Add(7);
            intArray.Add(8);
            intArray.Add(9);
            intArray.Add(10);
            intArray[3] = 11;
            Assert.True(intArray[3] == 11);
            Assert.True(intArray.Count == 4);
        }

        [Fact]
        public void ContainsElementSucceeds()
        {
            IntArray intArray = new IntArray();
            intArray.Add(7);
            intArray.Add(8);
            intArray.Add(9);
            Assert.True(intArray.Contains(9));
        }

        [Fact]
        public void SetElementSucceeds()
        {
            IntArray intArray = new IntArray();
            intArray.Add(7);
            intArray.Add(8);
            intArray.Add(9);
            intArray.Add(10);
            intArray[1] = 18;
            Assert.True(intArray.IndexOf(18) == 1);
        }

        [Fact]
        public void IndexOfElementSucceeds()
        {
            IntArray intArray = new IntArray();
            intArray.Add(7);
            intArray.Add(8);
            intArray.Add(9);
            intArray.Add(10);
            Assert.True(intArray.IndexOf(9) == 2);
        }

        [Fact]
        public void InsertElementSucceeds()
        {
            IntArray intArray = new IntArray();
            intArray.Add(7);
            intArray.Add(8);
            intArray.Add(9);
            intArray.Insert(1, 5);
            Assert.True(intArray.IndexOf(5) == 1);
            Assert.True(intArray.Count == 4);
        }

        [Fact]
        public void InsertElementWhenCountAndSizeAreEqualSucceeds()
        {
            IntArray intArray = new IntArray();
            intArray.Add(7);
            intArray.Add(8);
            intArray.Add(9);
            intArray.Add(10);
            intArray.Insert(1, 5);
            Assert.True(intArray.IndexOf(5) == 1);
            Assert.True(intArray.Count == 5);
        }

        [Fact]
        public void InsertElementAfterLastOccupiedPositionSucceeds()
        {
            IntArray intArray = new IntArray();
            intArray.Add(7);
            intArray.Add(8);
            intArray.Add(9);
            intArray.Insert(2, 5);
            Assert.True(intArray.IndexOf(5) == 2);
            Assert.True(intArray.Count == 4);
        }

        [Fact]
        public void RemoveElementSucceeds()
        {
            IntArray intArray = new IntArray();
            intArray.Add(7);
            intArray.Add(8);
            intArray.Add(9);
            intArray.Remove(8);
            Assert.True(intArray.IndexOf(9) == 1);
            Assert.True(intArray.Count == 2);
        }

        [Fact]
        public void RemoveLastElementSucceeds()
        {
            IntArray intArray = new IntArray();
            intArray.Add(7);
            intArray.Add(8);
            intArray.Add(9);
            intArray.Remove(9);
            Assert.False(intArray.Contains(9));
            Assert.True(intArray.Count == 2);
        }

        [Fact]
        public void RemoveAtIndexSucceeds()
        {
            IntArray intArray = new IntArray();
            intArray.Add(7);
            intArray.Add(8);
            intArray.Add(9);
            intArray.RemoveAt(0);
            Assert.True(intArray.IndexOf(9) == 1);
            Assert.True(intArray.Count == 2);
        }

        [Fact]
        public void RemoveAtIncorrectIndexReturnsDefaultZero()
        {
            IntArray intArray = new IntArray();
            intArray.RemoveAt(0);
            Assert.True(intArray[0] == 0);
            Assert.True(intArray.Count == 0);
        }
    }
}