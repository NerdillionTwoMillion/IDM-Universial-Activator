using System;
using System.Runtime.Serialization;

namespace CodeIsle.LibIpsNet.Exceptions
{
    [Serializable]
    public class IpsInvalidException : Exception
    {
        #region Public Constructors

        public IpsInvalidException()
            : base() { }

        public IpsInvalidException(string message)
            : base(message) { }

        public IpsInvalidException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public IpsInvalidException(string message, Exception innerException)
            : base(message, innerException) { }

        public IpsInvalidException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        #endregion Public Constructors

        #region Protected Constructors

        protected IpsInvalidException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        #endregion Protected Constructors
    }
}