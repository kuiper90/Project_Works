using System;
using System.Collections;
using System.Collections.Generic;

namespace Works
{
    public class CircularDoubleLinkedList<T> : ICollection<T>
    {
        CircularLinkedListNode<T> head = null;
        CircularLinkedListNode<T> tail = null;
        int size = 0;
        readonly IEqualityComparer<T> comparer;

        public CircularDoubleLinkedList() : this(null, EqualityComparer<T>.Default)
        {
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

        public CircularLinkedListNode<T> Head { get => head; }

        public CircularLinkedListNode<T> Tail { get => tail; }

        public int Count { get => size; }

        public bool IsReadOnly { get; set; }

        public CircularLinkedListNode<T> First { get; }

        public CircularLinkedListNode<T> Last { get; }

        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
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
                CircularLinkedListNode<T> node = this.head;
                for (int i = 0; i < index; i++)
                    node = node.next;
                return node.content;
            }
        }

        public void AddFirstNode(T content)
        {
            head = new CircularLinkedListNode<T>(content);
            tail = head;
            head.next = tail;
            head.prev = tail;
        }

        public void AddLast(T content)
        {
            if (head == null)
                this.AddFirstNode(content);
            else
            {
                CircularLinkedListNode<T> node = new CircularLinkedListNode<T>(content);
                tail.next = node;
                node.next = head;
                node.prev = tail;
                tail = node;
                head.prev = tail;
            }
            size++;
        }

        public void AddFirst(T content)
        {
            if (head == null)
                this.AddFirstNode(content);
            else
            {
                CircularLinkedListNode<T> node = new CircularLinkedListNode<T>(content);
                head.prev = node;
                node.prev = tail;
                node.next = head;
                tail.next = node;
                head = node;
            }
            size++;
        }

        public void AddBefore(CircularLinkedListNode<T> node, T content)
        {
            CheckNullNode(node);
            CircularLinkedListNode<T> tmp = this.FindNode(head, node.content);
            if (tmp != node)
                throw new InvalidOperationException("The input node doesn't belong to this list.");

            CircularLinkedListNode<T> newNode = new CircularLinkedListNode<T>(content);
            node.prev.next = newNode;
            newNode.prev = node.prev;
            newNode.next = node;
            node.prev = newNode;
            if (node == head)
                head = newNode;
            size++;
        }

        public void AddAfter(CircularLinkedListNode<T> node, T content)
        {
            CheckNullNode(node);
            CircularLinkedListNode<T> tmp = this.FindNode(head, node.content);
            if (tmp != node)
                throw new InvalidOperationException("Input node doesn't belong to this list.");

            CircularLinkedListNode<T> newNode = new CircularLinkedListNode<T>(content);
            newNode.next = node.next;
            node.next.prev = newNode;
            newNode.prev = node;
            node.next = newNode;
            if (node == tail)
                tail = newNode;
            size++;
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
            CircularLinkedListNode<T> node = this.head;

            if (node != null)
                do
                {
                    array[arrayIndex++] = node.content;
                    node = node.next;
                } while (node != head);
        }

        private void CheckIfValidArrayIndex(T[] array, int arrayIndex)
        {
            if (0 > arrayIndex || arrayIndex > array.Length)
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
            CircularLinkedListNode<T> current = tail;

            if (current != null)
            {
                do
                {
                    yield return current.content;
                    current = current.prev;
                } while (current != tail);
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
                { if (node.next != head || node.prev != tail)
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
            CircularLinkedListNode<T> node = FindNode(tail, content);

            return node;
        }         

        public bool Contains(T item) => FindFirst(item) != null;        

        public void Clear()
        {
            head = null;
            tail = null;
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
            else if (tail == node)
                tail = node.prev;
            size--;
            return true;
        }

        public bool RemoveFirst(CircularLinkedListNode<T> node)
            => this.RemoveNode(head);

        public bool RemoveLast() => this.RemoveNode(tail);

        public void RemoveAll(T content)
        {
            do
            {
                this.Remove(content);
            } while (true);
        }
    }
}
