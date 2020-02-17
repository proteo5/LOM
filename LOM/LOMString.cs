using System;
using System.Collections.Generic;
using System.Text;

namespace LOM
{
    public class LOMString
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
    }
}
