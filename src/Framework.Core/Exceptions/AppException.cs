using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Exceptions
{
    public class AppException : ApplicationException
    {
        /// <summary>Initializes a new instance of the class.</summary>
        public AppException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the class with a specified error message.
        /// </summary>
        /// <param name="message">The error message string.</param>
        public AppException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the class with a specified error message.
        /// </summary>
        /// <param name="message">The error message string.</param>
        public AppException(string message, params object[] args)
            : base(string.Format(message, args))
        {

        }

        /// <summary>
        /// Initializes a new instance of the class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message string.</param>
        /// <param name="innerException">The inner exception reference.</param>
        public AppException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
