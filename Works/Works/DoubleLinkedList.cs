using System;
using System.Collections;
using System.Collections.Generic;

namespace Works
{
    public class DoubleLinkedList<T> : ICollection<T>, IReadOnlyCollection<T>
    {
        DoubleLinkedListNode<T> _head = null;
        DoubleLinkedListNode<T> _tail = null;
        int size = 0;
        readonly IEqualityComparer<T> comparer;

        public DoubleLinkedListNode<T> head
        {
            get => _head;
            set => this._head = value;
        }

        public DoubleLinkedListNode<T> tail
        {
            get => _tail;
            set => this._tail = value;
        }        

        public DoubleLinkedList() { }

        public bool IsReadOnly { get; set; }

        public int Count { get => size; }

        void ICollection<T>.Add(T content)
        {
            this.AddLast(content);
        }

        protected bool IsValidIndex(int index) => 0 <= index && index < this.Count;

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
                DoubleLinkedListNode<T> current = this.head;
                for (int i = 0; i < index; i++)
                    current = current.next;
                return current.content;
            }
            set
            {
                CheckIfValidIndex(index);
                DoubleLinkedListNode<T> current = this.head;
                for (int i = 0; i < index; i++)
                    current = current.next;
                current.content = value;
            }
        }

        public DoubleLinkedList(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(null, "Collection is null.");
            }
            foreach (T item in collection)
            {
                AddLast(item);
            }
        }

        protected void CheckArrayCount(T[] array, int arrayIndex)
        {
            if (array.Length - arrayIndex < Count)
                throw new ArgumentException("Not enough elements after index in the destination array.");
        }

        private void CheckIfValidArrayIndex(T[] array, int arrayIndex)
        {
            if (0 > arrayIndex || arrayIndex >= array.Length)
                throw new ArgumentOutOfRangeException(null, "Invalid index.");
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            CheckArrayCount(array);
            CheckIfValidArrayIndex(array, arrayIndex);
            CheckArrayCount(array, arrayIndex);
            DoubleLinkedListNode<T> node = this.head;

            if (node != null)
            {
                do
                {
                    array[arrayIndex++] = node.content;
                    node = node.next;
                } while (node.next != null);
            }
        }

        private static void CheckArrayCount(T[] array)
        {
            if (array == null)
                throw new ArgumentNullException(null, "Destination array is null.");
        }

        private void ValidateNode(DoubleLinkedListNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(null, "Node is null.");
            if (node.list != this)
                throw new InvalidCastException("External linked list.");
            if (HasCycle(head))
                throw new Exception("This node belongs to a cycle.");
        }

        public DoubleLinkedListNode<T> AddFirst(T content)
        {
            DoubleLinkedListNode<T> node = new DoubleLinkedListNode<T>(this, content);

            if (head == null)
                this.AddFirstNode(node);
            else
            {
                this.InsertNodeBefore(head, node);
                head = node;
            }            
            return node;
        }

        public void AddFirst(DoubleLinkedListNode<T> node)
        {
            ValidateNode(node);
            if (head == null)            
                AddFirstNode(node);            
            else
            {
                InsertNodeBefore(head, node);
                head = node;            
            }
            node.list = this;
        }

        private void AddFirstNode(DoubleLinkedListNode<T> node)
        {
            node.next = null;
            node.prev = null;
            head = node;
            tail = head;
            size++;
        }

        private void InsertNodeBefore(DoubleLinkedListNode<T> node, DoubleLinkedListNode<T> newNode)
        {
            newNode.next = node;
            newNode.prev = node.prev;
            if (node.prev != null)
                node.prev.next = newNode;
            node.prev = newNode;
            if (node == head)
                head = newNode;
            size++;
        }

        private void InsertNodeAfter(DoubleLinkedListNode<T> node, DoubleLinkedListNode<T> newNode)
        {
            newNode.prev = node;
            newNode.next = node.next;
            if (node.next != null)
                node.next.prev = newNode;
            node.next = newNode;
            if (node == tail)
                tail = newNode;
            size++;
        }

        public DoubleLinkedListNode<T> AddLast(T content)
        {
            DoubleLinkedListNode<T> node = new DoubleLinkedListNode<T>(this, content);

            if (head == null)
                this.AddFirstNode(node);
            else
                this.InsertNodeAfter(tail, node);
            return node;
        }

        public void AddLast(DoubleLinkedListNode<T> node)
        {
            ValidateNode(node);
            if (head == null)
                AddFirstNode(node);
            else
                InsertNodeAfter(tail, node);
            node.list = this;
        }

        public void AddBefore(DoubleLinkedListNode<T> node, DoubleLinkedListNode<T> newNode)
        {
            ValidateNode(node);
            ValidateNode(newNode);
            InsertNodeBefore(node, newNode);
            newNode.list = this;
            if (node == head)
                head = newNode;
        }

        public DoubleLinkedListNode<T> AddBefore(DoubleLinkedListNode<T> node, T content)
        {
            ValidateNode(node);
            DoubleLinkedListNode<T> newNode = new DoubleLinkedListNode<T>(node.list, content);

            InsertNodeBefore(node, newNode);
            if (node == head)           
                head = newNode;            
            return newNode;
        }

        public void AddAfter(DoubleLinkedListNode<T> node, DoubleLinkedListNode<T> newNode)
        {
            ValidateNode(node);
            ValidateNode(newNode);
            InsertNodeAfter(node, newNode);
            newNode.list = this;
        }

        public DoubleLinkedListNode<T> AddAfter(DoubleLinkedListNode<T> node, T content)
        {
            ValidateNode(node);
            DoubleLinkedListNode<T> newNode = new DoubleLinkedListNode<T>(node.list, content);

            InsertNodeBefore(node.next, newNode);
            return newNode;
        }

        public DoubleLinkedListNode<T> Find(T content)
        {
            DoubleLinkedListNode<T> node = head;
            EqualityComparer<T> comp = EqualityComparer<T>.Default;
            if (node != null)
            {
                if (content != null)
                {
                    do
                    {
                        if (comp.Equals(node.content, content))
                            return node;
                        node = node.next;
                    } while (node.next != null);
                }
                else
                {
                    do
                    {
                        if (node.content == null)
                            return node;
                        node = node.next;
                    } while (node.next != null);
                }
            }
            return null;
        }

        public DoubleLinkedListNode<T> FindLast(T content)
        {
            if (head == null)
                return null;            
            DoubleLinkedListNode<T> node = tail;

            if (node != null)
            {
                if (content != null)
                {
                    do
                    {
                        if (node.content.Equals(content))
                            return node;
                        node = node.prev;
                    } while (node.next != null);
                }
                else
                {
                    do
                    {
                        if (node.content == null)
                            return node;
                        node = node.prev;
                    } while (node.next != null);
                }
            }
            return null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            DoubleLinkedListNode<T> current = head;

            if (current != null)
            {
                do
                {
                    yield return current.content;
                    current = current.next;
                } while (current.next != null);
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Contains(T content) => Find(content) != null;        

        public void Clear()
        {
            CheckIfHeadIsNull();
            DoubleLinkedListNode<T> current = head;
            while (current != null)
            {
                DoubleLinkedListNode<T> tmp = current;
                current = current.next;
                tmp.Invalidate();
            }
            head = null;
            size = 0;
        }

        private void CheckIfEmptyList(DoubleLinkedListNode<T> node)
        {
            if (head == null)
                throw new ArgumentNullException(null, "List is empty.");
        }

        public void RemoveNode(DoubleLinkedListNode<T> node)
        {
            CheckIfEmptyList(node);
            ValidateNode(node);

            if (node.prev == null)
                head = node.next;
            else if (node.next == null)
            {
                node.prev.next = node.next;
                tail = node.prev;
            }
            else
            {
                node.next.prev = node.prev;
                node.prev.next = node.next;
            }
            node.Invalidate();
            size--;
        }

        private void CheckIfHeadIsNull()
        {
            if (head == null)
                throw new ArgumentNullException(null, "Head is null.");
        }

        public void Remove(DoubleLinkedListNode<T> node)
        {
            ValidateNode(node);
            this.RemoveNode(node);
        }

        public bool Remove(T content)
        {
            DoubleLinkedListNode<T> node = this.Find(content);

            if (node != null)
            {
                RemoveNode(node);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            CheckIfValidIndex(index);
            int currentIndex = 0;
            DoubleLinkedListNode<T> current = head;           

            while (currentIndex < index)
            {
                current = current.next;
                currentIndex++;
            }
            RemoveNode(current);
        }

        public void RemoveFirst(DoubleLinkedListNode<T> node)
        {
            CheckIfHeadIsNull();
            this.RemoveNode(head);
        }

        public void RemoveLast()
        {
            CheckIfHeadIsNull();
            this.RemoveNode(tail);
        }

        public bool HasCycle(DoubleLinkedListNode<T> head)
        {
            if (head == null)
                return false;
            DoubleLinkedListNode<T> one = head.next;
            DoubleLinkedListNode<T> two = head;

            while (one != null && one.next != null && two != null)
            {
                if (one == two)
                    return true;
                one = one.next.next;
                two = two.next;
            }
            return false;
        }
    }
}
