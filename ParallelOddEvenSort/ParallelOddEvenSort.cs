using System;
using System.Collections.Generic;
using System.Threading;

namespace SortingPool
{
    class ParallelOddEvenSort : IDisposable
    {
        public  int _iterator;
        private int _threadsNum;
        private OddEven _oddEven;
        private ExtendedThreadPool _extendedThreadPool;


        public ParallelOddEvenSort(int threadsNum)
        {
            _iterator = 0;
            _threadsNum = threadsNum;
            _extendedThreadPool = new ExtendedThreadPool(threadsNum);
            
        }


        public void Insert(int[] array)
        {
            try
            {
                if (!_extendedThreadPool._extendedQueue.IsFull())
                {
                    _oddEven = new OddEven(array, 0, array.Length - 1, this);
                    _extendedThreadPool.Insert(_oddEven.Sort);
                    Interlocked.Increment(ref this._iterator);
                }
            }

            catch
            {
                throw new Exception();
            }
        }

        public bool IsSorted()
        {
            return _iterator == 0;
        }

        public void Dispose()
        {
            while (!IsSorted())
            {

            }

            _extendedThreadPool.Dispose();
        }
    }
}