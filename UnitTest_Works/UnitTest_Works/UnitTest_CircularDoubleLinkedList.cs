using System;
using System.Collections.Generic;
using Works;
using Xunit;

namespace UnitTest_Works
{
    public class UnitTest_CircularDoubleLinkedList
    {
        public CircularDoubleLinkedList<string> CircularDoubleLinkedListFactory()
        {
            CircularDoubleLinkedList<string> lst = new CircularDoubleLinkedList<string>();
            string[] input = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            int size = input.Length;

            if (size > 0)
            {
                for (int i = 0; i < size; i++)
                    lst.AddLast(input[i]);
            }
            return (lst);
        }

        [Fact]
        public void AddLastNode_ShoudlBe_True()
        {
            CircularDoubleLinkedList<string> lst = new CircularDoubleLinkedList<string>();
            lst.AddLast("11");
            Assert.True(lst[0] == "11");
        }

        [Fact]
        public void AddFirstNode_ShouldBe_True()
        {
            CircularDoubleLinkedList<string> lst = new CircularDoubleLinkedList<string>();
            lst.AddFirst("-11");
            Assert.True(lst[0] == "-11");
        }

        [Fact]
        public void ShouldBe_True_AddBeforeFirstNode()
        {
            CircularDoubleLinkedList<string> lst = CircularDoubleLinkedListFactory();
            CircularLinkedListNode<string> current = lst.FindFirst("1");
            lst.AddBefore(current, "-1");
            Assert.True(lst[0] == "-1");
            Assert.True(lst[1] == "1");
        }

        [Fact]
        public void AddAfterLastNode_ShouldBe_True()
        {
            CircularDoubleLinkedList<string> lst = CircularDoubleLinkedListFactory();
            CircularLinkedListNode<string> current = lst.FindFirst("9");
            lst.AddAfter(current, "-9");
            Assert.True(lst[8] == "9");
            Assert.True(lst[9] == "-9");
        }

        [Fact]
        public void ContainsExistentValue_ShouldBe_True()
        {
            CircularDoubleLinkedList<string> lst = CircularDoubleLinkedListFactory();
            Assert.True(lst.Contains("5"));
        }

        [Fact]
        public void AddEmptyAfterNode_ShouldBe_True()
        {
            CircularDoubleLinkedList<string> lst = CircularDoubleLinkedListFactory();
            CircularLinkedListNode<string> current = lst.FindFirst("3");
            lst.AddAfter(current, "-11");
            Assert.True(lst[2] == "3");
            Assert.True(lst[3] == "-11");
        }

        [Fact]
        public void AddAfterMidNode_ShouldBe_True()
        {
            CircularDoubleLinkedList<string> lst = CircularDoubleLinkedListFactory();
            CircularLinkedListNode<string> current = lst.FindFirst("3");
            CircularLinkedListNode<string> newCurrent = new CircularLinkedListNode<string>("20", current.list);
            lst.AddAfter(current, newCurrent);
            Assert.True(lst[2] == "3");
            Assert.True(lst[3] == "20");
        }

        [Fact]
        public void AddEmptyBeforeMidNode_ShouldBe_True()
        {
            CircularDoubleLinkedList<string> lst = CircularDoubleLinkedListFactory();
            CircularLinkedListNode<string> current = lst.FindFirst("2");
            lst.AddBefore(current, "");
            Assert.True(lst[1] == "");
            Assert.True(lst[2] == "2");
        }

        [Fact]
        public void AddBeforeNode_ShouldBe_True()
        {
            var lst = new CircularDoubleLinkedList<string> { "1", "2", "3", "2", "1" };
            lst.AddBefore(lst.FindLast("2"), "3");
            Assert.Equal("1 3 2 3 2 1".Split(' '), lst);
        }

        [Fact]
        public void GetNodeAfterNode_ShouldBe_True()
        {
            var lst = new CircularDoubleLinkedList<string> { "1", "2", "3" };
            IEnumerator<string> e = lst.GetEnumerator();
            e.MoveNext();
            Assert.Equal("1", e.Current);
            e.MoveNext();
            Assert.Equal("2", e.Current);
            e.MoveNext();
            Assert.Equal("3", e.Current);
        }

        [Fact]
        public void GetNodeBeforeNode_ShouldBe_True()
        {
            var lst = new CircularDoubleLinkedList<string> { "1", "2", "3" };
            IEnumerator<string> e = lst.GetReverseEnumerator();
            e.MoveNext();
            Assert.Equal("3", e.Current);
            e.MoveNext();
            Assert.Equal("2", e.Current);
            e.MoveNext();
            Assert.Equal("1", e.Current);
        }

