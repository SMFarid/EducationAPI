using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;

namespace EducationAPI.Common
{
    public class CommonResponse<T>
    {

        public CommonResponse()
        {
            Errors = new List<Error>();

        }
        public bool IsSuccess { get => Errors.Count == 0; }
        public List<Error> Errors { get; set; }
        public T Data { get; set; }
    }
}
