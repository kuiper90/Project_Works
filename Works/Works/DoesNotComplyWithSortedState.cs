using System;

namespace Works
{
    public class DoesNotComplyWithSortedState : Exception
    {
        public DoesNotComplyWithSortedState(string message) : base(message) { }

        //public void ThrowDoesNotComplyWithSortedStateException()
        //{
        //    throw new DoesNotComplyWithSortedState("Element is null or empty.");
        //}
    }
}
