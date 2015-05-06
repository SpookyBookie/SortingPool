using System.Threading;

namespace SortingPool
{
    class OddEven
    {
        private int _temp;
        private int _rear;
        private int _front;
        private int[] _array;
        private bool _cheker;
        private ParallelOddEvenSort _instance;
        

        public OddEven(int[] array, int front, int rear, ParallelOddEvenSort instance)
        {
            _rear = rear;
            _front = front;
            _array = array;
            _cheker = false;
            _instance = instance;
        }


        public void Sort()
        {
            while (!_cheker)
            {
                _cheker = true;

                for (int i = _front; i < _rear; i += 2)
                {
                    if (_array[i] > _array[i + 1])
                    {
                        _temp = _array[i];
                        _array[i] = _array[i + 1];
                        _array[i + 1] = _temp;

                        _cheker = false;
                    }
                }

                for (int i = _front + 1; i < _rear; i += 2)
                {
                    if (_array[i] > _array[i + 1])
                    {
                        _temp = _array[i];
                        _array[i] = _array[i + 1];
                        _array[i + 1] = _temp;

                        _cheker = false;
                    }
                }
            }

            Interlocked.Decrement(ref _instance._iterator);
        }
    }
}
