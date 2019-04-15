using System;
using Works;
using Xunit;

namespace UnitTest_Works
{
    public class UnitTest_LinkedList
    {
        public LinkedList<string> ListFactory()
        {
            LinkedList<string> lst = new LinkedList<string>();
            string[] input = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            int size = input.Length;

            if (size > 0)
            {
                for (int i = 0; i < size; i++)
                    lst.Append(input[i]);
            }
            return (lst);
        }

        [Fact]
        public void ShouldBe_True_AppenddNode()
        {
            LinkedList<string> lst = ListFactory();
            lst.Append("11");            
            Assert.True(lst[9] == "11");
        }

        [Fact]
        public void ShouldBe_True_AppenddEmptyNode()
        {
            LinkedList<string> lst = ListFactory();
            lst.Append("");
            Assert.True(lst[9] == "");
        }

        [Fact]
        public void ShouldBe_True_AppenddNullNode()
        {
            LinkedList<string> lst = ListFactory();
            lst.Append(null);
            Assert.True(lst[9] == null);
        }

        [Fact]
        public void ShouldBe_True_PrependNode()
        {
            LinkedList<string> lst = ListFactory();
            lst.Prepend("11");
            Assert.True(lst[0] == "11");
        }

        [Fact]
        public void ShouldBe_True_PrependNullNode()
        {
            LinkedList<string> lst = ListFactory();
            lst.Prepend(null);
            Assert.True(lst[0] == null);
        }

        [Fact]
        public void ShouldBe_True_PrependEmptyNode()
        {
            LinkedList<string> lst = ListFactory();
            lst.Prepend("");
            Assert.True(lst[0] == "");
        }

        [Fact]
        public void ShouldBe_True_DeleteGivenFirstNode()
        {
            LinkedList<string> lst = ListFactory();
            lst.DeleteWithValue("1");
            Assert.True(lst[0] == "2");
        }

        [Fact]
        public void ShouldBe_True_DeleteGivenLastNode()
        {
            LinkedList<string> lst = ListFactory();
            lst.DeleteWithValue("9");
            Assert.True(lst[7] == "8");
            Assert.False(lst[8] == "9");
        }

        [Fact]
        public void ShouldBe_True_DeleteGivenMidNode()
        {
            LinkedList<string> lst = ListFactory();
            lst.DeleteWithValue("5");
            Assert.True(lst[6] == "8");
        }

        [Fact]
        public void ShouldBe_True_DeleteDupsFirsNode()
        {
            LinkedList<string> lst = ListFactory();
            lst.Prepend("1");
            lst.DeleteDuplicates(lst.head);
            Assert.True(lst[0] == "1");
        }

        [Fact]
        public void ShouldBe_True_InsertDupNode()
        {
            LinkedList<string> lst = ListFactory();
            Node<string> current = lst.GetElement(lst.head, 4);
            lst.InsertNode("5", current);
            Assert.True(lst[4] == "5");
            Assert.True(lst[5] == "5");
        }

        [Fact]
        public void ShouldBe_True_DeleteDupsMidNode()
        {
            LinkedList<string> lst = ListFactory();
            Node<string> current = lst.GetElement(lst.head, 4);            
            lst.InsertNode("5", current);
            lst.DeleteDuplicates(lst.head);
            Assert.True(lst[5] == "6");
        }

        [Fact]
        public void ShouldBe_True_DeleteDupsLastNode()
        {
            LinkedList<string> lst = ListFactory();
            lst.Append("9");
            lst.DeleteDuplicates(lst.head);
            Assert.True(lst[8] == "9");
        }

        [Fact]
        public void ShouldBe_True_CopyToNewList()
        {
            LinkedList<string> lst = ListFactory();
            Node<string> node = new Node<string>("-1");
            lst.CopyTo(node, 5);
            Assert.True(lst.GetElement(node, 0).content == "-1");
            Assert.True(lst.GetElement(node, 2).content == "7");
        }
    }
}
