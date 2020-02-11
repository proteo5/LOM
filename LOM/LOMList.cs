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
            throw new NotImplementedException("At CopyTo");
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

        //implements IEnumerator
        public IEnumerator<T> GetEnumerator()
        {
            return (IEnumerator<T>)this;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException("At IEnumerator");
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

        }
    }

}
