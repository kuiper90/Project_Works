using System;
using System.Collections.Generic;
using System.Text;

namespace Works
{
    public class DoubleLinkedListNode<T>
    {
        DoubleLinkedList<T> _list;
        DoubleLinkedListNode<T> _next;
        DoubleLinkedListNode<T> _prev;
        T _content;

        internal DoubleLinkedListNode(T value)
        {
            this._content = value;
        }

        internal DoubleLinkedListNode(DoubleLinkedList<T> list, T value)
        {
            this._list = list;
            this._content = value;
        }

        public DoubleLinkedList<T> list
        {
            get => _list;
            set => this._list = value;
        }

        public DoubleLinkedListNode<T> next
        {           
            get => _next;
            set => this._next = value;
        }

        public DoubleLinkedListNode<T> prev
        {      
            get => _prev;
            set => this._prev = value;
        }

        public T content
        {
            get => _content;
            set => _content = value;
        }

        public void Invalidate()
        {
            _list = null;
            next = null;
            prev = null;
        }
    }
}
