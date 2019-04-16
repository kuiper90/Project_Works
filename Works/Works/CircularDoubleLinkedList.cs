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
            if (comparer == null)
                throw new ArgumentNullException("Comparer is null.");
            this.comparer = comparer;
            if (collection != null)
            {
                foreach (T item in collection)
                    this.AddLast(item);
            }
        }

        public int Count { get => size; }

        bool ICollection<T>.IsReadOnly { get => false; }

        public CircularLinkedListNode<T> First { get => head; }

        public CircularLinkedListNode<T> Last
        {
            get => head.prev;
            set => head.prev = value;
        }

        void ICollection<T>.Add(T content)
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
                CircularLinkedListNode<T> node = First.next;
                for (int i = 0; i < index; i++)
                    node = node.next;
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
            newNode.next = node;
            newNode.prev = node.prev;
            node.prev.next = newNode;
            node.prev = newNode;
            size++;
        }

        public void AddLast(CircularLinkedListNode<T> node)
        {
            ValidateNode(node);
            AddNode(head, node);
            node.list = this;
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
            AddNode(head.next, node);
            node.list = this;
        }

        public CircularLinkedListNode<T> AddFirst(T content)
        {
            CircularLinkedListNode<T> node = new CircularLinkedListNode<T>(content, this);
            AddNode(head.next, node);
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
            newNode.list = this;
        }

        public CircularLinkedListNode<T> AddAfter(CircularLinkedListNode<T> node, T content)
        {
            ValidateNode(node);
            CircularLinkedListNode<T> newNode = new CircularLinkedListNode<T>(content, node.list);            
            AddNode(node.next, newNode);
            return newNode;
        }

        public void AddAfter(CircularLinkedListNode<T> node, CircularLinkedListNode<T> newNode)
        {
            ValidateNode(node);            
            ValidateNode(newNode);
            AddNode(node.next, newNode);
            newNode.list = this;
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
            CircularLinkedListNode<T> one = head.next;
            CircularLinkedListNode<T> two = head;

            while (one != null && one.next != null && two != null)
            {
                if (one == two)
                    return true;
                one = one.next.next;
                two = two.next;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(null, "Destination array is null.");
            CheckIfValidArrayIndex(array, arrayIndex);
            CheckArrayCount(array, arrayIndex);
            CircularLinkedListNode<T> node = First.next;

            if (node != null)
                do
                {
                    array[arrayIndex++] = node.content;
                    node = node.next;
                } while (node != First);
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
            CircularLinkedListNode<T> current = head;

            if (current != null)
            {
                do
                {
                    yield return current.content;
                    current = current.next;
                } while (current != head);
            }
        }

        public IEnumerator<T> GetReverseEnumerator()
        {
            CircularLinkedListNode<T> current = Last;

            if (current != null)
            {
                do
                {
                    yield return current.content;
                    current = current.prev;
                } while (current != Last);
            }
        }

        public IEnumerator<T> GetGenericEnumerator(CircularLinkedListNode<T> node)
        {
            CircularLinkedListNode<T> current = node;

            if (current != null)
            {
                do
                {
                    yield return current.content;
                    current = current.next;
                } while (current != node);
            }
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
            CircularLinkedListNode<T> result = null;

            if (comparer.Equals(node.content, contentToCompare))
                result = node;
            else if (result == null)
                { if (node.next != head || node.prev != Last)
                    result = FindNode(node.next, contentToCompare);
            }
            return result;
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
            CircularLinkedListNode<T> prevNode = node.prev;
            prevNode.next = node.next;
            node.next.prev = node.prev;

            if (head == node)
                head = node.next;
            else if (Last == node)
                Last = node.prev;
            size--;
            return true;
        }

        public bool RemoveFirst(CircularLinkedListNode<T> node)
            => this.RemoveNode(head);

        public bool RemoveLast() => this.RemoveNode(Last);

        public void RemoveAll(T content)
        {
            do
            {
                this.Remove(content);
            } while (true);
        }
    }
}
