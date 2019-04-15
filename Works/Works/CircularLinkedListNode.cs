using System;
using System.Collections.Generic;

namespace Works
{
    public class CircularLinkedListNode<T>
    {
        CircularLinkedListNode<T> _next;
        CircularLinkedListNode<T> _prev;
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
       
        public T content
        {
            get => this._content;
            set => this._content = value;
        }
       
        internal CircularLinkedListNode(T item)
        {
            this.content = item;
        }
    }
}
