using System;
using UnityEngine;

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

    public T GetNext()
    {
        T item = _pool[_currentIndex];
        _currentIndex = (_currentIndex + 1) % _poolSize;
        return item;
    }


}