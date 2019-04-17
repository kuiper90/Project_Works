using System;
using System.Collections.Generic;

namespace Works
{
    public class LinkedList<T>
    {
        Node<T> _head = null;        
        Node<T> current = null;
        int size = 0;

        public Node<T> head
        {
            get => _head;
            set => this._head = value;
        }

        public LinkedList()
        {
        }

        public LinkedList(T value)
        {
            head.content = value;
            head.next = null;
            size++;
        }

        public int Count { get => size; }

        protected bool IsValidIndex(int index) => 0 <= index && index <= size;

        protected void CheckIfValidIndex(int index)
        {
            if (!IsValidIndex(index))
                throw new ArgumentOutOfRangeException(null, "Index out of bounds.");
        }

        public void Append(T content)
        {
            if (head == null)
            {
                head = new Node<T>(content);
                size++;
                return;
            }
            Node<T> current = head;

            while (current.next != null)
            {
                current = current.next;
            }
            current.next = new Node<T>(content);
            size++;
        }

        public void Prepend(T content)
        {
            Node<T> newHead = new Node<T>(content);
            newHead.next = head;
            head = newHead;
            size++;
        }

        public T this[int index]
        {
            get
            {
                CheckIfValidIndex(index);
                current = head;
                for (int i = 0; i < index; i++)
                {
                    if (current.next == null)
                        break;
                    current = current.next;
                }
                return current.content;
            }
        }

        private void ValidateNode(Node<T> node)
        {
            if (node == null)
                throw new ArgumentNullException("Node is null.");            
        }

        public void InsertNode(T content, Node<T> node)
        {
            ValidateNode(node.InsertNext(content));
            size++;            
        }

        public void DeleteWithValue(T data)
        {
            ValidateNode(head);
            if (head.content.Equals(data))
            {
                head = head.next;
                size--;
                return;
            }
            current = head;

            while (current.next != null)
            {
                if (current.next.content.Equals(data))
                {
                    current.next = current.next.next;
                    size--;
                    return;
                }
                current = current.next;
            }
        }

        public void DeleteDuplicates(Node<T> node)
        {
            HashSet<T> set = new HashSet<T>();
            Node<T> prev = null;

            while (node != null)
            {
                if (set.Contains(node.content))
                    prev.next = node.next;
                else
                {
                    set.Add(node.content);
                    prev = node;
                }
                node = node.next;
            }
        }

        public Node<T> Find(T content)
        {
            this.current = this.head;

            if (this.current != null)
            {
                do
                {
                    if (this.current.content.Equals(content))
                        return current;
                    this.current = current.next;
                } while (this.current.next != null);
            }
            else
            {
                do
                {
                    if (this.current.content == null)
                        return this.current;
                    this.current = this.current.next;
                } while (this.current.next != null);
            }
            return null;
        }

        public bool Contains(T content) => this.Find(content) != null;

        public Node<T> FindLast(T content)
        {
            Node<T> last = GetElement(head, this.Count);

            if (this.head == null)
                return null;
            if (this.current != null)
            {
                if (content != null)
                {
                    do
                    {
                        if (this.current.content.Equals(content))
                            return this.current;
                        this.current = this.current.next;
                    } while (this.current.next != last);
                }
                else
                {
                    do
                    {
                        if (this.current.content == null)
                            return this.current;
                        this.current = this.current.next;
                    } while (this.current != last);
                }
            }
            return null;
        }        

        public Node<T> GetElement(Node<T> head, int index)
        {
            int count = 0;
            if (head == null)
                return null;
            while (head != null)
            {
                if (count == index)
                    return head;
                head = head.next;
                count++;
            }
            return null;
        }

        public void Delete()
        {
            ValidateNode(current);
            current.content = current.next.content;
            current.next = current.next.next;
            current = null;
            size--;
        }

        public bool DeleteMidNode(Node<T> node)
        {
            if (head == null)
                return false;
            if (node == null || node.next == null)
                return false;
            if (node.next == null)
                node = null;
            else
            {
                node.content = node.next.content;
                node.next = node.next.next;
                node = null;
            }
            size--;
            return true;
        }

        public bool ContainsElement(Node<T> head, int index)
            => head != null && this.GetElement(head, index) != null;

        public void CopyTo(Node<T> destNode, int srcIndex)
        {
            ValidateNode(head);
            ValidateNode(destNode);
            CheckIfValidIndex(srcIndex);
            Node<T> current = this.GetElement(head, srcIndex);
            
            do
            {
                Node<T> newDestNode = new Node<T>(current.content);
                destNode.next = newDestNode;
                destNode = destNode.next;
                current = current.next;
            } while (current.next != null) ;
        }

        public Node<T> ReverseAndClone(Node<T> node)
        {
            Node<T> head = null;
            while (node != null)
            {
                Node<T> newNode = new Node<T>(node.content);
                newNode.next = head;
                head = newNode;
                node = node.next;
            }
            return head;
        }

        public void DeleteLinkedList()
        {
            this.head.next = null;
            this.head = null;
            size = 0;
        }

        public Node<T> CopyListToList()
        {
            Node<T> current = head;
            Node<T> tmp = current;
            while (current.next != null)
            {
                tmp.content = current.content;
                tmp.next = current.next;
                tmp.random = null;
                current.next = tmp;
                current = current.next;
            }

            Node<T> result = head.next;
            current = head;
            while (current.next != null)
            {
                current.next.random = current.random.next;
                current = current.next.next;
            }

            current = head;
            tmp = head.next;
            while (current.next != null && tmp.next != null)
            {
                current.next = current.next.next;                
                current = current.next;
                if (tmp.next != null)
                {
                    tmp.next = tmp.next.next;
                    tmp = tmp.next;
                }
            }
            return result;
        }
    }
}
