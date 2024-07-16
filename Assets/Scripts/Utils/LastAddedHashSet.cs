using System.Collections.Generic;

namespace Assets.Scripts.Utils
{
    public class LastAddedHashSet<T> : HashSet<T>
    {
        private T _lastAdded;
        public T LastAdded => _lastAdded;

        public new bool Add(T item)
        {
            bool added = base.Add(item);
            if (added)
            {
                _lastAdded = item;
            }
            return added;
        }

    }
}