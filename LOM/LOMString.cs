using System;
using System.Collections.Generic;
using System.Text;

namespace LOM
{
    public class LOMString
    {
        private readonly Guid _objectID;
        private readonly string _separator;

        internal LOMString() {
            this._objectID = Guid.NewGuid();
        }

        public LOMString(string value,string separator = " ")
        {
            this._objectID = Guid.NewGuid();
            this._separator = separator;
            StringManager.AddNewString( this._objectID, value,separator);
        }

        public override string ToString() {
            return StringManager.ToString(this._objectID,  this._separator);
        }
    }
}
