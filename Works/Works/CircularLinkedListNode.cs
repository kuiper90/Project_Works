using System;
using System.Collections.Generic;

namespace Works
{
    public class CircularLinkedListNode<T>
    {
        CircularLinkedListNode<T> _next;
        CircularLinkedListNode<T> _prev;
        CircularDoubleLinkedList<T> _list;
        T _content;

        public CircularLinkedListNode<T> next
        {
            get => this._next;
            internal set => this._next = value;
        }
    
        public CircularLinkedListNode<T> prev
        {
            get => this._prev;
            internal set => this._prev = value;
        }

        public CircularDoubleLinkedList<T> list
        {
            get => this._list;
            set => this._list = value;
        }

        public T content
        {
            get => this._content;
            set => this._content = value;
        }

        public CircularLinkedListNode(T content)
        {
            this.content = content;
        }

        public CircularLinkedListNode(T content, CircularDoubleLinkedList<T> _list)
        {
            this.content = content;
            this._list = _list;
        }

        internal CircularLinkedListNode()
        {
            prev = this;
            next = this;
        }
    }
}
