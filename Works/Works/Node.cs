using System;
using System.Collections.Generic;
using System.Text;

namespace Works
{
    public class Node<T>
    {
        Node<T> _next;
        Node<T> _random;
        T _content;

        public Node<T> next
        {
            get => this._next;
            set => this._next = value;
        }

        public Node<T> random
        {
            get => this._random;
            set => this._random = value;
    }

        public T content
        {
            get => this._content;
            set => this._content = value;
        }

        public Node(T content)
        {
            this.content = content;
        }

        public Node<T> InsertNext(T content)
        {
            Node<T> node = new Node<T>(content);
            if (this.next == null)
            {
                node.next = null;
                this.next = node;
            }
            else
            {
                Node<T> temp = this.next;
                node.next = temp;
                this.next = node;
            }
            return node;
        }
    }
}