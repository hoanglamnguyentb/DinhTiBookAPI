using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.Dtos
{
    public class ResponseWithDataDto<T>
    {
        //them ma code
        public string? Status { get; set; } // true false
        public T? Data { get; set; }
        public string? Message { get; set; }
        public int Code { get; set; }
    }

    public class ResponseWithMessageDto
    {
        public string Message { get; set; }
        public string Status { get; set; }
        public int Code { get; set; }
    }
}
