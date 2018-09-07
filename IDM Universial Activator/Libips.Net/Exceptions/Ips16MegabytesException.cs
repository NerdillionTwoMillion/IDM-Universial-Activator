using System;
using System.Runtime.Serialization;

namespace CodeIsle.LibIpsNet.Exceptions
{
    [Serializable]
    public class Ips16MegabytesException : Exception
    {
        #region Public Constructors

        public Ips16MegabytesException()
            : base() { }

        public Ips16MegabytesException(string message)
            : base(message) { }

        public Ips16MegabytesException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public Ips16MegabytesException(string message, Exception innerException)
            : base(message, innerException) { }

        public Ips16MegabytesException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        #endregion Public Constructors

        #region Protected Constructors

        protected Ips16MegabytesException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        #endregion Protected Constructors
    }
}