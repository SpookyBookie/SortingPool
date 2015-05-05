using System;
using System.Collections.Generic;

namespace SortingPool
{
    class ExtendedQueue<T>
    {
        private int _num;
        private int _rear;
        private int _front;
        private T[] _array;
        private int _maxSize;
        

        public ExtendedQueue()
        {
            _num = 0;
            _rear = -1;
            _front = 0;
            _maxSize = 1000;
            _array = new T[1000];
        }

        public void Insert(T value)
        {
            try
            {
                if (!IsFull())
                {
                    if (_rear == _maxSize - 1)
                    {
                        _rear = -1;
                    }

                    _array[++_rear] = value;
                    _num++;
                }

                else 
                {
                    throw new IndexOutOfRangeException();
                }
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        public T Remove()
        {
            try
            {
                if (!IsEmpty())
                {
                    T _temp = _array[_front++];

                    if (_front == _maxSize)
                    {
                        _front = 0;
                    }

                    _num--;

                    return _temp;
                }

                else
                {
                    throw new IndexOutOfRangeException();
                }
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        public bool IsEmpty()
        {
            return (_num == 0);
        }

        public bool IsFull()
        {
            return (_num == _maxSize);
        }
    }
}
