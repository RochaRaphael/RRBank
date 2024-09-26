using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRBank.Application.Model
{
    public class ResultViewModel<T>
    {
        public ResultViewModel(T data, List<string> errors)
        {
            Data = data;
            Errors = errors;
        }

        public ResultViewModel(bool success, string message)
        {
            Message = message;
            Success = success;
        }

        public ResultViewModel(bool success)
        {
            Success = success;
        }

        public ResultViewModel(T data)
        {
            Data = data;
            Success = true;
        }

        public ResultViewModel(List<string> errors)
        {
            Errors = errors;
            Success = false;
        }

        public ResultViewModel(string error)
        {
            Errors.Add(error);
            Success = false;
        }

        public T Data { get; private set; }
        public List<string> Errors { get; private set; } = new();
        public bool Success { get; private set; }
        public string Message { get; set; } = string.Empty;

    }
}
