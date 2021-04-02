using System;
using Works;
using Xunit;

namespace UnitTest_Works
{
    public class UnitTest_Dictionary
    {

        [Fact]
        public void AddEntry_ShouldBe_True()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>(10);
            dict.Add(1, "11");
            Assert.True(dict[1] == "11");
            var list = dict.Keys;
        }

        [Fact]
        public void AddSameKey_Should_ThrowException()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>(10);
            dict.Add(1, "11");

            var exception = Assert.Throws<ArgumentException>(() => dict.Add(1, "12"));
            Assert.True(exception.Message == "Adding duplicate.");
        }

        [Fact]
        public void AddNullKey_Should_ThrowException()
        {
            Dictionary<int?, string> dict = new Dictionary<int?, string>(10);
            dict.Add(1, "11");

            var exception = Assert.Throws<ArgumentNullException>(() => dict.Add(null, "12"));
            Assert.True(exception.Message == "Key is null.");
        }

        [Fact]
        public void AddKeyOutOfDict_Should_TriggerResize()
        {
            Dictionary<int?, string> dict = new Dictionary<int?, string>(10);
            for (int i = 0; i < 10; i++)
                dict.Add(i, "11");
            dict.Add(10, "11");
            Assert.True(dict[10] == "11");
        }

        [Fact]
        public void RemoveKey_Should_True()
        {
            Dictionary<int?, string> dict = new Dictionary<int?, string>(10);
            for (int i = 0; i < 10; i++)
                dict.Add(i, "11");
            dict.Remove(9);
            Assert.True(dict[9] == null);
        }

        [Fact]
        public void RemoveNullKey_Should_ThrowException()
        {
            Dictionary<int?, string> dict = new Dictionary<int?, string>(10);
            dict.Add(1, "11");

            var exception = Assert.Throws<ArgumentNullException>(() => dict.Remove(null));
            Assert.True(exception.Message == "Key is null.");
        }

        [Fact]
        public void ContainsExistentKey_ShouldBe_True()
        {
            Dictionary<int?, string> dict = new Dictionary<int?, string>(10);
            for (int i = 0; i < 10; i++)
                dict.Add(i, "11");
            Assert.True(dict.ContainsKey(3));
        }

        [Fact]
        public void ContainsNonExistentKey_ShouldBe_False()
        {
            Dictionary<int?, string> dict = new Dictionary<int?, string>(10);
            for (int i = 0; i < 10; i++)
                dict.Add(i, "11");
            Assert.False(dict.ContainsKey(33));
        }

        [Fact]
        public void GetExistentValue_ShouldBe_True()
        {
            Dictionary<int?, string> dict = new Dictionary<int?, string>(10);
            for (int i = 0; i < 10; i++)
                dict.Add(i, "11");
            string value = null;
            Assert.True(dict.TryGetValue(3, out value));
            Assert.True(value == "11");
        }

        [Fact]
        public void GetNonExistentValue_ShouldBe_False()
        {
            Dictionary<int?, string> dict = new Dictionary<int?, string>(10);
            for (int i = 0; i < 10; i++)
                dict.Add(i, "11");
            string value = null;
            Assert.False(dict.TryGetValue(33, out value));
            Assert.True(value == null);
        }

        [Fact]
        public void GetExistentValue_Should_ReturnValue()
        {
            Dictionary<int?, string> dict = new Dictionary<int?, string>(10);
            for (int i = 0; i < 10; i++)
                dict.Add(i, "11");
            Assert.True(dict.GetValueOrDefault(3) == "11");
        }

        [Fact]
        public void GetNonExistentValue_Should_ReturnDefault()
        {
            Dictionary<int?, string> dict = new Dictionary<int?, string>(10);
            for (int i = 0; i < 10; i++)
                dict.Add(i, "11");
            Assert.True(dict.GetValueOrDefault(33) == null);
        }
        
        [Fact]
        public void AddEntry_CheckTime_HashCode()
        {
            Dictionary<string, string> dict1 = new Dictionary<string, string>(10);
            Dictionary<string, string> dict2 = new Dictionary<string, string>(10);
            var watchCheck = new System.Diagnostics.Stopwatch();
            var watchNoCheck = new System.Diagnostics.Stopwatch();

            watchCheck.Start();
            for(int i = 0; i < 999999; i++)
                dict1.Insert(i.ToString(), "10", true, true);
            watchCheck.Stop();
            var execTimeCheck = watchCheck.ElapsedMilliseconds;

            watchNoCheck.Start();
            for (int i = 0; i < 999999; i++)
                dict2.Insert(i.ToString(), "11", true, false);
            watchNoCheck.Stop();

            Assert.True(execTimeCheck <= watchNoCheck.ElapsedMilliseconds);
        }

        [Fact]
        public void AddEntry_CheckMem_HashCode()
        {
            Dictionary<string, string> dict1 = new Dictionary<string, string>(10);
            Dictionary<string, string> dict2 = new Dictionary<string, string>(10);
            long initialMemory = GC.GetTotalMemory(true);

            for (int i = 0; i < 999999; i++)
                dict1.Insert(i.ToString(), "10", true, true);
            long dict1Memory = GC.GetTotalMemory(true) - initialMemory;

            for (int i = 0; i < 999999; i++)
                dict2.Insert(i.ToString(), "11", true, false);
            long dict2Memory = GC.GetTotalMemory(true) - initialMemory - dict1Memory;

            Assert.True(dict1Memory <= dict2Memory);
        }
    }
}