using System;
using System.Runtime.Serialization;

namespace CodeIsle.LibIpsNet.Exceptions
{
    [Serializable]
    public class IpsScrambledException : Exception
    {
        #region Public Constructors

        public IpsScrambledException()
            : base() { }

        public IpsScrambledException(string message)
            : base(message) { }

        public IpsScrambledException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public IpsScrambledException(string message, Exception innerException)
            : base(message, innerException) { }

        public IpsScrambledException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        #endregion Public Constructors

        #region Protected Constructors

        protected IpsScrambledException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        #endregion Protected Constructors
    }
}