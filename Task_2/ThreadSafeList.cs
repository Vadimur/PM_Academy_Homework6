using System.Collections.Generic;

namespace Task_2
{
    public class ThreadSafeList<T>
    {
        private List<T> _innerList;

        public ThreadSafeList()
        {
            _innerList = new List<T>();
        }

        public void Add(T element)
        {
            lock (_innerList)
            {
                _innerList.Add(element);
            }
        }

        public void AddWithoutDuplicate(T element)
        {
            lock (_innerList)
            {
                if (!Contains(element))
                    Add(element);
            }
        }

        public void AddRange(IEnumerable<T> elements)
        {
            lock (_innerList)
            {
                _innerList.AddRange(elements);
            }
        }

        public bool Contains(T element)
        {
            lock (_innerList)
            {
                return _innerList.Contains(element);
            }
        }

        public T[] GetElements()
        {
            lock (_innerList)
            {
                return _innerList.ToArray();
            }
        }
        
    }
}