        [Fact]
        public void DeleteLastNode_ShouldBe_True()
        {
            CircularDoubleLinkedList<string> lst = CircularDoubleLinkedListFactory();
            lst.RemoveLast();
            Assert.True(lst[7] == "8");
            Assert.False(lst[8] == "9");
        }

        [Fact]
        public void DeleteFirstNode_ShouldBe_True()
        {
            var lst = new CircularDoubleLinkedList<int> { 1, 2, 3 };
            lst.RemoveNode(lst.FindFirst(2));
            Assert.Equal(new[] { 1, 3 }, lst);
        }

        [Fact]
        public void RemoveFirst_ShouldDelete_FirstNode()
        {
            var lst = new CircularDoubleLinkedList<int> { 1, 2, 3 };
            Assert.True(lst.RemoveFirst());
            Assert.Equal(new[] { 2, 3 }, lst);
        }

        [Fact]
        public void RemoveFirst_Should_ReturnFalse_EmptyList()
        {
            var lst = new CircularDoubleLinkedList<int>();
            Assert.False(lst.RemoveFirst());
        }

        [Fact]
        public void RemoveLast_ShouldBe_True()
        {
            var lst = new CircularDoubleLinkedList<int> { 1, 2, 3 };
            Assert.True(lst.RemoveLast());
            Assert.Equal(new[] { 1, 2 }, lst);
        }

        [Fact]
        public void CopyList_ToNewList_ShoudlBe_True()
        {
            var lst = new CircularDoubleLinkedList<int> { 1, 2, 3, 4, 5 };
            var random = lst.FindFirst(3);

            var result = new CircularDoubleLinkedList<int>();
            var current = random;
            do
            {
                result.AddFirst(current.content);
                current = current.Prev;
            }
            while (current != lst.First.Prev);
            Assert.Equal(new[] { 1, 2, 3 }, result);
        }

        [Fact]
        public void RemoveLast_Should_ReturnFalse_EmptyList()
        {
            var lst = new CircularDoubleLinkedList<int> { };
            Assert.False(lst.RemoveLast());
        }

        [Fact]
        public void DeleteMidNode_ShouldBe_True()
        {
            CircularDoubleLinkedList<string> lst = CircularDoubleLinkedListFactory();
            Assert.True(lst.RemoveNode(lst.FindFirst("7")));            
            Assert.True(lst[6] == "8");
        }

        [Fact]
        public void CopyToArray_ShouldBe_True()
        {
            CircularDoubleLinkedList<string> lst = CircularDoubleLinkedListFactory();
            string[] arr = new string[10];
            lst.CopyTo(arr, 1);
            Assert.Equal(new [] { null, "1", "2", "3", "4", "5", "6", "7", "8", "9" }, arr);
        }

        [Fact]
        public void Should_NotCopy_EmptyList()
        {
            CircularDoubleLinkedList<string> lst = new CircularDoubleLinkedList<string>();
            string[] arr = new string[1] { "initial" };
            lst.CopyTo(arr, 0);
            Assert.Equal(new string[] { "initial" }, arr);
        }

        [Fact]
        public void NoCopyToNullArray_ShouldBe_True()
        {
            CircularDoubleLinkedList<string> lst = CircularDoubleLinkedListFactory();
            string[] arr = null;
            var exception = Record.Exception (() => lst.CopyTo(arr, 1));
            Assert.IsType(typeof(ArgumentNullException), exception);
            Assert.True(exception.Message == "Destination array is null.");
        }

        [Fact]
        public void NoCopyTo_InvalidIndexOfArray_ShouldBe_True()
        {
            CircularDoubleLinkedList<string> lst = CircularDoubleLinkedListFactory();
            string[] arr = new string[3];                     
            var exception = Record.Exception(() => lst.CopyTo(arr, 3));
            Assert.IsType(typeof(ArgumentOutOfRangeException), exception);
            Assert.True(exception.Message == "Invalid index.");
        }

        [Fact]
        public void NoCopyTo_InsufficientElementsArray_ShouldBe_True()
        {
            CircularDoubleLinkedList<string> lst = CircularDoubleLinkedListFactory();
            string[] arr = new string[9];
            var exception = Record.Exception(() => lst.CopyTo(arr, 3));
            Assert.IsType(typeof(ArgumentException), exception);
            Assert.True(exception.Message == "Not enough elements after index in the destination array.");
        }
    }
}
