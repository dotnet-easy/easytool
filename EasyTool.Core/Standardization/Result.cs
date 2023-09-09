using System;
using System.Collections.Generic;

namespace EasyTool
{
    public record Result
    {
        public string? Message { get; set; } = null;

        public int? Code { get; set; } = null;

        public bool IsOK { get; set; } = true;

        public Result(bool isOK = true)
        {
            this.IsOK = isOK;
        }

        public Result(string msg)
        {
            this.Message = msg;
            this.IsOK = true;
        }

        public Result(string msg, bool isOK)
        {
            this.Message = msg;
            this.IsOK = isOK;
        }

        public Result(string msg, bool isOK, int? code)
        {
            this.Message = msg;
            this.IsOK = isOK;
            this.Code = code;
        }

        public static Result Ok(string msg = "") => new Result(msg, true);
        public static Result<T> Ok<T>(T data) => new Result<T>(data);
        public static ResultSet<T> OkSet<T>(List<T> dataSet, int total) => new ResultSet<T>(dataSet, total);
        public static Result Fail(string message) => new Result(message, false);
    }

    public record Result<T> : Result
    {
        public T? Data { get; set; }

        public Result() : base()
        {
        }

        public Result(T data) : base(true)
        {
            Data = data;
        }

        public Result(T data, string msg, bool isOK) : base(msg, isOK)
        {
            Data = data;
        }

        public Result(string msg, bool isOK = false) : base(msg, isOK)
        {
            Data = default;
        }

    }

    public record ResultSet<T> : Result<List<T>>
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }

        public ResultSet() : base()
        {

        }

        public ResultSet(List<T> dataSet) : base(dataSet)
        {
            Total = dataSet.Count;
        }

        public ResultSet(List<T> dataSet, int total) : base(dataSet)
        {
            Total = total;
        }

        public ResultSet(string msg, bool isOK = false) : base(msg, isOK)
        {
        }
    }
}
