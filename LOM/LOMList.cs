using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LOM
{
    public class LOMList<T> : IList<T>, IEnumerator<T>, IEnumerable<T>
    {
        private readonly Guid _objectID;
        public LOMList()
        {
            this._objectID = Guid.NewGuid();
        }

        ~LOMList()
        {
            ListManager.Destroy(this._objectID);
        }

        internal LOMList(Guid objectID)
        {
            this._objectID = objectID;
        }

        public T this[int index]
        {
            get => (T)ListManager.GetAtIndex(this._objectID, index);
            set => ListManager.SetAtIndex(this._objectID, index, value);
        }

        public int Count => ListManager.Count(this._objectID);
        public int Length => this.Count;
        public bool IsReadOnly { get => false; }

        public void Add(T item)
        {
            ListManager.Add(this._objectID, item);
        }

        public void Clear()
        {
            ListManager.Clear(this._objectID);
        }

        public bool Contains(T item)
        {
            return ListManager.Contains(this._objectID, item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            var arrayValues = ListManager.CopyTo(this._objectID);
            var index = -1;
            for (int i = arrayIndex; i < array.Length; i++)
            {
                index++;
                if (i >= array.Length || index >= arrayValues.Length) break;
                array[i] = (T)arrayValues[index];
            }
        }

        public void CopyTo(T[] array)
        {

            var arrayValues = ListManager.CopyTo(this._objectID);
            for (int i = 0; i < array.Length; i++)
            {
                if (i >= array.Length || i >= arrayValues.Length) break;
                array[i] = (T)arrayValues[i];
            }
        }

        public LOMList<T> Clone()
        {
            Guid newobjectID = ListManager.Clone(_objectID);
            return new LOMList<T>(newobjectID);
        }

        public int IndexOf(T item)
        {
            return ListManager.IndexOf(this._objectID, item);
        }

        public void Insert(int index, T item)
        {
            ListManager.Insert(this._objectID, index, item);
        }


        public bool Remove(T item)
        {
            return ListManager.Remove(this._objectID, item);
        }

        public void RemoveAt(int index)
        {
            ListManager.RemoveAt(this._objectID, index);
        }

        //Implements IEnumerator
        public IEnumerator<T> GetEnumerator()
        {
            return (IEnumerator<T>)this;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator<T>)this;
        }
        private int position = -1;
        public T Current
        {
            get { return (T)this[position]; }
        }
        object IEnumerator.Current
        {
            get { return (object)this[position]; }
        }

        public bool MoveNext()
        {
            position++;
            bool seguir = position < this.Length;
            if (!seguir) this.Reset();
            return (seguir);
        }
        public void Reset()
        {
            position = -1;
        }

        public void Dispose()
        {
            ListManager.Dispose(this._objectID);
        }

        public void Destroy()
        {
            ListManager.Destroy(this._objectID);
        }
    }

}
