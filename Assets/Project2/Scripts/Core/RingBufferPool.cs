using System;

namespace GameGuruCase.Project2.Core
{
    /// <summary>
    /// A simple ring buffer that cycles through a pool of items.
    /// </summary>
    /// <typeparam name="T">Type of the pooled items.</typeparam>
    public class RingBufferPool<T>
    {
        private readonly T[] _pool;
        private readonly int _poolSize;
        private int _currentIndex = 0;
        private readonly Func<T> _createFunc;

        public RingBufferPool(int poolSize, Func<T> createFunc = null, T[] pool = null)
        {
            _poolSize = poolSize;
            _createFunc = createFunc;
            if (pool != null)
            {
                _pool = pool;
            }
            else
            {
                _pool = new T[poolSize];
                for (int i = 0; i < poolSize; i++)
                {
                    _pool[i] = createFunc();
                }
            }
        }

        /// <summary>
        /// Returns the next pooled item, cycling through the buffer.
        /// </summary>
        public T GetNext()
        {
            T item = _pool[_currentIndex];
            _currentIndex = (_currentIndex + 1) % _poolSize;
            return item;
        }

        /// <summary>
        /// Returns the entire pool array.
        /// </summary>
        public T[] GetAll()
        {
            return _pool;
        }
    }
}