using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Clients.Objects
{
    /// <summary>
    /// The result of an operation
    /// </summary>
    public class CallResult
    {
        /// <summary>
        /// An error if the call didn't succeed, will always be filled if Success = false
        /// </summary>
        public Error? Error { get; internal set; }

        /// <summary>
        /// Whether the call was successful
        /// </summary>
        public bool Success => Error == null;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="error"></param>
        public CallResult(Error? error)
        {
            Error = error;
        }

        /// <summary>
        /// Overwrite bool check so we can use if(callResult) instead of if(callResult.Success)
        /// </summary>
        /// <param name="obj"></param>
        public static implicit operator bool(CallResult obj)
        {
            return obj?.Success == true;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Success ? $"Success" : $"Error: {Error}";
        }
    }

    /// <summary>
    /// The result of an operation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CallResult<T> : CallResult
    {
        /// <summary>
        /// The data returned by the call, only available when Success = true
        /// </summary>
        public T Data { get; internal set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="data"></param>
        /// <param name="originalData"></param>
        /// <param name="error"></param>
        protected CallResult([AllowNull] T data, Error? error) : base(error)
        {
            Data = data;
        }

        /// <summary>
        /// Create a new data result
        /// </summary>
        /// <param name="data">The data to return</param>
        public CallResult(T data) : this(data, null) { }

        /// <summary>
        /// Create a new error result
        /// </summary>
        /// <param name="error">The erro rto return</param>
        public CallResult(Error error) : this(default, error) { }

        /// <summary>
        /// Overwrite bool check so we can use if(callResult) instead of if(callResult.Success)
        /// </summary>
        /// <param name="obj"></param>
        public static implicit operator bool(CallResult<T> obj)
        {
            return obj?.Success == true;
        }

        /// <summary>
        /// Whether the call was successful or not. Useful for nullability checking.
        /// </summary>
        /// <param name="data">The data returned by the call.</param>
        /// <param name="error"><see cref="Error"/> on failure.</param>
        /// <returns><c>true</c> when <see cref="CallResult{T}"/> succeeded, <c>false</c> otherwise.</returns>
        public bool GetResultOrError([MaybeNullWhen(false)] out T data, [NotNullWhen(false)] out Error? error)
        {
            if (Success)
            {
                data = Data!;
                error = null;

                return true;
            }
            else
            {
                data = default;
                error = Error!;

                return false;
            }
        }

        /// <summary>
        /// Copy the WebCallResult to a new data type
        /// </summary>
        /// <typeparam name="K">The new type</typeparam>
        /// <param name="data">The data of the new type</param>
        /// <returns></returns>
        public CallResult<K> As<K>([AllowNull] K data)
        {
            return new CallResult<K>(data, Error);
        }

        /// <summary>
        /// Copy as a dataless result
        /// </summary>
        /// <returns></returns>
        public CallResult AsDataless()
        {
            return new CallResult(null);
        }

        /// <summary>
        /// Copy as a dataless result
        /// </summary>
        /// <returns></returns>
        public CallResult AsDatalessError(Error error)
        {
            return new CallResult(error);
        }

        /// <summary>
        /// Copy the WebCallResult to a new data type
        /// </summary>
        /// <typeparam name="K">The new type</typeparam>
        /// <param name="error">The error to return</param>
        /// <returns></returns>
        public CallResult<K> AsError<K>(Error error)
        {
            return new CallResult<K>(default, error);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Success ? $"Success" : $"Error: {Error}";
        }
    }

    /// <summary>
    /// The result of a request
    /// </summary>
    public class WebCallResult : CallResult
    {
        public HttpRequestMessage? RequestMessage { get; set; }

        public HttpResponseMessage? ResponseMessage { get; set; }

        public WebCallResult(
            Error error,
            HttpRequestMessage requestMessage,
            HttpResponseMessage responseMessage
            ) : base(error)
        {
            RequestMessage = requestMessage;
            ResponseMessage = responseMessage;
        }

        public WebCallResult() : base(default) { }

        /// <summary>
        /// Return the result as an error result
        /// </summary>
        /// <param name="error">The error returned</param>
        /// <returns></returns>
        public WebCallResult AsError(Error error)
        {
            return new WebCallResult(error, RequestMessage, ResponseMessage);
        }
    }

    /// <summary>
    /// The result of a request
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WebCallResult<T> : CallResult<T>
    {
        public HttpRequestMessage? RequestMessage { get; set; }

        public HttpResponseMessage? ResponseMessage { get; set; }

        public WebCallResult(
            Error error,
            HttpRequestMessage? requestMessage = default,
            HttpResponseMessage? responseMessage = default)
             : base(error)
        {
            RequestMessage = requestMessage;
            ResponseMessage = responseMessage;
        }

        /// <summary>
        /// Copy as a dataless result
        /// </summary>
        /// <returns></returns>
        public new WebCallResult AsDataless()
        {
            return new WebCallResult();
        }
        /// <summary>
        /// Copy as a dataless result
        /// </summary>
        /// <returns></returns>
        public new WebCallResult AsDatalessError(Error error)
        {
            return new WebCallResult(error, RequestMessage, ResponseMessage);
        }

        /// <summary>
        /// Create a new error result
        /// </summary>
        /// <param name="error">The error</param>
        public WebCallResult(T data) : base(data) { }

        public WebCallResult(T? data, Error? error, HttpRequestMessage? requestMessage, HttpResponseMessage? responseMessage) : base(data, error) 
        {
            RequestMessage = requestMessage;
            ResponseMessage = responseMessage;
        }

        /// <summary>
        /// Copy the WebCallResult to a new data type
        /// </summary>
        /// <typeparam name="K">The new type</typeparam>
        /// <param name="data">The data of the new type</param>
        /// <returns></returns>
        public new WebCallResult<K> As<K>([AllowNull] K data)
        {
            return new WebCallResult<K>(data, Error, RequestMessage, ResponseMessage);
        }

        /// <summary>
        /// Copy the WebCallResult to a new data type
        /// </summary>
        /// <typeparam name="K">The new type</typeparam>
        /// <param name="error">The error returned</param>
        /// <returns></returns>
        public new WebCallResult<K> AsError<K>(Error error)
        {
            return new WebCallResult<K>(default, error, RequestMessage, ResponseMessage);
        }
    }
}