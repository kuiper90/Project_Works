using System;
using System.Collections;
using System.Collections.Generic;

namespace Works
{
    public class CircularDoubleLinkedList<T> : ICollection<T>
    {
        CircularLinkedListNode<T> head = null;
        int size = 0;
        readonly IEqualityComparer<T> comparer;

        public CircularDoubleLinkedList() : this(null, EqualityComparer<T>.Default)
        {
            head = new CircularLinkedListNode<T>();
        }

        public CircularDoubleLinkedList(IEnumerable<T> collection) : this(collection, EqualityComparer<T>.Default)
        {
        }

        public CircularDoubleLinkedList(IEqualityComparer<T> comparer) : this(null, comparer)
        {
        }

        public CircularDoubleLinkedList(IEnumerable<T> collection, IEqualityComparer<T> comparer)
        {
            this.comparer = comparer ?? throw new ArgumentNullException("Comparer is null.");
            if (collection != null)
            {
                foreach (T item in collection)
                    this.AddLast(item);
            }
        }

        public int Count { get => size; }

        bool ICollection<T>.IsReadOnly { get => false; }

        public CircularLinkedListNode<T> First { get => head.Next; }

        public CircularLinkedListNode<T> Last
        {
            get => head.Prev;
            set => head.Prev = value;
        }

        public void Add(T content)
        {
            this.AddLast(content);
        }

        protected bool IsValidIndex(int index) => 0 <= index && index <= this.Count;

        protected void CheckIfValidIndex(int index)
        {
            if (!IsValidIndex(index))
                throw new ArgumentOutOfRangeException(null, "Invalid index.");
        }

        public T this[int index]
        {
            get
            {
                CheckIfValidIndex(index);
                CircularLinkedListNode<T> node = First;
                for (int i = 0; i < index; i++)
                    node = node.Next;
                return node.content;
            }
        }

        internal void ValidateNode(CircularLinkedListNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException("Node is null.");
            if (node.list != this)
                throw new InvalidOperationException("Node belongs to another class.");
        }

        private void AddNode(CircularLinkedListNode<T> node, CircularLinkedListNode<T> newNode)
        {
            newNode.Next = node;
            newNode.Prev = node.Prev;
            node.Prev.Next = newNode;
            node.Prev = newNode;
            node.list = this;
            size++;
        }

        public void AddLast(CircularLinkedListNode<T> node)
        {
            ValidateNode(node);
            AddNode(head, node);
        }

        public CircularLinkedListNode<T> AddLast(T content)
        {
            CircularLinkedListNode<T> newNode = new CircularLinkedListNode<T>(content, this);
            AddNode(head, newNode);
            return newNode;
        }

        public void AddFirst(CircularLinkedListNode<T> node)
        {
            ValidateNode(node);
            AddNode(head.Next, node);
        }

        public CircularLinkedListNode<T> AddFirst(T content)
        {
            CircularLinkedListNode<T> node = new CircularLinkedListNode<T>(content, this);
            AddNode(head.Next, node);
            return node;
        }

        public CircularLinkedListNode<T> AddBefore(CircularLinkedListNode<T> node, T content)
        {
            ValidateNode(node);
            CircularLinkedListNode<T> newNode = new CircularLinkedListNode<T>(content, node.list);
            AddNode(node, newNode);
            return newNode;
        }

        public void AddBefore(CircularLinkedListNode<T> node, CircularLinkedListNode<T> newNode)
        {
            ValidateNode(node);
            ValidateNode(newNode);
            AddNode(node, newNode);
        }

        public CircularLinkedListNode<T> AddAfter(CircularLinkedListNode<T> node, T content)
        {
            ValidateNode(node);
            CircularLinkedListNode<T> newNode = new CircularLinkedListNode<T>(content, node.list);
            AddNode(node.Next, newNode);
            return newNode;
        }

        public void AddAfter(CircularLinkedListNode<T> node, CircularLinkedListNode<T> newNode)
        {
            ValidateNode(node);
            ValidateNode(newNode);
            AddNode(node.Next, newNode);
        }

        private void CheckNullNode(CircularLinkedListNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(null, "Node is null.");
        }

        public bool HasCycle(CircularLinkedListNode<T> head)
        {
            if (head == null)
                return false;
            CircularLinkedListNode<T> one = head.Next;
            CircularLinkedListNode<T> two = head;

            while (one != null && one.Next != null && two != null)
            {
                if (one == two)
                    return true;
                one = one.Next.Next;
                two = two.Next;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(null, "Destination array is null.");
            CheckIfValidArrayIndex(array, arrayIndex);
            CheckArrayCount(array, arrayIndex);
            CircularLinkedListNode<T> node = head.Next;

            while (node != head)
            {
                array[arrayIndex++] = node.content;
                node = node.Next;
            }
        }

        private void CheckIfValidArrayIndex(T[] array, int arrayIndex)
        {
            if (0 > arrayIndex || arrayIndex >= array.Length)
                throw new ArgumentOutOfRangeException(null, "Invalid index.");
        }

        protected void CheckArrayCount(T[] array, int arrayIndex)
        {
            if (array.Length - arrayIndex < Count)
                throw new ArgumentException("Not enough elements after index in the destination array.");
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var current in GetNodeEnumerator(1))
                yield return current.content;
        }

        public IEnumerable<CircularLinkedListNode<T>> GetNodeEnumerator(int tmp)
        {
            if (tmp == 1)
            {
                for (var current = head.Next; current != head; current = current.Next)
                    yield return current;
            }
            else if (tmp == 2)
            {
                for (var current = head.Prev; current != head; current = current.Prev)
                    yield return current;
            }
        }

        public IEnumerator<T> GetReverseEnumerator()
        {
            foreach (var current in GetNodeEnumerator(2))
                yield return current.content;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public CircularLinkedListNode<T> FindFirst(T content)
        {
            CheckIfHeadIsNull();
            CircularLinkedListNode<T> node = FindNode(head, content);

            return node;
        }

        private CircularLinkedListNode<T> FindNode(CircularLinkedListNode<T> node, T contentToCompare)
        {
            foreach (var current in GetNodeEnumerator(1))
                if (comparer.Equals(current.content, contentToCompare))
                    return current;
            return null;
        }

        private void CheckIfHeadIsNull()
        {
            if (head == null)
                throw new ArgumentNullException(null, "Head is null.");
        }

        public CircularLinkedListNode<T> FindLast(T content)
        {
            CheckIfHeadIsNull();
            CircularLinkedListNode<T> node = FindNode(Last, content);

            return node;
        }

        public bool Contains(T item) => FindFirst(item) != null;

        public void Clear()
        {
            head = null;
            Last = null;
            size = 0;
        }

        public bool Remove(T content)
        {
            CircularLinkedListNode<T> node = this.FindFirst(content);

            return (node != null) ? this.RemoveNode(node) : false;
        }

        public bool RemoveNode(CircularLinkedListNode<T> node)
        {
            if (node == head)
                return false;
            node.Prev.Next = node.Next;
            node.Next.Prev = node.Prev;

            size--;
            return true;
        }

        public bool RemoveFirst()
            => this.RemoveNode(First);

        public bool RemoveLast() => this.RemoveNode(Last);

        public void RemoveAll(T content)
        {
            head = new CircularLinkedListNode<T>();
        }
    }
}
