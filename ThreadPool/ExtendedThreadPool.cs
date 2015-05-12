using System;
using System.Collections.Generic;
using System.Threading;

namespace SortingPool
{
    public delegate void VoidPoolDelegate();
    
    class ExtendedThreadPool : IDisposable
    {
        Thread[] _threads;
        private object _locker;
        public ExtendedQueue<VoidPoolDelegate> _extendedQueue;


        public ExtendedThreadPool(int size)
        {
            _locker = new object();
            _threads = new Thread[size];
            _extendedQueue = new ExtendedQueue<VoidPoolDelegate>();

            for (int i = 0; i < size; i++)
            {
                _threads[i] = new Thread(new Worker(_extendedQueue, _locker).Work);
                _threads[i].Name = i.ToString();
                _threads[i].Start();
            }
        }

        public void Insert(VoidPoolDelegate value)
        {
            lock (_locker)
            {
                _extendedQueue.Insert(value);
            }
        }

        public void Dispose()
        {
            for (int i = 0; i < _threads.Length; i++)
            {
                _threads[i].Abort();
            }

            _extendedQueue = null;
        }
    }


    class Worker
    {
        private object _locker;
        private VoidPoolDelegate _delegate;
        private ExtendedQueue<VoidPoolDelegate> _extendedQueue;


        public Worker(ExtendedQueue<VoidPoolDelegate> poolDelegate, object locker)
        {
            _delegate = null;
            _locker = locker;
            _extendedQueue = poolDelegate;
        }


        public void Work()
        {
            try
            {
               while (true)
                {
                    lock (_locker)
                    {
                        if (!_extendedQueue.IsEmpty())
                        {
                            _delegate = _extendedQueue.Remove();
                        }
                    }

                    if (_delegate != null)
                    {
                        _delegate();
                        _delegate = null;
                    }
                }
            }

            catch (ThreadAbortException)
            {

            }
        }
    }
}
