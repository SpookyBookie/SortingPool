using System;
using System.Collections.Generic;

namespace SortingPool
{
    class ParallelOddEvenSort : IDisposable
    {
        public  int _iterator;
        private Array[] _array;
        private int _threadsNum;
        private OddEven[] _oddEven;
        private ExtendedThreadPool _extendedThreadPool;


        public ParallelOddEvenSort(Array[] array, int threadsNum)
        {
            _iterator = 0;
            _array = array;
            _threadsNum = threadsNum;
            _oddEven = new OddEven[threadsNum];
            _extendedThreadPool = new ExtendedThreadPool(threadsNum);
            
        }

        public void Sort()
        {
            for (int i = 0; i < _threadsNum; i++)
            {
                _oddEven[i] = new OddEven((int[])_array[i], 0, _array[i].Length - 1, this);
            }

            for (int i = 0; i < _threadsNum; i++)
            {
                _extendedThreadPool.Insert(_oddEven[i].Sort);
            }

            while (this._iterator != _threadsNum)
            {

            }
        }

        public void Dispose()
        {
            _extendedThreadPool.Dispose();
        }
    }
}