using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LOM
{
    public class LOMString : IList<string>, ICollection<string>, IEnumerator<string>, IEnumerable<string>
    {
        private readonly Guid _stringID;
        private readonly string _separator;

        internal LOMString(Guid stringID, string separator)
        {
            this._separator = separator;
            this._stringID = stringID;
        }

        public LOMString(string value, string separator = "")
        {
            this._stringID = Guid.NewGuid();
            this._separator = separator;
            StringManager.AddNewString(this._stringID, value, separator);
        }

        public string this[int index]
        {
            get => StringManager.GetAtIndex(this._stringID, index);
            set => StringManager.SetAtIndex(this._stringID, index, value);
        }

        public override string ToString()
        {
            return StringManager.ToString(this._stringID, this._separator);
        }

        public LOMString Clone()
        {
            var newStringID = StringManager.Clone(this._stringID);

            return new LOMString(newStringID, _separator);
        }

        public void Add(string value)
        {
            StringManager.Add(this._stringID, value);
        }

        public void Insert(int index, string value)
        {
            StringManager.Insert(this._stringID, index, value);
        }

        public bool Remove(string value)
        {
            return StringManager.Remove(this._stringID, value);
        }

        public void RemoveAt(int index)
        {
            StringManager.RemoveAt(this._stringID, index);
        }

        public int IndexOf(string value)
        {
            return StringManager.IndexOf(this._stringID, value);
        }

        public bool Contains(string value)
        {
            return StringManager.Contains(this._stringID, value);
        }

        public int Count => StringManager.Count(this._stringID);
        public int Length => this.Count;

        public bool IsReadOnly { get => false; }

        public void Clear()
        {
            StringManager.Clear(this._stringID);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            var arrayValues = StringManager.CopyTo(this._stringID);
            var index = -1;
            for (int i = arrayIndex; i < array.Length; i++)
            {
                index++;
                if (i >= array.Length || index >= arrayValues.Length) break;
                array[i] = arrayValues[index];
            }
        }

        public void CopyTo(string[] array)
        {

            var arrayValues = StringManager.CopyTo(this._stringID);
            for (int i = 0; i < array.Length; i++)
            {
                if (i >= array.Length || i >= arrayValues.Length) break;
                array[i] = arrayValues[i];
            }
        }

        //Implements IEnumerator
        public IEnumerator<string> GetEnumerator()
        {
            return (IEnumerator<string>)this;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator<string>)this;
        }

        private int position = -1;

        public string Current
        {
            get { return this[position]; }
        }
        object IEnumerator.Current
        {
            get { return this[position]; }
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
            StringManager.Dispose(this._stringID);
        }
    }
}
