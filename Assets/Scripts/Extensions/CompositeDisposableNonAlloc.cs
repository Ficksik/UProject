using System;
using System.Collections;
using System.Collections.Generic;

namespace Extensions
{
    /* NOTE: CompositeDisposableNonAlloc was created in order 
     * not to create CompositeDisposable every time after Dispose.
     * Thereby reducing the memory allocation */
    public class CompositeDisposableNonAlloc: IDisposable,ICollection<IDisposable>
    {
        private List<IDisposable> _disposables;
        
        private List<IDisposable> Disposables => _disposables??= new List<IDisposable>();
        public int Count => Disposables.Count;
        public bool IsReadOnly => false;

        public void Dispose()
        {
            for (int i = 0; i < Disposables.Count; i++)
            {
                Disposables[i].Dispose();
            }
            Disposables.Clear();
        }

        public IEnumerator<IDisposable> GetEnumerator()
        {
            return Disposables.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
           return GetEnumerator();
        }

        public void Add(IDisposable item)
        {
            if(item == null) return;
            Disposables.Add(item);
        }

        public void Clear()
        {
            Disposables.Clear();
        }

        public bool Contains(IDisposable item)
        {
            if (item == null) return true;
            return Disposables.Contains(item);
        }

        public void CopyTo(IDisposable[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (arrayIndex < 0 || arrayIndex >= array.Length)
                throw new ArgumentOutOfRangeException("arrayIndex");

            var disArray = new List<IDisposable>();
            foreach (var item in Disposables)
            {
                if (item != null) disArray.Add(item);
            }

            Array.Copy(disArray.ToArray(), 0, array, arrayIndex, array.Length - arrayIndex);
        }

        public bool Remove(IDisposable item)
        {
            if (item == null) return false;
            if(Contains(item))
            {
                Disposables.Remove(item);
                return true;
            }
            return false;
        }
    }
}
