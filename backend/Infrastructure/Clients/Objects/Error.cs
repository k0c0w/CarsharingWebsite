using System;
using System.Net;

namespace Clients.Objects
{
    public abstract class Error
    {
        /// <summary>
        /// The error code from the server
        /// </summary>
        public int? Code { get; set; }

        /// <summary>
        /// The message for the error that occurred
        /// </summary>
        public string Message { get; set; }

        protected Error(int? code, string message) 
        {
            Code = code;
            Message = message;
        }
    }
    public abstract class Error<T> : Error
    {
        /// <summary>
        /// The data which caused the error
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        protected Error(int? code, string message, T? data) : base(code, message)
        {
            Data = data;
        }
    }

    /// <summary>
    /// Cant reach server error
    /// </summary>
    public class CantConnectError : Error<HttpRequestException>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public CantConnectError() : this("Can't connect to the server", null) { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public CantConnectError(string message, HttpRequestException? data) : base(default, message, data) { }
    }

    public class NoApiCredentialsError<T> : Error<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public NoApiCredentialsError() : this("No credentials were provided") { }
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public NoApiCredentialsError(string message, T? data = default) : base(default, message, data) { }
    }

    /// <summary>
    /// Error returned by the server
    /// </summary>
    public class ServerError<T> : Error<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public ServerError(string message, T? data = default) : this(0, message, data) { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public ServerError(int? code, string message, T? data = default) : base(code, message, data) { }
    }

    /// <summary>
    /// Unknown error
    /// </summary>
    public class UnknownError<T> : Error<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="data">Error data</param>
        public UnknownError(string message, T? data = default) : this(null, message, data) { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public UnknownError(int? code, string message, T? data) : base(code, message, data) { }
    }

    /// <summary>
    /// An invalid parameter has been provided
    /// </summary>
    public class ArgumentError<T> : Error<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="message"></param>
        public ArgumentError(string message) : this(message, default) { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public ArgumentError(string message, T? data) : base(default, message, data) { }
    }
}