using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFLeastResistancePath.Utilities
{
    /// <summary>
    /// Common return class that can be used to return anything inside ReturnValue object including error details.
    /// </summary>
    public class ReturnResult
    {
        public ReturnResult()
        {
            HasErrors = false;
        }
        public object ReturnValue;
        public bool HasErrors;
        public string ErrorMessage;
    }
}