using System;
using System.Runtime.Serialization;

namespace CodeIsle.LibIpsNet.Exceptions
{
    [Serializable]
    public class IpsIdenticalException : Exception
    {
        #region Public Constructors

        public IpsIdenticalException()
            : base() { }

        public IpsIdenticalException(string message)
            : base(message) { }

        public IpsIdenticalException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public IpsIdenticalException(string message, Exception innerException)
            : base(message, innerException) { }

        public IpsIdenticalException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        #endregion Public Constructors

        #region Protected Constructors

        protected IpsIdenticalException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        #endregion Protected Constructors
    }
}