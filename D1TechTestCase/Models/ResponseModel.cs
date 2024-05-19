using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace D1TechTestCase.Core.Models
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }


        public List<String> Errors { get; set; }


        public static ResponseModel<T> Success(int statusCode, T data)
        {
            return new ResponseModel<T> { Data = data, StatusCode = statusCode };
        }
        public static ResponseModel<T> Success(int statusCode)
        {
            return new ResponseModel<T> { StatusCode = statusCode };
        }

        public static ResponseModel<T> Fail(int statusCode, List<string> errors)
        {
            return new ResponseModel<T> { StatusCode = statusCode, Errors = errors };
        }

        public static ResponseModel<T> Fail(int statusCode, string error)
        {
            return new ResponseModel<T> { StatusCode = statusCode, Errors = new List<string> { error } };
        }
    }
}
