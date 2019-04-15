using System;
using Works;
using Xunit;

namespace UnitTest_Works
{
    public class UnitTest_DoubleLinkedList
    {
        public DoubleLinkedList<string> DoubleLinkedListFactory()
        {
            DoubleLinkedList<string> lst = new DoubleLinkedList<string>();
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
            DoubleLinkedList<string> lst = DoubleLinkedListFactory();
            DoubleLinkedListNode<string> node = 
            lst.AddLast("11");
            Assert.True(lst[9] == "11");
        }

        [Fact]
        public void ShouldBe_True_AddLastEmpty()
        {
            DoubleLinkedList<string> lst = DoubleLinkedListFactory();
            lst.AddLast("");
            Assert.True(lst[8] == "9");
            Assert.True(lst[9] == "");
        }

        [Fact]
        public void ShouldBe_True_AddLastNull()
        {
            DoubleLinkedList<string> lst = DoubleLinkedListFactory();
            DoubleLinkedListNode<string> node = null;
            Action act = () => lst.AddLast(node);
            var exception = Assert.Throws<ArgumentNullException>(act);
            Assert.True(exception.Message == "Node is null.");            
        }

        [Fact]
        public void ShouldBe_True_AddFirstNode()
        {
            DoubleLinkedList<string> lst = DoubleLinkedListFactory();
            lst.AddFirst("-1");
            Assert.True(lst[0] == "-1");
        }

        [Fact]
        public void ShouldBe_True_DeleteGivenFirstNode()
        {
            DoubleLinkedList<string> lst = DoubleLinkedListFactory();
            lst.RemoveFirst(lst.head);
            Assert.True(lst[0] == "2");
        }

        [Fact]
        public void ShouldBe_True_DeleteGivenLastNode()
        {
            DoubleLinkedList<string> lst = DoubleLinkedListFactory();
            lst.RemoveLast();
            Assert.True(lst[7] == "8");
            var exception = Record.Exception(() => Assert.True(lst[8] == "9"));
            Assert.IsType(typeof(ArgumentOutOfRangeException), exception);
            Assert.True(exception.Message == "Invalid index.");
        }

        [Fact]
        public void ShouldBe_True_DeleteNodeWithGivenContent()
        {
            DoubleLinkedList<string> lst = DoubleLinkedListFactory();
            lst.Remove("5");
            Assert.True(lst[6] == "8");
        }

        [Fact]
        public void ShouldBe_True_DeleteGivenNode()
        {
            DoubleLinkedList<string> lst = DoubleLinkedListFactory();
            lst.RemoveNode(lst.Find("1"));
            Assert.True(lst[0] == "2");
        }

        [Fact]
        public void ShouldBe_True_AddContentBefore()
        {
            DoubleLinkedList<string> lst = DoubleLinkedListFactory();
            DoubleLinkedListNode<string> current = lst.Find("4");
            DoubleLinkedListNode<string> node = lst.AddBefore(current, "-5");
            Assert.True(lst[3] == "-5");
            Assert.True(lst[4] == "4");
        }

        [Fact]
        public void ShouldBe_True_AddNodeBefore()
        {
            DoubleLinkedList<string> lst = DoubleLinkedListFactory();
            DoubleLinkedListNode<string> current = lst.Find("4");
            lst.AddBefore(current, current.next);
            Assert.True(lst[3] == "5");
            Assert.True(lst[4] == "4");
        }

        [Fact]
        public void ShouldBe_True_AddNodeAfter()
        {
            DoubleLinkedList<string> lst = DoubleLinkedListFactory();
            DoubleLinkedListNode<string> current = lst.Find("4");
            lst.AddAfter(current, "5");
            Assert.True(lst[4] == "5");
            Assert.True(lst[5] == "5");
        }

        [Fact]
        public void ShouldBe_True_CopyToArray()
        {
            DoubleLinkedList<string> lst = DoubleLinkedListFactory();
            string[] arr = new string[11];
            lst.CopyTo(arr, 2);
            Assert.True(arr[2] == "1");
        }
    }
}
