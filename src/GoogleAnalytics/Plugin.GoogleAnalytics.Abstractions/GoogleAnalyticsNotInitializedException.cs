using System;

namespace Plugin.GoogleAnalytics.Abstractions
{
    public class GoogleAnalyticsNotInitializedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MyException"/> class
        /// </summary>
        public GoogleAnalyticsNotInitializedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MyException"/> class
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        public GoogleAnalyticsNotInitializedException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MyException"/> class
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        /// <param name="inner">The exception that is the cause of the current exception. </param>
        public GoogleAnalyticsNotInitializedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
