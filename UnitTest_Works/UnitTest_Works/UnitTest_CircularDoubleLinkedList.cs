using System;
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
        public void ShouldBe_True_AddLastNode()
        {
            CircularDoubleLinkedList<string> lst = CircularDoubleLinkedListFactory();
            lst.AddLast("11");
            Assert.True(lst[9] == "11");
        }

        [Fact]
        public void ShouldBe_True_AddFirstNode()
        {
            CircularDoubleLinkedList<string> lst = CircularDoubleLinkedListFactory();
            lst.AddFirst("11");
            Assert.True(lst[0] == "11");
        }

        [Fact]
        public void ShouldBe_True_ContainsValue()
        {
            CircularDoubleLinkedList<string> lst = CircularDoubleLinkedListFactory();
            Assert.True(lst.Contains("5"));
        }

        [Fact]
        public void ShouldBe_True_DeleteLastNode()
        {
            CircularDoubleLinkedList<string> lst = CircularDoubleLinkedListFactory();
            lst.RemoveLast();
            Assert.True(lst[7] == "8");
            Assert.False(lst[8] == "9");
        }

        [Fact]
        public void ShouldBe_True_DeleteFirstNode()
        {
            CircularDoubleLinkedList<string> lst = CircularDoubleLinkedListFactory();
            CircularLinkedListNode<string> current = lst.FindFirst("5");
            lst.RemoveFirst(current);
            Assert.True(lst[6] == "8");
        }

        [Fact]
        public void ShouldBe_True_DeleteMidNode()
        {
            CircularDoubleLinkedList<string> lst = CircularDoubleLinkedListFactory();
            Assert.True(lst.RemoveNode(lst.FindFirst("7")));            
            Assert.True(lst[6] == "8");
        }

        [Fact]
        public void ShouldBe_True_AddAfter_Node()
        {
            CircularDoubleLinkedList<string> lst = CircularDoubleLinkedListFactory();
            CircularLinkedListNode<string> current = lst.FindFirst("3");
            lst.AddAfter(current, "-11");
            Assert.True(lst[2] == "3");
            Assert.True(lst[3] == "-11");
        }

        [Fact]
        public void ShouldBe_True_AddAfterNode()
        {
            CircularDoubleLinkedList<string> lst = CircularDoubleLinkedListFactory();
            CircularLinkedListNode<string> current = lst.FindFirst("3");
            lst.AddAfter(current, "-7");
            Assert.True(lst[3] == "-7");
            Assert.True(lst[2] == "3");
        }

        [Fact]
        public void ShouldBe_True_AddBefore_Node()
        {
            CircularDoubleLinkedList<string> lst = CircularDoubleLinkedListFactory();
            CircularLinkedListNode<string> current = lst.FindFirst("2");
            lst.AddBefore(current, "-12");
            Assert.True(lst[1] == "-12");
            Assert.True(lst[2] == "2");
        }

        [Fact]
        public void ShouldBe_True_AddBeforeNode()
        {
            CircularDoubleLinkedList<string> lst = CircularDoubleLinkedListFactory();
            CircularLinkedListNode<string> current = lst.FindLast("5");
            lst.AddBefore(current, "-10");
            Assert.True(lst[4] == "-10");
            Assert.True(lst[5] == "5");
        }

        [Fact]
        public void ShouldBe_True_NoCopyToNullArray()
        {
            CircularDoubleLinkedList<string> lst = CircularDoubleLinkedListFactory();
            string[] arr = new string[11];
            lst.CopyTo(arr, 1);
            Assert.True(arr[2] == "2");
        }
    }
}
