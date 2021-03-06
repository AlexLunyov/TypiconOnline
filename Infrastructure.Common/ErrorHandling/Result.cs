﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Infrastructure.Common.ErrorHandling
{
    public class Result
    {
        public string Error { get; }

        /// <summary>
        /// Код ошибки
        /// </summary>
        public int ErrorCode { get; }

        public bool Success => Error == null;
        public bool Failure => !Success;

        protected Result() { }

        protected Result(string message)
        {
            //if (String.IsNullOrEmpty(message))
            //    throw new ArgumentException();

            Error = message;
        }

        protected Result(int errorCode, string message = "") : this(message)
        {
            ErrorCode = errorCode;
        }

        public static Result Fail(string message) => new Result(message);

        public static Result Fail(int errorCode, string message) => new Result(errorCode, message);

        public static Result Ok() => new Result();

        public static Result<T> Fail<T>(string message) => new Result<T>(default, message);

        public static Result<T> Fail<T>(int errorCode, string message) => new Result<T>(default, errorCode, message);

        public static Result<T> Ok<T>(T value) => new Result<T>(value);

        public static Result Combine(params Result[] results)
        {
            var errors = new StringBuilder();

            foreach (Result result in results)
            {
                if (result.Failure)
                {
                    errors.AppendLine(result.Error);
                }
            }

            return (errors.Length == 0) 
                ? Ok()
                : Fail(errors.ToString());
        }
    }


    public class Result<T> : Result
    {
        private T _value;

        public T Value
        {
            get
            {
                Contract.Requires(Success);

                return _value;
            }
            private set { _value = value; }
        }

        protected internal Result(T value)
            : base()
        {
            Value = value;
        }

        protected internal Result(T value, string error)
            : base(error)
        {
            Contract.Requires(value != null || !string.IsNullOrEmpty(error));

            Value = value;
        }

        protected internal Result(T value, int errorCode, string error)
            : base(errorCode, error)
        {
            Contract.Requires(value != null || !string.IsNullOrEmpty(error));

            Value = value;
        }
    }
}
