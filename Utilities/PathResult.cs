using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFLeastResistancePath.Utilities
{
    /// <summary>
    /// Contains properties required to return as Least Resistance Path Finder result
    /// </summary>
    public class PathResult
    {
        private bool _success;
        private string _successResult;
        private int? _resistance;
        private string _leastPath;
        private bool _hasErrors;
        private string _errorMessage;
        /// <summary>
        /// Constructor setting default properties
        /// </summary>
        public PathResult()
        {
            _success = false;
            _successResult = String.Empty;
            _resistance = int.MinValue;
            _leastPath = String.Empty;
            _hasErrors = false;
            _errorMessage = String.Empty;
        }
        public Boolean Success
        {
            get { return _success; }
            set { _success = value; }
        }
        public String SuccessResult
        {
            get { return _success == true ? "Yes" : "No"; }
        }
        public Int32? Resistance
        {
            get { return _resistance; }
            set { _resistance = value; }
        }
        public String LeastPath
        {
            get { return _leastPath; }
            set { _leastPath = value; }
        }
        public Boolean HasErrors
        {
            get { return _hasErrors; }
            set { _hasErrors = value; }
        }
        public String ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }
    }
}
